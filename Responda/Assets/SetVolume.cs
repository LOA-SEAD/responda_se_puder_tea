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

    public Text TextoVoltar;

    int origem;

    void Start()
    {
        origem = Informacoes.GetOrigem();
        if(origem == 0){
            TextoVoltar.text = "Voltar para o Menu";
        }
        else if(origem == 1){
            TextoVoltar.text = "Voltar para as Opções";
        }
        MusicaFundo.value = Informacoes.GetValueMusicaFundo();
        Efeitos.value = Informacoes.GetValueEfeitos();
        LeituraTexto.value = Informacoes.GetValueLeituraTexto();
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
