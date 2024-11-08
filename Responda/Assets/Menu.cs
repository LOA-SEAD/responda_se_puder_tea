﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    const int ORIGEM_MENU = 0;
    const int ORIGEM_JOGO = 1;

    public Button botao_instrucoes;
    public Button botao_jogar;
    public Button botao_opcoes;
    public Button botao_creditos;
    public Button botao_sair;

    public AudioSource intrucoesAudio;
    public AudioSource jogarAudio;

    public AudioSource configAudio;

    public AudioSource creditosAudio;

    public AudioSource sairAudio;

    public AudioSource topBottomAudio;

    public AudioSource inicioAudio;

    public AudioSource plateia;

    public Image roda;

    public Image fundo;

    private void AtualizarVolume(){
        plateia.volume = Informacoes.GetValueEfeitos();
        intrucoesAudio.volume = Informacoes.GetValueLeituraTexto();
        jogarAudio.volume = Informacoes.GetValueLeituraTexto();
        configAudio.volume = Informacoes.GetValueLeituraTexto();
        creditosAudio.volume = Informacoes.GetValueLeituraTexto();
        sairAudio.volume = Informacoes.GetValueLeituraTexto();
        topBottomAudio.volume = Informacoes.GetValueLeituraTexto();
        inicioAudio.volume = Informacoes.GetValueLeituraTexto();
    }

    // Start is called before the first frame update
    void Start()
    {
        AtualizarVolume();
        Debug.Log("[Menu] Start - Inicio");
        CarregaDados.Load(this);
        Debug.Log("[Menu] Quantidade itens: " + CarregaDados.listaDados.Count);

        #if UNITY_ANDROID
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            //Screen.fullScreen = false;
        #endif
        Informacoes.SetOrigem(ORIGEM_MENU);
        Debug.Log("[Menu] Start - Fim");
    }

    // Update is called once per frame
    void Update()
    {
        roda.transform.Rotate(0, 0, 0.05f);   
        fundo.color = new Color(1, 1, 0.5f + Mathf.PingPong(Time.time/3, 0.5f), 1);
    }

    public void HighlightMenu (int botao){
        #if UNITY_ANDROID
            if(botao == 0){
                botao_instrucoes.Select();
            }
            if(botao == 1){
                botao_jogar.Select();
            }
            if(botao == 2){
                botao_opcoes.Select();
            }
            if(botao == 3){
                botao_creditos.Select();
            }
            if(botao == 4){
                botao_sair.Select();
            }
        #endif
    }
}
