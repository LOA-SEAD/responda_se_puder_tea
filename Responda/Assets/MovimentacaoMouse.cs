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
    public Button selecionar;
    private Vector3 lastMousePosition;
    public bool isMouseMoving;
    public bool isInputDetected;

    void Start()
    {
        lastMousePosition = Input.mousePosition;
        isMouseMoving = false;
        isInputDetected = false;
    }

    void Update()
    {
        bool mouseMoved = Input.mousePosition != lastMousePosition;
        bool arrowKeysPressed = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);
        bool tabPressed = Input.GetKey(KeyCode.Tab);

        if(mouseMoved)
            EventSystem.current.SetSelectedGameObject(null);
        
        if(tabPressed && mouseMoved){
            selecionar.Select();
            lastMousePosition = Input.mousePosition;
        }
        
    }

    private IEnumerator HandleInputDetection()
    {
        isInputDetected = true;
        isMouseMoving = true;
        yield return new WaitForSeconds(5f);
        isMouseMoving = false;
        isInputDetected = false;
    }

    public bool IsMouseMoving()
    {
        return isMouseMoving;
    }
}
