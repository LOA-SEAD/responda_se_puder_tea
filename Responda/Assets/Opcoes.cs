using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Opcoes : MonoBehaviour
{
    const int ORIGEM_MENU = 0;
    const int ORIGEM_JOGO = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void continuarJogo()
    {
        #if UNITY_ANDROID
            SceneManager.LoadScene("JogoPaisagem");
        #else
            SceneManager.LoadScene("Jogo");
        #endif
    }

    public void configuracoesAudio()
    {
        Informacoes.SetOrigem(ORIGEM_JOGO);
        SceneManager.LoadScene("Configuracoes");
    }

    public void abrirInstrucoes()
    {
        Informacoes.SetOrigem(ORIGEM_JOGO);
        SceneManager.LoadScene("Instrucoes");
    }

    public void desistir()
    {
        Informacoes.SetCaminhos(1);
        SceneManager.LoadScene("Menu");
    }

    public void Recomecar(){
        Informacoes.SetCaminhos(1);
        Informacoes.SetStatus(0);
		Informacoes.SetNivel(0);
		SceneManager.LoadScene("Nivel");
        //SceneManager.LoadScene("Menu");
    }
}
