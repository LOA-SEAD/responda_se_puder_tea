using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public Slider MusicaFundo;

    public AudioSource MusicaFundoAudio;
    public Slider Efeitos;
    public AudioSource EfeitosAudio;
    public Slider LeituraTexto;
    public AudioSource LeituraTextoAudio;

    public AudioSource MusicaFundoAudioFundo;
    public AudioSource EfeitosAudioFundo;
    public AudioSource LeituraTextoAudioFundo;

    public AudioSource voltarAudio;
    public AudioClip[] voltarAudioClip;

    public Text TextoVoltar;

    public Text TextoVoltarFundo;

    public Button voltar;

    public int origem;

    public int posicao;

    void Start()
    {

        origem = Informacoes.GetOrigem();
        if(origem == 0){
            TextoVoltar.text = "Voltar";
            TextoVoltarFundo.text = "Voltar";
            voltarAudio.clip = voltarAudioClip[1];
            voltarAudio.volume = Informacoes.GetValueLeituraTexto();

        }
        else if(origem == 1){
            TextoVoltar.text = "Voltar";
            TextoVoltarFundo.text = "Voltar";
            voltarAudio.clip = voltarAudioClip[0];
            voltarAudio.volume = Informacoes.GetValueLeituraTexto();

        }
        
        MusicaFundo.value = Informacoes.GetValueMusicaFundo();
        Efeitos.value = Informacoes.GetValueEfeitos();
        LeituraTexto.value = Informacoes.GetValueLeituraTexto();
        MusicaFundoAudioFundo.volume = Informacoes.GetValueLeituraTexto();
        EfeitosAudioFundo.volume = Informacoes.GetValueLeituraTexto();
        LeituraTextoAudioFundo.volume = Informacoes.GetValueLeituraTexto();
        voltarAudio.volume = Informacoes.GetValueLeituraTexto();
        MusicaFundoAudioFundo.Stop();
        EfeitosAudioFundo.Stop();
        LeituraTextoAudioFundo.Stop();
        voltarAudio.Stop();

        MusicaFundo.Select();

    }

    void Update()
    {


    }


    public void AtualizarMusicaFundo(){

        if(MusicaFundo.value != Informacoes.GetValueMusicaFundo()){
        Informacoes.SetValueMusicaFundo(MusicaFundo.value);
        MusicaFundoAudio.volume = MusicaFundo.value;
        MusicaFundoAudio.Play();
        }

    }

    public void AtualizarEfeitos(){
        
        if(Efeitos.value != Informacoes.GetValueEfeitos()){
        Informacoes.SetValueEfeitos(Efeitos.value);
        EfeitosAudio.volume = Efeitos.value;
        EfeitosAudio.Play();
        }
    }

    public void AtualizarLeituraTexto(){
            
        if(LeituraTexto.value != Informacoes.GetValueLeituraTexto()){
        Informacoes.SetValueLeituraTexto(LeituraTexto.value);
        LeituraTextoAudio.volume = LeituraTexto.value;
        LeituraTextoAudio.Play();
        }
    }
}
