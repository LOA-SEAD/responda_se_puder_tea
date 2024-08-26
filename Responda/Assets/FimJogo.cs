using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class FimJogo : MonoBehaviour
{
    public Text pontos;
    public Text bonus_tela;
    public Text x5050;
    public Text pular;
    public Text pontos_final;
    public Button botao;
    public Button prosseguir;

    public Text texto_prosseguir;

    public CanvasGroup canvasGroup;


    public Image imagem;
    public Sprite[] spritearray;

    double bonus_d;
    int pontuacao;
    int bonus;

    int bonus_5050;
    int bonus_pular;

    private bool fadein;

    public AudioSource avancar;

    void AttAudios(){
        avancar.volume = Informacoes.GetValueLeituraTexto();
        avancar.Stop();

    }

    void Start()
    {
        
        // imagem.SetActive(false);

        canvasGroup.alpha = 0;
        fadein = false;
        pontuacao = Informacoes.GetPontos();
        Informacoes.SetCaminhos(1);
        CalcularBonus();
        //prosseguir.Select();
        AttAudios();   
        botao.onClick.AddListener(() => Voltar());
    }

    void Voltar(){
		SceneManager.LoadScene("Menu");
	}
    
    private void ColocarPontuacao()
    {
        pontos.text = pontuacao.ToString();
        
    }

    void Update()
    {

        if (fadein)
        {
            canvasGroup.alpha += Time.deltaTime*0.5f;
        }

        if (canvasGroup.alpha >= 1)
        {
            fadein = false;
            SceneManager.LoadScene("Final");

        }

        ColocarPontuacao();

    }

    public void Finalizar()
    {

        fadein = true;
        imagem.sprite = spritearray[0];
        prosseguir.enabled = false;
        texto_prosseguir.text = "";
    }

    public void MenuVoltar(){
        Informacoes.SetStatus(0);
        SceneManager.LoadScene("Menu");
    
    }


    private void CalcularBonus()
    {
        // bonus_d = (Informacoes.GetQuantidadeFacil() + Informacoes.GetQuantidadeMedio() + Informacoes.GetQuantidadeDificil()) * 0.2;
        // bonus = Convert.ToInt32(bonus_d) * 10;
        bonus = 10;

        //Teste
        // Informacoes.SetQuantidade5050(2);
        //  Informacoes.SetQuantidadePular(1);

        int pontuacao_aux = pontuacao;

        bonus_5050 = Informacoes.GetQuantidade5050() * bonus;
        bonus_pular = Informacoes.GetQuantidadePular() * bonus;


           
        x5050.text = "Bônus por não usar a ajuda 5050: + " + bonus_5050.ToString() + " pontos!";
        pontuacao = pontuacao + bonus_5050;


        pular.text = "Bônus por não usar a ajuda Pular: + " + bonus_pular.ToString() + " pontos!";
        pontuacao = pontuacao + bonus_pular;

        pontos_final.text = "Pontuação Final: " + pontuacao.ToString();
        Informacoes.SetPontos(pontuacao);
        
    }
}
