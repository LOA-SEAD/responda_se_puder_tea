using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {
	float volume_efeitos, volume_musica, volume_texto;

	int origem;

	void Start()
	{
		volume_efeitos = Informacoes.GetValueEfeitos();
        volume_musica = Informacoes.GetValueEfeitos();
        volume_texto = Informacoes.GetValueLeituraTexto();
        Informacoes.SetNivel(0);
    }

	public void Sair()
	{

		Application.Quit();
	}
	
	public void MudarTela(string tela)
	{
		if(tela=="JogoTutorial")
		{
			tela = "Nivel";
			Informacoes.SetNivel(99);
		}
		SceneManager.LoadScene(tela);
	}

	public void MudarTelaOrigem()
	{
		origem = Informacoes.GetOrigem();
		if (origem == 0)
		{
			SceneManager.LoadScene("Menu");
		}
		else if(origem == 1)
		{
			SceneManager.LoadScene("Opcoes");
		}
	}

	public void IniciarJogo()
	{
		
		SceneManager.LoadScene("Nivel");
	}
}