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

    public bool fadein;

    public bool teste;

    public int pergunta = Informacoes.GetPerguntaAtual();

    public int pergunta_facil = Informacoes.GetQuantidadeFacil();
    public int pergunta_medio = Informacoes.GetQuantidadeMedio();
    public int pergunta_dificil = Informacoes.GetQuantidadeDificil();
    public int perguntas_totais = Informacoes.GetQuantidadeFacil() + Informacoes.GetQuantidadeMedio() + Informacoes.GetQuantidadeDificil();

    public float carrega;


    void Start(){
        //pontos_player.text = Informacoes.GetPontos().ToString();
       jogoScript = GameObject.Find("JogoFunctions").GetComponent<Jogo>();
       AttTexto();
       // Debug.Log(nivel_atual);
    }

    public void AtivarProxQuestao(){
       if(jogoScript.AtualizarNivel() == 1)
            SceneManager.LoadScene("Nivel");
        else if(Informacoes.GetPerguntaAtual() >= Informacoes.GetQuantidadeFacil() + Informacoes.GetQuantidadeMedio() + Informacoes.GetQuantidadeDificil()){
            anim.SetTrigger("zoom");
            //carrega = 0;
            fadein = true;
        }
        else 
            anim.SetTrigger("zoom");
   }

   public void AttTexto(){
         pontos.text = "Você ganhou " + Informacoes.GetPontosGanhos().ToString() + " pontos!";
   }

   void Update(){

        if(Informacoes.GetPerguntaAtual() >= Informacoes.GetQuantidadeFacil() + Informacoes.GetQuantidadeMedio() + Informacoes.GetQuantidadeDificil()){
            teste = true;
        }else{
            teste = false;
        
        }

        if(fadein){
            carrega += Time.deltaTime * 2.5f;

            if(carrega >= 1){
                SceneManager.LoadScene("Fim");
            }
        }
       
   }

 
}
