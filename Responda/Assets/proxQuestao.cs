using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class proxQuestao : MonoBehaviour
{
    
    public TextMeshProUGUI pontos_player;

    public TextMeshProUGUI pontos;
    public Animator anim;
    private Jogo jogoScript;


    void Start(){
        //pontos_player.text = Informacoes.GetPontos().ToString();
       jogoScript = GameObject.Find("JogoFunctions").GetComponent<Jogo>();
       AttTexto();
       // Debug.Log(nivel_atual);
    }

    public void AtivarProxQuestao(){
       if(jogoScript.AtualizarNivel() == 1)
            SceneManager.LoadScene("Nivel");
        else
            anim.SetTrigger("zoom");
   }

   public void AttTexto(){
         pontos.text = "Você ganhou " + Informacoes.GetPontosGanhos().ToString() + " pontos!";
   }

 
}
