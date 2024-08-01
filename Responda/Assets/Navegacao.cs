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
        //Shift + Tab
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if(Input.GetKeyDown(KeyCode.Tab)){
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
                            if (current == dica || current == ajuda5050 || current == pular || current == opcoes)
                            {
                                index = -1;
                                if(alternativa1.interactable){
                                    alternativa1.Select();
                                }
                                else if(alternativa2.interactable){
                                    alternativa2.Select();
                                }
                                else{
                                    alternativa3.Select();
                                }
                            }
                            else if (current == alternativa1 || current == alternativa2 || current == alternativa3 || current == alternativa4)
                            {
                                pergunta.Select();
                            }
                            else if (current == pergunta){
                                dica.Select();
                                index = 0;
                            }
                        }
                    }
                }
            }
        }

        //Tab
        else if (Input.GetKeyDown(KeyCode.Tab))
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
                        else if(current == dica || current == ajuda5050 || current == pular || current == opcoes){
                            index = -1;
                            pergunta.Select();
                        }
                    }
                }
            }
        }

        //Seta pra baixo
        if (Input.GetKeyDown(KeyCode.DownArrow))
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
                        if (current == alternativa1)
                        {
                            if(alternativa2.interactable){
                                alternativa2.Select();
                            }
                            else if(alternativa3.interactable){
                                alternativa3.Select();
                            }
                            else{
                                alternativa4.Select();
                            }
                        }
                        else if (current == alternativa2)
                        {
                            if(alternativa3.interactable){
                                alternativa3.Select();
                            }
                            else if (alternativa4.interactable){
                                alternativa4.Select();
                            }
                        }
                        else if (current == alternativa3)
                        {
                            if(alternativa4.interactable){
                                alternativa4.Select();
                            }
                        }
                        else if (current == dica)
                        {
                            if(ajuda5050.interactable){
                                ajuda5050.Select();
                                index = 1;
                            }
                            else if(pular.interactable){
                                pular.Select();
                                index = 2;
                            }
                            else if(confirmar.interactable){
                                confirmar.Select();
                            }
                        }
                        else if (current == ajuda5050)
                        {
                            if(pular.interactable){
                                pular.Select();
                                index = 2;
                            }
                            else if(confirmar.interactable){
                                confirmar.Select();
                            }
                        }
                        else if (current == pular)
                        {
                            if(confirmar.interactable){
                                confirmar.Select();
                            }
                        }
                    }
                }
            }
        }

        //Seta pra cima
        if (Input.GetKeyDown(KeyCode.UpArrow))
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
                        if (current == alternativa4)
                        {
                            if(alternativa3.interactable){
                                alternativa3.Select();
                            }
                            else if(alternativa2.interactable){
                                alternativa2.Select();
                            }
                            else{
                                alternativa1.Select();
                            }
                        }
                        else if (current == alternativa3)
                        {
                            if(alternativa2.interactable){
                                alternativa2.Select();
                            }
                            else if(alternativa1.interactable){
                                alternativa1.Select();
                            }
                        }
                        else if (current == alternativa2)
                        {
                            if(alternativa1.interactable){
                                alternativa1.Select();
                            }
                        }
                        else if (current == pular)
                        {
                            if(ajuda5050.interactable){
                                ajuda5050.Select();
                                index = 1;
                            }
                            else{
                                dica.Select();
                                index = 0;
                            }
                        }
                        else if (current == ajuda5050)
                        {
                            dica.Select();
                             index = 0;
                        }
                        else if (current == confirmar)
                        {
                            if(pular.interactable){
                                pular.Select();
                            }
                            else if(ajuda5050.interactable){
                                ajuda5050.Select();
                            }
                            else{
                                dica.Select();
                            }
                        }
                    }
                }
            }
        }
    }
}