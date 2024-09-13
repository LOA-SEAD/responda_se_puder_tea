using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Navegacao : MonoBehaviour
{
    public bool findFirstSelectable = false;
    public Button pergunta;
    public Button alternativa1;
    public Button alternativa2;
    public Button alternativa3;
    public Button alternativa4;
    public Button dica;
    public Button ajuda5050;
    public Button pular;
    public Button confirmar;

    public Button opcoes;
    public Animator btnAnim;
    public int index = -1;

    void Update()
    {
        

        if(Informacoes.GetCursosBlock() + 10 != 2 ){
            if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (EventSystem.current != null)
            {
                GameObject selected = EventSystem.current.currentSelectedGameObject;
                if(selected == null && findFirstSelectable)
                {
                    Selectable found = (Selectable.allSelectables.Count > 0) ? Selectable.allSelectables[0] : null;
                    if(found != null)
                    {
                        selected = found.gameObject;
                    }
                }
                if (selected != null)
                {
                    Selectable current = (Selectable)selected.GetComponent("Selectable");
                    if (current != null)
                    {
                        if (current == pergunta)
                        {
                            if (alternativa1.interactable){
                                alternativa1.Select();
                            }
                            else if (alternativa2.interactable){
                                alternativa2.Select();
                            }
                            else if (alternativa3.interactable){
                                alternativa3.Select();
                            }
                        }
                        else if (current == alternativa1 || current == alternativa2 || current == alternativa3 || current == alternativa4)
                        {
                            dica.Select();
                            index = 0;
                        }
                        else if(current == dica || current == ajuda5050 || current == pular){
                            index = -2;
                            opcoes.Select();
                        }else if( current == opcoes){
                            index = -1;
                            pergunta.Select();
                        }
                    }
                }
            }
        }

        }
       
    }
}