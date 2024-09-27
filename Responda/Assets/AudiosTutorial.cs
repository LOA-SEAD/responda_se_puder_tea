using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AudiosTutorial : MonoBehaviour
{
    // Start is called before the first frame update
     public AudioSource pergunta;
     public Button botao_pergunta;
     public AudioSource alternativa1;
     public AudioSource alternativa2;
     public AudioSource alternativa3;
     public AudioSource alternativa4;
     public AudioSource dica;
    public AudioClip[] adClips;

    public AudioClip[] perguntas;

    public AudioClip[] variacoes;

    public AudioClip pergunta_2_fase;
    public AudioClip dica_2_fase;

    public int pcont = 0;

    public int questao = 0;

    public int guarda_pergunta = 0;

    public void AttPergunta(){
        pcont++;
        pergunta.clip = perguntas[pcont];
        pergunta.Stop();
        pergunta.volume = Informacoes.GetValueLeituraTexto();
        botao_pergunta.Select();
        pergunta.Play();

    }

    public void AttPergunta_botao(){
        if(questao == 0){
            AttPergunta();
        }else if(questao == 1){
            if(guarda_pergunta == 2){
                perguntas[5] = variacoes[0];
            }else{
                perguntas[5] = variacoes[1];
            }
            AttPergunta();

        }else if(questao == 2){
            if(guarda_pergunta == 1){
                perguntas[9] = variacoes[2];
            }else{
                perguntas[9] = variacoes[3];
            }
            AttPergunta();
        }else if(questao == 3){
            if(guarda_pergunta == 1){
                perguntas[9] = variacoes[4];
            }else{
                perguntas[9] = variacoes[5];
            }
            AttPergunta();
        }
        questao++;
        botao_pergunta.Select();
    }

    public void GetPergunta(int bot){
        guarda_pergunta = bot;
    }

    public void Pular(){
        alternativa1.clip = adClips[10];
        alternativa1.volume = Informacoes.GetValueLeituraTexto();
        alternativa2.clip = adClips[11];
        alternativa2.volume = Informacoes.GetValueLeituraTexto();
        alternativa3.clip = adClips[12];
        alternativa3.volume = Informacoes.GetValueLeituraTexto();
        alternativa4.clip = adClips[13];
        alternativa4.volume = Informacoes.GetValueLeituraTexto();
        dica.clip = adClips[14];
        dica.volume = Informacoes.GetValueLeituraTexto();
        pergunta.clip = pergunta_2_fase;
        pergunta.volume = Informacoes.GetValueLeituraTexto();
        dica.clip = dica_2_fase;
        dica.volume = Informacoes.GetValueLeituraTexto();
        botao_pergunta.Select();
        questao++;

    }
    

    void Start()
    {
        botao_pergunta.Select();
        pergunta.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pcont == 3){

            alternativa1.clip = adClips[0];
            alternativa1.volume = Informacoes.GetValueLeituraTexto();
            alternativa2.clip = adClips[1];
            alternativa2.volume = Informacoes.GetValueLeituraTexto();
            alternativa3.clip = adClips[2];
            alternativa3.volume = Informacoes.GetValueLeituraTexto();
            alternativa4.clip = adClips[3];
            alternativa4.volume = Informacoes.GetValueLeituraTexto();
        }
        
        if(pcont == 7){
            alternativa1.clip = adClips[5];
            alternativa1.volume = Informacoes.GetValueLeituraTexto();
            alternativa2.clip = adClips[6];
            alternativa2.volume = Informacoes.GetValueLeituraTexto();
            alternativa3.clip = adClips[7];
            alternativa3.volume = Informacoes.GetValueLeituraTexto();
            alternativa4.clip = adClips[8];
            alternativa4.volume = Informacoes.GetValueLeituraTexto();
            dica.clip = adClips[9];
            dica.volume = Informacoes.GetValueLeituraTexto();
        }
    }
}
