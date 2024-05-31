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

    double bonus_d;
    int pontuacao;
    int bonus;

    void Start()
    {
        pontuacao = Informacoes.GetPontos();
        CalcularBonus();
        botao.onClick.AddListener(() => Voltar());
    }

    void Voltar(){
		SceneManager.LoadScene("Menu");
	}
    
    private void ColocarPontuacao()
    {
        pontos.text = pontuacao.ToString();
    }

    private void CalcularBonus()
    {
        bonus_d = (Informacoes.GetQuantidadeFacil() + Informacoes.GetQuantidadeMedio() + Informacoes.GetQuantidadeDificil()) * 0.2;
        bonus = Convert.ToInt32(bonus_d) * 10;
        if (Informacoes.GetStatus5050() == 0 || Informacoes.GetStatusPular() == 0)
        {
            bonus_tela.text = "Você desbloqueou conquistas e ganhou bônus na pontuação.\n\n";
            bonus_tela.text += "Bônus acumulados: ";
            if (Informacoes.GetStatus5050() == 0)
            {
                x5050.text = "Não usou a ajuda 5050: + " + bonus.ToString() + " pontos!";
                pontuacao = pontuacao + bonus;
            }
            if (Informacoes.GetStatusPular() == 0)
            {
                pular.text = "Não usou a ajuda Pular: + " + bonus.ToString() + " pontos!";
                pontuacao = pontuacao + bonus;
            }
            pontos_final.text = "Pontuação atualizada: " + pontuacao.ToString();
        }
    }
}
