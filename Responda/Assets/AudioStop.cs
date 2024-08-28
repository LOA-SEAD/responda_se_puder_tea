using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioStop : MonoBehaviour
{
    public AudioSource[] audios;

    public void PararAudios(AudioSource audi_button)
    {
       foreach (AudioSource audio in audios)
        {
            if (audio != audi_button) // Verifica se o áudio não é o do próprio botão
            {
                audio.volume = 0;
                audio.Stop();
                audio.Stop();
                audio.time = 0;
                StartCoroutine(RestoreVolumeAfterDelay(audio, 1f));
            }
        }
    }

    private IEnumerator RestoreVolumeAfterDelay(AudioSource audio, float delay)
    {
        yield return new WaitForSeconds(delay); // Espera o tempo especificado (1 segundo)
        audio.volume = Informacoes.GetValueLeituraTexto(); // Restaura o volume
    }
}
