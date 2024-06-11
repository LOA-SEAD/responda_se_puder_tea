using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class proxQuestao : MonoBehaviour
{
    
    public TextMeshProUGUI pontos_player;
    public Animator anim;
    private Jogo jogoScript;


    void Start(){
        //pontos_player.text = Informacoes.GetPontos().ToString();
       jogoScript = GameObject.Find("JogoFunctions").GetComponent<Jogo>();
       // Debug.Log(nivel_atual);
    }

    public void AtivarProxQuestao(){
       if(jogoScript.AtualizarNivel() == 1)
            SceneManager.LoadScene("Nivel");
        else
            anim.SetTrigger("zoom");
   }

 
}
