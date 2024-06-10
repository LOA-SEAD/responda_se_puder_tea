using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class carregarTelaJogo : MonoBehaviour
{
    public void carregarJogo(){

        #if UNITY_ANDROID
            SceneManager.LoadScene("JogoPaisagem");
        #else
            if(Informacoes.GetNivel() != 99) //TUTORIAL
            {
                SceneManager.LoadScene("Jogo");
            }
            else
            {
            SceneManager.LoadScene("JogoTutorial");
        }
            
        #endif

    }
}
