using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.EventSystems;

public class MovimentacaoMouse : MonoBehaviour
{
    public Button[] buttonsToSelect; // Arraste os botões desejados para este campo no Inspector

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && Informacoes.GetCursosBlock() != 2)
        {
            if (EventSystem.current.currentSelectedGameObject == null)
        {
            SelectInteractableButton(buttonsToSelect);
        }
        }
    }

    private void SelectInteractableButton(Button[] buttons)
    {
        foreach (Button button in buttons)
        {
            if (button != null && button.interactable)
            {
                EventSystem.current.SetSelectedGameObject(button.gameObject);
                break; // Seleciona o primeiro botão interactable e interrompe o loop
            }
        }
    }
}
