using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Creditos : MonoBehaviour
{
    public bool FindFirstSelectableCreditos = false;

    public Text paginacao;
    public Text texto_creditos;
    public Button botao_esquerda;
    public Button botao_direita;
    public Button voltar;
    public GameObject bolinha_esquerda;
    public GameObject bolinha_direita;

    int pag;

    void Start()
    {
        #if UNITY_ANDROID
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            //Screen.fullScreen = false;
        #endif
        pag = 1;
        AtualizaCreditos(pag);
    }

    // Update is called once per frame
    void Update()
    {
        //Direita
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            PagSeguinte();
        }

        //Esquerda
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PagAnterior();
        }
    }

    public void AtualizaCreditos(int novapag){
        botao_direita.interactable = true;
        botao_esquerda.interactable = true;
        bolinha_esquerda.SetActive(true);
        bolinha_direita.SetActive(true);
        if (novapag == 1){
            texto_creditos.text = "Página 1";
            paginacao.text = "1/4";
            botao_esquerda.interactable = false;
            bolinha_esquerda.SetActive(false);
        }
        else if (novapag == 2){
            texto_creditos.text = "Página 2";
            paginacao.text = "2/4";
        }
        else if (novapag == 3){
            texto_creditos.text = "Página 3";
            paginacao.text = "3/4";
        }
        else if (novapag == 4){
            texto_creditos.text = "Página 4";
            paginacao.text = "4/4";
            botao_direita.interactable = false;
            bolinha_direita.SetActive(false);
        }
    }

    public void PagSeguinte()
    {
        if (pag <= 3){
            AtualizaCreditos(++pag);
        }
    }

    public void PagAnterior()
    {
        if (pag >= 2){
            AtualizaCreditos(--pag);
        }
    }
}
