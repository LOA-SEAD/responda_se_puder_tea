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

    public Button recomecar;

    public Button instrucoes;

    public Button configuracoes;

    public Button sairBotao;

    public AudioSource confirmar;
    public AudioSource nao;

    public AudioSource confirmarBotao;
    public AudioSource recomecarBotao;
    public AudioSource intrudcoesBotao;

    public AudioSource configuracoesBotao;

    public AudioSource sairButton;

    public AudioSource simm;

    // Start is called before the first frame update

    void AttAudio()
    {
        //Informacoes.SetValueLeituraTexto(0.5f);
        confirmar.volume = Informacoes.GetValueLeituraTexto();
        nao.volume = Informacoes.GetValueLeituraTexto();
        confirmarBotao.volume = Informacoes.GetValueLeituraTexto();
        recomecarBotao.volume = Informacoes.GetValueLeituraTexto();
        intrudcoesBotao.volume = Informacoes.GetValueLeituraTexto();
        configuracoesBotao.volume = Informacoes.GetValueLeituraTexto();
        sairButton.volume = Informacoes.GetValueLeituraTexto();
        simm.volume = Informacoes.GetValueLeituraTexto();
        confirmar.Stop();
        nao.Stop();
        confirmarBotao.Stop();
        recomecarBotao.Stop();
        intrudcoesBotao.Stop();
        configuracoesBotao.Stop();
        sairButton.Stop();
        simm.Stop();

    }
    void Start()
    {
        AttAudio();
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
        confirmar.Play();
        sim.Select();
        //confirmar.enabled = false;
        confirmarBotao.enabled = false;
        recomecarBotao.enabled = false;
        intrudcoesBotao.enabled = false;
        configuracoesBotao.enabled = false;
        sairButton.enabled = false;
        sair.interactable = false;
        recomecar.interactable = false;
        instrucoes.interactable = false;
        configuracoes.interactable = false;
        sairBotao.interactable= false;


    }

    public void fecharPainelConfirmar()
    {
        Panel_confirmar_anim.SetBool("showPanel", false);
        sair.Select();
        confirmar.enabled = true;
        confirmarBotao.enabled = true;
        recomecarBotao.enabled = true;
        intrudcoesBotao.enabled = true;
        configuracoesBotao.enabled = true;
        sairButton.enabled = true;
        sair.interactable = true;
        recomecar.interactable = true;
        instrucoes.interactable = true;
        configuracoes.interactable = true;
        sairBotao.interactable= true;
        AttAudio();

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
