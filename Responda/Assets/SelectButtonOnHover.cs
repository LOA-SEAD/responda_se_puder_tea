using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SelectButtonOnHover : MonoBehaviour, IPointerEnterHandler
{
    public AudioSource hoverSound; // Arraste o áudio desejado para este campo no Inspector
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Reproduz o áudio quando o mouse está sobre o botão
        if (hoverSound != null)
        { 
            if(GetComponent<Button>() != null)
            {
                if (GetComponent<Button>().IsInteractable()) //Não reproduzir se estiver interactable = false
                {
                    audioSource = hoverSound;
                    audioSource.volume = Informacoes.GetValueLeituraTexto();
                    audioSource.Play();
                }
            }
           
            
        }

        EventSystem.current.SetSelectedGameObject(gameObject);
    }


}