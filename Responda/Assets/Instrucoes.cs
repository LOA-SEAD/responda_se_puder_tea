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

    public AudioSource regrass;
    public AudioSource ajudas;
    public AudioSource pontuacaos;
    public AudioSource navegacaos;
    public AudioSource creditoss;
    public AudioSource tutorials;

    public AudioSource menu;
    public AudioClip[] adClips;

    // Sumario Buttons

    public Button regras;
    public Button ajuda;
    public Button pontuacao;

    public Button navegacao;
    public Button creditos;

    public Button tutorial;

    // Sumario Texts

    public Text regrasText;
    public Text ajudaText;
    public Text pontuacaoText;
    public Text navegacaoText;
    public Text creditosText;
    public Text tutorialText;

    int pag;
    int origem;

    int posicao;


    void AttAudios(){
        regrass.volume = Informacoes.GetValueLeituraTexto();
        ajudas.volume = Informacoes.GetValueLeituraTexto();
        pontuacaos.volume = Informacoes.GetValueLeituraTexto();
        navegacaos.volume = Informacoes.GetValueLeituraTexto();
        creditoss.volume = Informacoes.GetValueLeituraTexto();
        menu.volume = Informacoes.GetValueLeituraTexto();
        regrass.Stop();
        ajudas.Stop();
        pontuacaos.Stop();
        navegacaos.Stop();
        creditoss.Stop();
        menu.Stop();
    }

    void Start()
    {
        #if UNITY_ANDROID
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            //Screen.fullScreen = false;
        #endif
        origem = Informacoes.GetOrigem();
        if(origem == 0){
            voltarTexto.text = "Voltar";
        }
        else if(origem == 1){
            voltarTexto.text = "Voltar";
        }
        Informacoes.setTelaChamou("Instrucoes");
        pag = 0;
        posicao = 0;
        AtualizaInstrucoes(pag);
        AttAudios();
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

        Sumario();
    }

    public void AtualizaInstrucoes(int novapag){
        botao_esquerda.interactable = true;
        botao_direita.interactable = true;
        bolinha_esquerda.SetActive(true);
        bolinha_direita.SetActive(true);
        if(novapag == 0){
            Pagina0();
            posicao = 0;
            botao_esquerda.interactable = false;
            bolinha_esquerda.SetActive(false);
        }
        else if (novapag == 1){
            Pagina1();
        }
        else if (novapag == 2){
            Pagina2();
        }
        else if (novapag == 3){
            Pagina3();
        }
        else if (novapag == 4){
            Pagina4();
        }
        else if ( novapag == 5){
            Pagina5();
            botao_direita.interactable = false;
            bolinha_direita.SetActive(false);
        }
    }

    public void PagSeguinte()
    {
        if (pag <= 4){
            AtualizaInstrucoes(++pag);
        }
    }

    public void PagAnterior()
    {
        if (pag >= 1){
            AtualizaInstrucoes(--pag);
        }
    }

    private void Sumario(){
        if(pag == 0){
            regras.enabled = true;
            ajuda.enabled = true;
            pontuacao.enabled = true;
            navegacao.enabled = true;
            creditos.enabled = true;
            tutorial.enabled = true;
            regras.gameObject.SetActive(true);
            ajuda.gameObject.SetActive(true);
            pontuacao.gameObject.SetActive(true);
            navegacao.gameObject.SetActive(true);
            creditos.gameObject.SetActive(true);
            tutorial.gameObject.SetActive(true);

            regrasText.text = "1. Regras do Jogo";
            ajudaText.text = "2. Ajuda";
            pontuacaoText.text = "3. Pontuação Final";
            navegacaoText.text = "4. Navegação pelo Teclado ( Acessibilidade )";
            creditosText.text = "5. Créditos";
            tutorialText.text = "6. Tutorial";
        }else{
            regras.enabled = false;
            ajuda.enabled = false;
            pontuacao.enabled = false;
            navegacao.enabled = false;
            creditos.enabled = false;
            tutorial.enabled = false;

            regras.gameObject.SetActive(false);
            ajuda.gameObject.SetActive(false);
            pontuacao.gameObject.SetActive(false);
            navegacao.gameObject.SetActive(false);
            creditos.gameObject.SetActive(false);
            tutorial.gameObject.SetActive (false);

            regrasText.text = "";
            ajudaText.text = "";
            pontuacaoText.text = "";
            navegacaoText.text = "";
            creditosText.text = "";
            tutorialText.text = "";
        }
    }

    // Funções do Indice

    public void Regras(){
        pag = 1;
        AtualizaInstrucoes(pag);
    }

    public void Ajuda(){
        pag = 2;
        AtualizaInstrucoes(pag);
    }

    public void Pontuacao(){
        pag = 3;
        AtualizaInstrucoes(pag);
    }

    public void Navegacao(){
        pag = 4;
        AtualizaInstrucoes(pag);
    }

    public void Creditos(){
        pag = 5;
        AtualizaInstrucoes(pag);
    }
    public void Tutorial()
    {
        pag = 6;
        SceneManager.LoadScene("Tutorial");
    }


    // Páginas 

    public void Pagina0(){
        adSource.clip = adClips[0];
        adSource.volume = Informacoes.GetValueLeituraTexto();
        adSource.Play();
        AttAudios();

        paginacao.text = "0/5";
        subtitulo.text = "Índice";
        texto_instrucoes.text = "";
        regras.Select();
        regrass.Play();

    }

    public void Pagina1()
    {
      
        adSource.clip = adClips[1];
        adSource.volume = Informacoes.GetValueLeituraTexto();
        adSource.Play();
        AttAudios();
        paginacao.text = "1/5";
        subtitulo.text = "Regras do jogo";
        texto_instrucoes.text = "Responda à pergunta selecionando a alternativa correta.Após selecionada, confirme sua escolha. \n\n";
        texto_instrucoes.text += "A cada resposta certa, você ganhará os pontos referentes ao nível. Nível Fácil será 10, Médio 15 e Difícil 20.\n\n";
        texto_instrucoes.text += "Você terá 3 tipos de ajuda caso tenha dificuldade em achar a solução.\n\n";
        texto_instrucoes.text += "Além disso, haverá bônus por acetos consecutivos e ajudas não usadas.";

    }

    public void Pagina2()
    {
        adSource.clip = adClips[2];
        AttAudios();
        adSource.volume = Informacoes.GetValueLeituraTexto();
        adSource.Play();
        paginacao.text = "2/5";
        subtitulo.text = "Ajuda";
        texto_instrucoes.text = "Haverá 3 tipos de ajuda, caso tenha dificuldade em achar a solução.\n\n";
        texto_instrucoes.text += "Dica - Fornece uma pista para encontrar a resposta certa. Fique a vontade para utilizar essa ajuda quantas vezes quiser, sem qualquer custo. \n\n";
        texto_instrucoes.text += "50 50 - Elimina metade das alternativas possíveis. Você só poderá utilizar essa ajuda 2 vezes, use com cuidado.\n\n";
        texto_instrucoes.text += "Pular - A questão atual será substituida por outra. O Pular poderá ser utilizado uma única vez.";


    }

    public void Pagina3()
    {
        adSource.clip = adClips[3];
        AttAudios();
        adSource.volume = Informacoes.GetValueLeituraTexto();
        adSource.Play();
        paginacao.text = "3/5";
        subtitulo.text = "Pontuação final";
        texto_instrucoes.text = "A cada questão certa, você ganhará os pontos referente ao nível. Nível Fácil será 10, Médio 15 e Difícil 20.\n\n";
        texto_instrucoes.text += "A cada acerto consecutivo receberá 5 pontos extras. Ao errar uma questão, deixará de ganhar o bônus\n";
        texto_instrucoes.text += "Caso volte a acertar, é possível voltar a ganhar a pontuação extra.\n";
        texto_instrucoes.text += "Caso a resposta esteja errada, você não ganhará nenhum ponto.\n";
        texto_instrucoes.text += "Caso chegue ao fim do jogo sem utilizar as ajudas 50 50 ou Pular, você ganhará um bônus para cada ajuda não utilizada.";


    }

    public void Pagina4()
    {
        adSource.clip = adClips[4];
        AttAudios();
        adSource.volume = Informacoes.GetValueLeituraTexto();
        adSource.Play();
        paginacao.text = "4/5";
        subtitulo.text = "Navegação pelo teclado (Acessibilidade)";
        texto_instrucoes.text = "Durante o jogo, você poderá utilizar a tecla TAB para navegar entre os 3 blocos da tela de jogo: pergunta, alternativas e ajudas.\n\n";
        texto_instrucoes.text += "Para navegar entre as alternativas ou entre as ajudas, use as setas direcionais.\n\n";
        texto_instrucoes.text += "Para acessar as opções, aperte a tecla ESC, e para selecionar qualquer item interativo, pressione a tecla ENTER.\n\n";
        texto_instrucoes.text += "Caso perca a navegação, pressione a tecla TAB para voltar à navegação pelo teclado.";

    }

    public void Pagina5(){
        adSource.clip = adClips[5];
        AttAudios();
        adSource.volume = Informacoes.GetValueLeituraTexto();
        adSource.Play();
        paginacao.text = "5/5";
        subtitulo.text = "Créditos";
        texto_instrucoes.text = "Desenvolvido por:\n\n";
        texto_instrucoes.text += "Gabriel Lourenço de Paula Graton\n";
        texto_instrucoes.text += "Leandro Keller Salto\n";
        texto_instrucoes.text += "Rhana Omolayo Pinheiro Oresotu\n";
        texto_instrucoes.text += "Maria Clara Silva Azevedo\n";

    }
}
