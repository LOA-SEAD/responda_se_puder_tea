using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

using System.IO;

public class CarregaDados : MonoBehaviour
{
    private static string arquivo = "DadosResponda.json";
    private static string path;

    public static List<DadosJogo> listaDados = new List<DadosJogo>();

    // Start is called before the first frame update
    void Start()
    {
        //path = Application.persistentDataPath + '/' + arquivo;
        //path = Application.streamingAssetsPath + '/' + arquivo;
        //Debug.Log("Caminho: " + path);
        //Load();
    }

    public static void Load()
    { 
        BetterStreamingAssets.Initialize();
        listaDados.Clear();
        //path = Application.streamingAssetsPath + '/' + arquivo;

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

        /*using (StreamReader sr = new StreamReader(path))
        {
            while(sr.Peek() >= 0)
            {
                DadosJogo DadosJson = JsonUtility.FromJson<DadosJogo>(sr.ReadLine());
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
        }*/
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
