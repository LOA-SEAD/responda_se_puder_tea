using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Networking;
public class Opcoes : MonoBehaviour
{
    const int ORIGEM_MENU = 0;
    const int ORIGEM_JOGO = 1;

    public Animator Panel_confirmar_anim;

    public Button sim;

    public Button sair;

    public AudioSource confirmar;
    public AudioSource nao;

    // Start is called before the first frame update
    void Start()
    {
        confirmar.volume = Informacoes.GetValueLeituraTexto();
        nao.volume = Informacoes.GetValueLeituraTexto();
        confirmar.Stop();
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

    public void MostrarPainelConfirmar()
    {
        Panel_confirmar_anim.SetBool("showPanel", true);
        sim.Select();
    }

    public void fecharPainelConfirmar()
    {
        Panel_confirmar_anim.SetBool("showPanel", false);
        sair.Select();
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
