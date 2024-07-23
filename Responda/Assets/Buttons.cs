using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {
	float volume_efeitos, volume_musica, volume_texto;

	int origem;

	public Image imagem;
	public CanvasGroup canvasGroup;

	private bool fadein;

	void Start()
	{
		volume_efeitos = Informacoes.GetValueEfeitos();
        volume_musica = Informacoes.GetValueEfeitos();
        volume_texto = Informacoes.GetValueLeituraTexto();
        Informacoes.SetNivel(0);
    	fadein = false;
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
		//SceneManager.LoadScene("Narrativa");
		fadein = true;
		imagem.gameObject.SetActive(true);
		
		
		//SceneManager.LoadScene("Jogo");
		//SceneManager.LoadScene("Nivel");
	}

	private void Update()
	{
		if(fadein)
		{
			canvasGroup.alpha += Time.deltaTime * 0.5f;
			if(canvasGroup.alpha >= 1)
			{
				//SceneManager.LoadScene("Narrativa");
				Informacoes.SetStatus(0);
				Informacoes.SetNivel(0);
				SceneManager.LoadScene("Nivel");
			}
		}
	}
}