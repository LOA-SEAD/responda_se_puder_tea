using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Networking;
public class Final : MonoBehaviour
{
    public Image comeco;

    public Image tiposFinal;
    public Sprite[] spritearray;

    public CanvasGroup canvasGroup;

    public CanvasGroup canvasGroupCarta;
    public bool fadeOut;

    public bool fadein;

    private int final;

    public AudioClip[] falas;
    public AudioSource fala;

    public AudioClip[] efeitos;
    public AudioSource efeito;

    private String[] roteiro;
    public Text texto;

    private string roteiro_aux;
    public Image UI;

    public Sprite[] UIs;

    private String[] nomes;
    public Text nome;

    private int carrega;
    public int num_texto;

    public float velocidade;
    public bool fim;

    public float teste;

    public int pontuacao;

    public int pontuacao_total;

    public int intervalos;

    // Start is called before the first frame update

    private void AtualizarAudios(AudioSource p){
        efeito.volume = Informacoes.GetValueEfeitos();
        fala.volume = Informacoes.GetValueLeituraTexto();
    }
    void Start()
    {
        canvasGroup.alpha = 1;
        fadeOut = true;
        pontuacao = Informacoes.GetPontos();
        Comecar();
        PontuacaoIntervalos();

    }

    public int quantidadeFacil = Informacoes.GetQuantidadeFacil();
    public int quantidadeMedio = Informacoes.GetQuantidadeMedio();
    public int quantidadeDificil = Informacoes.GetQuantidadeDificil();
    public int quantidade5050 = Informacoes.GetQuantidade5050();
    public int quantidadePular = Informacoes.GetQuantidadePular();
    public int quantidadeAcertos = Informacoes.GetQuantidadeAcertos();

    void PontuacaoIntervalos(){

        pontuacao_total = 0;

        pontuacao_total = 10*Informacoes.GetQuantidadeFacil() + 15*Informacoes.GetQuantidadeMedio() + 20*Informacoes.GetQuantidadeDificil();
        pontuacao_total += 10*Informacoes.GetQuantidade5050() + 10*Informacoes.GetQuantidadePular() + 5*Informacoes.GetQuantidadeAcertos();

        intervalos = pontuacao_total/4;

        canvasGroupCarta.alpha = 1;

        if(pontuacao <= intervalos){
            final = 0;
        }
        if(pontuacao > intervalos && pontuacao <= 2*intervalos){
            final = 1;
        }
        if(pontuacao > 2*intervalos && pontuacao <= 3*intervalos){
            final = 2;
        }
        if(pontuacao > 3*intervalos){
            final = 3;
        }

        if (final == 0)
        {
            tiposFinal.sprite = spritearray[0];
        }
        else if (final == 1)
        {
            tiposFinal.sprite = spritearray[1];
        }
        else if (final == 2)
        {
            tiposFinal.sprite = spritearray[2];
        }
        else if (final == 3)
        {
            tiposFinal.sprite = spritearray[3];
        }

    }

    void Comecar(){
        CarregarTexto();
        carrega = 0;
        fim = false;
        roteiro_aux = roteiro[num_texto++];
        texto.text = "";
    }
    public void Pular(){
        comeco.gameObject.SetActive(true);
		fadein = true;

    }

    public void Prosseguir(){

        if(fim == true){
            roteiro_aux = roteiro[num_texto++];
            texto.text = "";
            carrega = 0;
            fim = false;
        }else{
            roteiro_aux = roteiro[num_texto-1];
            texto.text = roteiro_aux;
            carrega = roteiro_aux.Length;
            fim = true;
            if(num_texto == 8){

                comeco.gameObject.SetActive(true);
                fadein = true;
            }
            velocidade = 0;
        }
    }

    void Cartas(){



        if(final == 0){
            roteiro[1] = "Só queria mandar um super obrigado por toda a ajuda para eu conseguir morar na garagem. Vocês foram demais!";
            roteiro[2] = "Sem a orientação e ajuda do meu amigo e de vocês, eu provavelmente ainda estaria procurando um lugar adequado.";
            roteiro[3] = "Estou adorando o novo espaço. É perfeito para recarregar minhas baterias (literalmente, haha).";
        }else if(final == 1){
            roteiro[1] = "Queria agradecer muito por toda a ajuda com a atualização das minhas peças, elas brilham";
            roteiro[2] = "Você e meu amigo realmente fizeram a diferença! Sem vocês, eu não teria conseguido fazer essa melhoria.";
            roteiro[3] = "Estou adorando as novas peças. Eu tô muito lindo!";
        }else if(final == 2){
            roteiro[1] = "Super obrigado por toda a ajuda com a atualização das minhas peças, agora deu até vontade de ir na academia";
            roteiro[2] = "Vocês foram demais! Agora, com meu novo visual e corpo bombado, me sinto muito mais eficiente e estiloso.";
            roteiro[3] = "Posso simplesmente levantar todo esse peso de ser um robô moderno e lindo!";
        }else if(final == 3){
            roteiro[1] = "Queria te mandar um alô e um super obrigado diretamente da praia!";
            roteiro[2] = "Agora consigo curtir o vento com meus cabelos de metal e minha nova aparência. Curti demais";
            roteiro[3] = "Vou lá surfar nessas ondas da internet. Valeu por tudo!";
        }

    }

    void CarregarTexto(){
        roteiro = new String[8];
        texto.text = "";
        num_texto = 0;
        roteiro[0] = "Agora vamos ver como nosso último participante está hoje em dia… Ele nos mandou uma foto com uma carta. Vamos apresenta-la a vocês";
        Cartas();
        roteiro[4] = "Obrigado por Tudo !";
        roteiro[5] = "Uau, olha só… Ele está diferente..";
        roteiro[6] = "...";
        roteiro[7] = "Ás vezes me emociono… Dessa vez vamos prosseguir para a próxima pergunta para encontramos nosso próximo jogador… Qual a...";

    }
    // Update is called once per frame

    void CarregaTextoNaTela(){

        velocidade += Time.deltaTime*10;
        if(velocidade > 0.5){
            texto.text += roteiro_aux[carrega];
            carrega++;
            velocidade = 0;

            if(carrega == roteiro_aux.Length){
                fim = true;

                if(num_texto == 8){
                    comeco.gameObject.SetActive(true);
                    fadein = true;
                }
                
            }

        }
    }
    void Update()
    {
        if(fim == false){
            CarregaTextoNaTela();
        }
        
        if (fadeOut)
        {
            canvasGroup.alpha = canvasGroup.alpha - Time.deltaTime;
        }

        if (canvasGroup.alpha <= 0 && fadeOut)
        {
            fadeOut = false;
            canvasGroup.alpha = 0;

        }

        if(fadein)
        {
            canvasGroup.alpha += Time.deltaTime;
        }

        if (canvasGroup.alpha >= 1)
        {
            fadein = false;
            SceneManager.LoadScene("Menu");

        }

        teste = canvasGroup.alpha;

        if(num_texto > 1 && num_texto < 6){
            nome.text = "Robs";
        }else{
            nome.text = "Apresentador";
        }

        if (Input.GetKeyDown(KeyCode.Return)) {
            Prosseguir();
        }
    }   
}
