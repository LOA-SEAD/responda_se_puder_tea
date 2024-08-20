using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class audioMNivel : MonoBehaviour
{
 public AudioSource adSource;
    public AudioClip[] adClips;

    public AudioClip[] variacoes;
    public Button instrucoes; 

    public AudioSource plateia;

    private IEnumerator coroutine;

    // Start is called before the first frame update

    private void AtualizarAudios(AudioSource p){
        plateia.volume = Informacoes.GetValueEfeitos();
        p.volume = Informacoes.GetValueLeituraTexto();
    }
    void Start()
    {
        coroutine = playAudioSequentially();
        StartCoroutine(coroutine);
        //Informacoes.SetPontosGanhos(15);
        GetPontos();
    }

    void GetPontos(){

        if(Informacoes.GetPontosGanhos() == 10){
            adClips[1] = variacoes[0];
        }else if(Informacoes.GetPontosGanhos() == 15){
            adClips[1] = variacoes[1];
        }else if(Informacoes.GetPontosGanhos() == 20){
            adClips[1] = variacoes[2];
        }else if(Informacoes.GetPontosGanhos() == 25){
            adClips[1] = variacoes[3];
        }

    }

    IEnumerator playAudioSequentially(){
        yield return null;

        //1.Loop through each AudioClip
        for (int i = 0; i < adClips.Length; i++)
        {
            //2.Assign current AudioClip to audiosource
            adSource.clip = adClips[i];

            //3.Play Audio
            AtualizarAudios(adSource);
            adSource.Play();

            //4.Wait for it to finish playing
            while (adSource.isPlaying)
            {
                yield return null;
            }

            //5. Go back to #2 and play the next audio in the adClips array
        }
        instrucoes.Select();
    }

    public void StopAudio(){
        
        StopCoroutine(coroutine);
    }
}
