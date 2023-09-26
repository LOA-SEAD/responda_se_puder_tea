using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CarregaDados : MonoBehaviour {
    private static string arquivo = "DadosResponda.json";

    private static string[] conteudo;

    public static List<DadosJogo> listaDados = new List<DadosJogo>();

    public static bool isLoaded = false;

    void Start () {
        Load(this);
    }

    public static void Load(MonoBehaviour behaviour) {
        
        if (!isLoaded) {
            Debug.Log("[CarregaDados] - Load() - Inicio");
            Debug.Log("[CarregaDados] - Limpando listaDados - Inicio");
            isLoaded = false;
            listaDados.Clear();
            Debug.Log("[CarregaDados] - Limpando listaDados - Fim");
            #if UNITY_WEBGL
                Debug.Log("[CarregaDados] - UNITY_WEBGL - Inicio");
                behaviour.StartCoroutine(GetByHTTP());
                behaviour.StartCoroutine(ProcessaJson());
                Debug.Log("[CarregaDados] - UNITY_WEBGL - Fim");
            #else
                GetByBSA();
            #endif
            Debug.Log("[CarregaDados] - Load() - Fim");
        }
    }

    private static IEnumerator GetByHTTP() {
        Debug.Log("[CarregaDados] - GetByHTTP() - Inicio");
        Debug.Log(Application.streamingAssetsPath);
        string URL = Path.Combine(Application.streamingAssetsPath, arquivo);
        Debug.Log(URL);
        using(UnityWebRequest www = UnityWebRequest.Get(URL)) {
        	yield return www.SendWebRequest();
            
        	if (www.isNetworkError) {
            	Debug.Log(www.error);
        	}
        	else {
            	// Show results as text
            	// Debug.Log(www.downloadHandler.text);
                conteudo = www.downloadHandler.text.Split('\n');
                isLoaded = true; 
            }
            www.Dispose();
        }
        Debug.Log("[CarregaDados] - GetByHTTP() - Fim");
    }

    private static IEnumerator ProcessaJson() {
        while (!isLoaded)
            yield return new WaitForSeconds(0.1f);
        
        Debug.Log("[ProcessaJSON] - Inicio");
        foreach (string line in conteudo) {
            DadosJogo DadosJson = JsonUtility.FromJson<DadosJogo>(line);
			DadosJogo dados = new DadosJogo();
           	dados.pergunta = DadosJson.pergunta;
            dados.resposta = DadosJson.resposta;
            dados.r2 = DadosJson.r2;
            dados.r3 = DadosJson.r3;
            dados.r4 = DadosJson.r4;
            dados.dica = DadosJson.dica;
            dados.nivel = DadosJson.nivel;
            listaDados.Add(dados);
        }
        Debug.Log("[ProcessaJSON] listaDados.Count = " + listaDados.Count);
        Debug.Log("[ProcessaJSON] - Fim");
    }

    private static void GetByBSA()
    { 
       Debug.Log("[CarregaDados] - GetByBSA() - Inicio");
       BetterStreamingAssets.Initialize();
       var jsonText = BetterStreamingAssets.ReadAllLines(arquivo);
       foreach (var line in jsonText)
       {
            DadosJogo DadosJson = JsonUtility.FromJson<DadosJogo>(line);
            DadosJogo dados = new DadosJogo();
            dados.pergunta = DadosJson.pergunta;
            dados.resposta = DadosJson.resposta;
            dados.r2 = DadosJson.r2;
            dados.r3 = DadosJson.r3;
            dados.r4 = DadosJson.r4;
            dados.dica = DadosJson.dica;
            dados.nivel = DadosJson.nivel;
            listaDados.Add(dados);
       }
       isLoaded = true;
       Debug.Log("[CarregaDados] - GetByBSA() - Fim");
    }
}

[System.Serializable]
public class DadosJogo
{
    public string pergunta;
    public string resposta;
    public string r2;
    public string r3;
    public string r4;
    public string dica;
    public string nivel;
    public string audio_pergunta;
    public string audio_dica;
    public string[] audio_alternativas = new string[4];
}
