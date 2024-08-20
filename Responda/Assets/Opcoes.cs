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
    
    public Animator Panel_cursor_anim;

    public Button sim;

    public Button sair;

    public Button recomecar;

    public Button instrucoes;

    public Button configuracoes;

    public Button sairBotao;

    public Button navegaButton;

    public AudioSource confirmar;
    public AudioSource nao;

    public AudioSource confirmarBotao;
    public AudioSource recomecarBotao;
    public AudioSource intrudcoesBotao;

    public AudioSource configuracoesBotao;

    public AudioSource sairButton;

    public AudioSource simm;

    public AudioSource voltar;

    public AudioSource teclado;

    public AudioSource mouse;

    public AudioSource navegaçao;

    public Button mouseButton;

    public Button tecladoButton;

    public Button voltarButton;


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
        voltar.volume = Informacoes.GetValueLeituraTexto();
        teclado.volume = Informacoes.GetValueLeituraTexto();
        mouse.volume = Informacoes.GetValueLeituraTexto();
        navegaçao.volume = Informacoes.GetValueLeituraTexto();
        confirmar.Stop();
        nao.Stop();
        confirmarBotao.Stop();
        recomecarBotao.Stop();
        intrudcoesBotao.Stop();
        configuracoesBotao.Stop();
        sairButton.Stop();
        simm.Stop();
        voltar.Stop();
        teclado.Stop();
        mouse.Stop();
        navegaçao.Stop();

    }
    void Start()
    {
        AttAudio();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FecharPainelCursor()
	{
		Panel_cursor_anim.SetBool("showPanel", false);
		navegaçao.Stop();
		sair.Select();
        LigarBotoes();
		
	}

    public void MostrarPainelCursor()
    {
        Panel_cursor_anim.SetBool("showPanel", true);
        navegaçao.Play();
        mouseButton.Select();
        DesligarBotoes();
    }

    public void SelecionarCursor(int botao)
	{
		if(botao == 0)
		{
			Informacoes.SetCursosBlock(1);
		}else if(botao == 1)
		{
			Informacoes.SetCursosBlock(2);
		}else if(botao == 2)
		{
			Informacoes.SetCursosBlock(0);
		}

		FecharPainelCursor();
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


    public void LigarBotoes(){

        confirmar.enabled = true;
        confirmarBotao.enabled = true;
        recomecarBotao.enabled = true;
        intrudcoesBotao.enabled = true;
        configuracoesBotao.enabled = true;
        navegaçao.enabled = true;
        sairButton.enabled = true;
        sair.interactable = true;
        recomecar.interactable = true;
        instrucoes.interactable = true;
        configuracoes.interactable = true;
        sairBotao.interactable= true;
        navegaButton.interactable = true;
        AttAudio();

    }

    public void DesligarBotoes(){
        confirmarBotao.enabled = false;
        recomecarBotao.enabled = false;
        intrudcoesBotao.enabled = false;
        configuracoesBotao.enabled = false;
        navegaçao.enabled = false;
        sairButton.enabled = false;
        sair.interactable = false;
        recomecar.interactable = false;
        instrucoes.interactable = false;
        configuracoes.interactable = false;
        sairBotao.interactable= false;
        navegaButton.interactable = false;
    }
    public void MostrarPainelConfirmar()
    {
        Panel_confirmar_anim.SetBool("showPanel", true);
        confirmar.Play();
        sim.Select();
        //confirmar.enabled = false;
        DesligarBotoes();


    }

    public void fecharPainelConfirmar()
    {
        Panel_confirmar_anim.SetBool("showPanel", false);
        sair.Select();
        LigarBotoes();

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
