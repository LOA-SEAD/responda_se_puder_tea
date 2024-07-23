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

    // Sumario Buttons

    public Button regras;
    public Button ajuda;
    public Button pontuacao;

    public Button navegacao;
    public Button creditos;

    // Sumario Texts

    public Text regrasText;
    public Text ajudaText;
    public Text pontuacaoText;
    public Text navegacaoText;
    public Text creditosText;

    int pag;
    int origem;

    int posicao;

    // Controle do Selectable
    void Controle(){

        if(posicao == 0){
            regras.Select();
        }
        else if(posicao == 1){
            ajuda.Select();
        }
        else if(posicao == 2){
            pontuacao.Select();
        }
        else if(posicao == 3){
            navegacao.Select();
        }
        else if(posicao == 4){
            creditos.Select();
        }

    }

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

        pag = 0;
        posicao = 0;
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

        // Para cima
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            if(posicao > 0){
                posicao--;
            }else{
                posicao = 4;
            }
        }

        // Para baixo
        else if(Input.GetKeyDown(KeyCode.DownArrow)){
            if(posicao < 4){
                posicao++;
            }else{
                posicao = 0;
            }
        }

        Sumario();
        Controle();
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

            regrasText.text = "1. Regras";
            ajudaText.text = "2. Ajuda";
            pontuacaoText.text = "3. Pontuação";
            navegacaoText.text = "4. Navegação";
            creditosText.text = "5. Créditos";
        }else{
            regras.enabled = false;
            ajuda.enabled = false;
            pontuacao.enabled = false;
            navegacao.enabled = false;
            creditos.enabled = false;

            regrasText.text = "";
            ajudaText.text = "";
            pontuacaoText.text = "";
            navegacaoText.text = "";
            creditosText.text = "";
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

    // Páginas 

    public void Pagina0(){
        adSource.clip = adClips[0];
        adSource.Play();
        adSource.Stop();
        paginacao.text = "0/5";
        subtitulo.text = "Índice";
        texto_instrucoes.text = "";
        

    }

    public void Pagina1()
    {
      
        adSource.clip = adClips[0];
        adSource.Play();
        paginacao.text = "1/5";
        subtitulo.text = "Regras do jogo";
        texto_instrucoes.text = "Responda à pergunta selecionando a alternativa correta.\n\nSempre que selecionar uma alternativa você deverá confirmar a escolha.\n\n";
        texto_instrucoes.text += "Você terá 3 tipos de ajuda caso tenha dificuldade em achar a resposta.\n\n";
        texto_instrucoes.text += "O jogo é composto por 3 níveis: fácil, médio e difícil.";

    }

    public void Pagina2()
    {
        adSource.clip = adClips[1];
        adSource.Play();
        paginacao.text = "2/5";
        subtitulo.text = "Ajuda";
        texto_instrucoes.text = "Dica - Fornece uma pista para encontrar a resposta certa. Você poderá utilizar essa ajuda quantas vezes quiser.\n\n";
        texto_instrucoes.text += "50 50 - Elimina metade das alternativas possíveis. Você só poderá utilizar essa ajuda 2 vezes, use com cuidado.\n\n";
        texto_instrucoes.text += "Pular - A questão atual será substituida por outra. O Pular poderá ser utilizado uma única vez.";


    }

    public void Pagina3()
    {
        adSource.clip = adClips[2];
        adSource.Play();
        paginacao.text = "3/5";
        subtitulo.text = "Pontuação final";
        texto_instrucoes.text = "A cada questão certa, você ganhará os pontos referente ao nível. Nível Fácil será 10, Médio 15 e Difícil 20.\n";
        texto_instrucoes.text += "Acertando consecutivamente irá receber o bônus de 5 pontos, perdendo caso erre a questão, podendo retoma-lo depois. \n";
        texto_instrucoes.text += "Caso a resposta esteja errada, você não ganhará nenhum ponto.\n";
        texto_instrucoes.text += "Caso chegue ao fim do jogo sem utilizar as ajudas 50 50 ou Pular, você ganhará um bônus para cada ajuda não utilizada";


    }

    public void Pagina4()
    {
        adSource.clip = adClips[3];
        adSource.Play();
        paginacao.text = "4/5";
        subtitulo.text = "Navegação pelo teclado (Acessibilidade)";
        texto_instrucoes.text = "Durante o jogo, você poderá utilizar a tecla TAB para navegar entre os blocos da tela de jogo.\n\n";
        texto_instrucoes.text += "Sendo eles pergunta, alternativas e ajudas.\n\n";
        texto_instrucoes.text += "Para acessar as opções aperte a tecla ESC, e para selecionar qualquer item interativo, pressione a tecla ENTER\n\n";
        texto_instrucoes.text += "Caso perca a navegação pelo teclado, pressione a tecla TAB para voltar a navegação.";

    }

    public void Pagina5(){
        adSource.clip = adClips[4];
        adSource.Play();
        adSource.Stop();
        paginacao.text = "5/5";
        subtitulo.text = "Créditos";
        texto_instrucoes.text = "Desenvolvido por:\n\n";
        texto_instrucoes.text += "Gabriel Lourenço de Paula Graton\n";
        texto_instrucoes.text += "Leandro Keller Salto\n";
        texto_instrucoes.text += "Rhana Omolayo Pinheiro Oresotu\n";
        texto_instrucoes.text += "Maria Clara Silva Azevedo\n";

    }
}
