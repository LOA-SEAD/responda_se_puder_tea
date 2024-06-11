using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MudarNivel : MonoBehaviour
{
    private const int FACIL = 0;
    private const int MEDIO = 1;

    public Text nivel_tela;
    public AudioClip[] audioClips;
    public AudioClip[] audiosSeq;
    public AudioSource adSource;

    public AudioSource plateia;
    public Animator anim;

    private void AtualizarAudios(AudioSource p){
        plateia.volume = Informacoes.GetValueEfeitos();
        p.volume = Informacoes.GetValueLeituraTexto();
    }

    void Start()
    {
        int nivel = Informacoes.GetNivel();
        if (nivel == FACIL)
        {
            Informacoes.SetStatus(0);
            nivel_tela.text = "Nível fácil";
            nivel_tela.color = new Color(0.07f, 0.66f, .20f);
            audiosSeq[0] = audioClips[0];
        }
        else if (nivel == MEDIO)
        {
            nivel_tela.text = "Nível médio";
            nivel_tela.color = new Color(1f, 0.7f, 0f);
            audiosSeq[0] = audioClips[1];
        }
        else
        {
            nivel_tela.text = "Nível difícil";
            nivel_tela.color = new Color(1f, 0.14f, 0f);
            audiosSeq[0] = audioClips[2];
        }
        
        StartCoroutine(playAudioSequentially());
    }

    IEnumerator playAudioSequentially(){
        yield return null;

        //1.Loop through each AudioClip
        for (int i = 0; i < audiosSeq.Length; i++)
        {
            //2.Assign current AudioClip to audiosource
            adSource.clip = audiosSeq[i] ;

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
    }

   public void ZoomInAnim(){
       anim.SetTrigger("zoom");
   }
}
