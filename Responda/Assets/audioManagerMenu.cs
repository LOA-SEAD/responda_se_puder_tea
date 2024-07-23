using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class audioManagerMenu : MonoBehaviour
{

    public AudioSource[] adSource;
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

            if(i == 1)
                adSource[1].Play();
            //2.Assign current AudioClip to audiosource
            adSource[0].clip = adClips[i];

            //3.Play Audio
            adSource[0].Play();

            //4.Wait for it to finish playing
            while (adSource[0].isPlaying)
            {
                yield return null;
            }

            //5. Go back to #2 and play the next audio in the adClips array
        }
        instrucoes.Select();
    }

    public void StopAudio(){
        adSource[0].Stop();
        adSource[1].Stop();
    }
}
