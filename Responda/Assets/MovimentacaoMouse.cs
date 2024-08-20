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
    public Button buttonToSelect; // Arraste o botão desejado para este campo no Inspector

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && Informacoes.GetCursosBlock() != 2)
        {
            SelectButtonIfNoneSelected(buttonToSelect);
        }
    }

    private void SelectButtonIfNoneSelected(Button button)
    {
        if (button != null && EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(button.gameObject);
        }
    }

}
