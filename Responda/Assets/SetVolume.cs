using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public Slider MusicaFundo;
    public Slider Efeitos;
    public Slider LeituraTexto;

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
        Informacoes.SetValueMusicaFundo(MusicaFundo.value);
    }

    public void AtualizarEfeitos(){
        Informacoes.SetValueEfeitos(Efeitos.value);
    }

    public void AtualizarLeituraTexto(){
        Informacoes.SetValueLeituraTexto(LeituraTexto.value);
    }
}
