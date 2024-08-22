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

	public AudioSource confirmar;

	public AudioSource naoo;

	public AudioSource simm;

	public AudioSource voltar;

	public Button sim;
	public Button nao;

	public Button play;

	public Button opcoes;

	public Button intrucoes;

	public Button sair;

	public AudioSource playAudio;

	public AudioSource opcoesAudio;

	public AudioSource intrucoesAudio;

	public AudioSource sairAudio;

	public Button voltarBotao;

	public Animator Panel_confirmar_anim;

	void Start()
	{
		volume_efeitos = Informacoes.GetValueEfeitos();
        volume_musica = Informacoes.GetValueEfeitos();
        volume_texto = Informacoes.GetValueLeituraTexto();
        Informacoes.SetNivel(0);
    	fadein = false;
		Informacoes.SetCursosBlock(0);
		fadein = false;
		AttVolume();
		
		//Informacoes.SetCursosBlock(2);
	}

	void AttVolume(){
	confirmar.volume = volume_texto;
		naoo.volume = volume_texto;
		simm.volume = volume_texto;
		voltar.volume = volume_texto;
		playAudio.volume = volume_texto;
		opcoesAudio.volume = volume_texto;
		intrucoesAudio.volume = volume_texto;
		sairAudio.volume = volume_texto;
		confirmar.Stop();
		naoo.Stop();
		simm.Stop();
		voltar.Stop();
		playAudio.Stop();
		opcoesAudio.Stop();
		intrucoesAudio.Stop();
		sairAudio.Stop();
	}

	public void DesativarBotoes(){
		play.interactable = false;
		opcoes.interactable = false;
		intrucoes.interactable = false;
		sair.interactable = false;
		playAudio.enabled = false;
		opcoesAudio.enabled = false;
		intrucoesAudio.enabled = false;
		sairAudio.enabled = false;
		//voltarBotao.interactable = false;

	}

	public void AtivarBotoes(){
		play.interactable = true;
		opcoes.interactable = true;
		intrucoes.interactable = true;
		sair.interactable = true;
		playAudio.enabled = true;
		opcoesAudio.enabled = true;
		intrucoesAudio.enabled = true;
		sairAudio.enabled = true;
	
		//voltarBotao.interactable = true;
	}


	public void Sair()
	{

		Application.Quit();
	}
	
	public void MudarTela(string tela)
	{
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

	public void IniciarTutorial()
	{
		SceneManager.LoadScene("Tutorial");
        fadein = true;
        
    }

	
    public void MostrarPainelConfirmar()
    {
        Panel_confirmar_anim.SetBool("showPanel", true);
        confirmar.Play();
        sim.Select();
		DesativarBotoes();
    }

	public void SelecionarCursor(int botao)
	{
		if(botao == 0)
		{
			Informacoes.SetCursosBlock(1);
		}else if(botao == 1)
		{
			Informacoes.SetCursosBlock(2);
		}else if(botao == 2)
		{
			Informacoes.SetCursosBlock(0);
		}

		fadein = true;
		imagem.gameObject.SetActive(true);
		FecharPainelCursor();
	}

	public void FecharPainelCursor()
	{
		Panel_confirmar_anim.SetBool("showPanel", false);
		confirmar.Stop();
		play.Select();
		AtivarBotoes();
		
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
				SceneManager.LoadScene("Narrativa");
			}
		}
	}
}