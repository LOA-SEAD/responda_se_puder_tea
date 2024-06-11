using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class audioManager : MonoBehaviour
{

    public AudioSource adSource;
    public AudioClip[] adClips;
    public Button instrucoes; 

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(playAudioSequentially());
    }

    IEnumerator playAudioSequentially(){
        yield return null;

        //1.Loop through each AudioClip
        for (int i = 0; i < adClips.Length; i++)
        {
            //2.Assign current AudioClip to audiosource
            adSource.clip = adClips[i];

            //3.Play Audio
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
}
