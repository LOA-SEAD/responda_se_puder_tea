using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class Instrucoes : MonoBehaviour
{
    public bool FindFirstSelectableInstrucoes = false;

    public TextMeshProUGUI subtitulo;
    public TextMeshProUGUI paginacao;
    public TextMeshProUGUI texto_instrucoes;
    public Button botao_esquerda;
    public Button botao_direita;
    public Button voltar;
    public Text voltarTexto;
    public GameObject bolinha_esquerda;
    public GameObject bolinha_direita;
    public AudioSource adSource;
    public AudioClip[] adClips;

    int pag;
    int origem;

    void Start()
    {
        #if UNITY_ANDROID
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            //Screen.fullScreen = false;
        #endif
        origem = Informacoes.GetOrigem();
        if(origem == 0){
            voltarTexto.text = "Menu";
        }
        else if(origem == 1){
            voltarTexto.text = "Opções";
        }
        pag = 1;
        AtualizaInstrucoes(pag);
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

    public void AtualizaInstrucoes(int novapag){
        botao_esquerda.interactable = true;
        botao_direita.interactable = true;
        bolinha_esquerda.SetActive(true);
        bolinha_direita.SetActive(true);
        if (novapag == 1){
            Pagina1();
            botao_esquerda.interactable = false;
            bolinha_esquerda.SetActive(false);
        }
        else if (novapag == 2){
            Pagina2();
        }
        else if (novapag == 3){
            Pagina3();
        }
        else if (novapag == 4){
            Pagina4();
            botao_direita.interactable = false;
            bolinha_direita.SetActive(false);
        }
    }

    public void PagSeguinte()
    {
        if (pag <= 3){
            AtualizaInstrucoes(++pag);
        }
    }

    public void PagAnterior()
    {
        if (pag >= 2){
            AtualizaInstrucoes(--pag);
        }
    }

    public void Pagina1()
    {
      
        adSource.clip = adClips[0];
        adSource.Play();
        paginacao.text = "1/4";
        subtitulo.text = "Regras do jogo";
        texto_instrucoes.text = "Responda à pergunta selecionando a alternativa correta.\n\nSempre que selecionar uma alternativa você deverá confirmar a escolha.\n\n";
        texto_instrucoes.text += "Você terá 3 tipos de ajuda caso tenha dificuldade em achar a alternativa correta.\n\n";
        texto_instrucoes.text += "O jogo é composto por 3 níveis: fácil, médio e difícil.";
    }

    public void Pagina2()
    {
        adSource.clip = adClips[1];
        adSource.Play();
        paginacao.text = "2/4";
        subtitulo.text = "Ajuda";
        texto_instrucoes.text = "Dica - Fornece uma pista para encontrar a resposta certa. Você poderá utilizar essa ajuda quantas vezes quiser.\n\n";
        texto_instrucoes.text += "50 50 - Elimina metade das alternativas possíveis. Você só poderá utilizar essa ajuda uma vez, use com cuidado.\n\n";
        texto_instrucoes.text += "Pular - A questão atual será substituida por outra. O Pular poderá ser utilizado uma única vez.";

    }

    public void Pagina3()
    {
        adSource.clip = adClips[2];
        adSource.Play();
        paginacao.text = "3/4";
        subtitulo.text = "Pontuação final";
        texto_instrucoes.text = "A cada questão certa, você ganhará 10 pontos. Caso a resposta esteja errada, você não ganhará nenhum ponto.\n\n";
        texto_instrucoes.text += "Caso chegue ao fim do jogo sem utilizar as ajudas 50 50 ou Pular, você ganhará um bônus para cada ajuda não utilizada";

    }

    public void Pagina4()
    {
        adSource.clip = adClips[3];
        adSource.Play();
        paginacao.text = "4/4";
        subtitulo.text = "Navegação pelo teclado (Acessibilidade)";
        texto_instrucoes.text = "Durante o jogo, você poderá utilizar a tecla TAB para navegar entre os blocos da tela de jogo.\n\n";
        texto_instrucoes.text += "Sendo eles pergunta, alternativas e ajudas.\n\n";
        texto_instrucoes.text += "Para acessar as opções aperte a tecla ESC, e para selecionar qualquer item interativo, pressione a tecla ENTER";
    }
}
