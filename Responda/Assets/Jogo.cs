using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Networking;
using System.Linq;
using UnityEngine.EventSystems;


public class Jogo : MonoBehaviour
{
    private Rect janelaDica = new Rect (0, 0, Screen.width, Screen.height);
    private bool showJanelaDica = false;

    private Rect janelaCerto = new Rect (0, 0, Screen.width, Screen.height);
    private bool showCerto = false;

    private Rect janelaErrado = new Rect (0, 0, Screen.width, Screen.height);
    private bool showErrado = false;

    // Adicionado Bonus
    public bool Acerto_Consecutivo = false;

    int quantidade_acertos = 0;

    public Text pergunta_tela;
    public Text alternativa1_tela;
    public Text alternativa2_tela;
    public Text alternativa3_tela;
    public Text alternativa4_tela;
    public Text pontuacao_tela;
    public Text valorQuestao;
    public Text dificuldade_tela;
    public Text numero_questao_tela;
    public TextMeshProUGUI panel_title;
    public TextMeshProUGUI panel_text;
    public Button botao_panel;
    public Button botao_panel_sim;
    public Button[] alternativas = new Button[4];
    public Button botao_pergunta;
    public Button confirmar;
    public Button ajuda5050;

    public Button opcoes;

    public Text ajuda5050_tela;
    public Button dica;
    public Button pular;

    public Text pular_tela;

    public GameObject[] barra_facil = new GameObject[17];
    public GameObject[] barra_medio = new GameObject[17];
    public GameObject[] barra_dificil = new GameObject[17];

    public GameObject[] risco = new GameObject[4];

    public TextAsset arquivo_texto;

    public Animator Panel_anim;
    public Animator Panel_confirmar_anim;

    const int SIM = 1;
    const int NAO = 0;
    const int FACIL = 0;
    const int MEDIO = 1;
    const int DIFICIL = 2;
    const int INICIO = 0;
    const int MEIO = 1;
    const int STATUSOPCOES = 2;
    const int PROXPALAVRA = 3;
    const int MUDOU_NIVEL = 1;
    const int NAO_MUDOU_NIVEL = 0;

    const int JANELA = 1;
    const int JOGANDO = 0;

    public int quantidade_facil;
    public int quantidade_medio;
    public int quantidade_dificil;
    string[] perguntas_facil = new string[11];
    string[] perguntas_medio = new string[11];
    string[] perguntas_dificil = new string[11];
    public int[] respostas_facil = new int[11];
    public int[] respostas_medio = new int[11];
    public int[] respostas_dificil = new int[11];
    string[,] respostas_possiveis_facil = new string[11, 4];
    string[,] respostas_possiveis_medio = new string[11, 4];
    string[,] respostas_possiveis_dificil = new string[11, 4];
    string[] dicas_facil = new string[11];
    string[] dicas_medio = new string[11];
    string[] dicas_dificil = new string[11];
    string[] audios_perguntas = new string[33];
    string[,] audios_alternativas = new string[33, 4];
    string[] audios_dicas = new string[33];

    int alternativa_correta;
    int pontos_ganhos;
    int alternativa_escolhida;
    int tirar_1;
    int tirar_2;
    public int pular_agora;

    public int questao_x_de_y;
    public int selecionou5050 = NAO;

    public int quantidade_5050 = 2;

    public Text quantidade_5050_tela;
    int quantidade_pular = 1;

    public Text quantidade_pular_tela;
    public int selecionou_pular = 0;
    public int nivel_atual = FACIL;
    int pontuacao;

    int estado;

    int Selecionado;

    float volume_efeitos, volume_musica, volume_texto;

    string txt_audio_pergunta;
    string txt_audio_a0;
    string txt_audio_a1;
    string txt_audio_a2;
    string txt_audio_a3;
    string txt_audio_dica;

    public AudioSource audio_pergunta;
    public AudioSource audio_a0;
    public AudioSource audio_a1;
    public AudioSource audio_a2;
    public AudioSource audio_a3;
    public AudioSource audio_dica;
    public AudioSource audio_transicoes;

    public AudioSource audio_botao_dica;
    public AudioSource audio_5050;
    public AudioSource audio_pular;

    public AudioSource audio_confirmar;

    public AudioSource audio_opcoes;
    public AudioSource audio_nao;

    public AudioSource audio_sim;
    
    System.Random random = new System.Random();

    //Utilizados para o double touch
    bool one_click = false;
    bool timer_running;
    float timer_for_double_click;

    float delay = 5.0f;
    
    // Declaracao de Variaveis
    public Image[] images;
    //public Shader[] shaders;

    public Sprite[] sprites_variacoes;

    public Image[] ajudas;

    public Sprite[] sprites_ajudas;

    public int pergunta_atual;

    public int[] perguntas_bool;
    public int[] ajudas_usadas;

    public int caminhos;


    void Start()
    {
        Debug.Log("[Jogo] Start - Inicio");
        CarregaDados.Load(this);
        Debug.Log("[Jogo] Quantidade itens: " + CarregaDados.listaDados.Count);
        Debug.Log("[Jogo] Start - Fim");
        pergunta_atual = 0;

        //pergunta_tela.color = new Color(0.427451f, 0.427451f, 0.427451f, 1);
    
        
        #if UNITY_ANDROID
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            //Screen.fullScreen = false;
        #endif
        //TirarBarras();

        caminhos = Informacoes.GetCaminhos();

        if (Informacoes.GetStatus() == INICIO)
        {
            InicializarJogo();
        }
        else
        {
            PegarInfosSalvas();
            PegarProximaQuestao();
            if(Informacoes.GetStatus() == STATUSOPCOES){
                questao_x_de_y--;
            }

        }
         Debug.Log("qst" + questao_x_de_y);
          Debug.Log("nvl" + nivel_atual);

         if (VerificarFim() == 0){
            AtualizarPerguntaTela();
        }

        if(selecionou5050 == SIM){

            alternativas[tirar_1].interactable = false;
            alternativas[tirar_2].interactable = false;
            ajuda5050.interactable = false;

            if(tirar_1 == 0){
                alternativa1_tela.color =  new Color(0.427451f, 0.427451f, 0.427451f, 1);
            }else if(tirar_1 == 1){
                alternativa2_tela.color =  new Color(0.427451f, 0.427451f, 0.427451f, 1);
            }else if(tirar_1 == 2){
                alternativa3_tela.color =  new Color(0.427451f, 0.427451f, 0.427451f, 1);
            }else{
                alternativa4_tela.color =  new Color(0.427451f, 0.427451f, 0.427451f, 1);
            }
            
            if(tirar_2 == 0){
                alternativa1_tela.color =  new Color(0.427451f, 0.427451f, 0.427451f, 1);
            }else if(tirar_2 == 1){
                alternativa2_tela.color =  new Color(0.427451f, 0.427451f, 0.427451f, 1);
            }else if(tirar_2 == 2){
                alternativa3_tela.color =  new Color(0.427451f, 0.427451f, 0.427451f, 1);
            }else{
                alternativa4_tela.color =  new Color(0.427451f, 0.427451f, 0.427451f, 1);
            }

            ajuda5050_tela.color =  new Color(0.427451f, 0.427451f, 0.427451f, 1);
            quantidade_5050_tela.color = new Color(0.5660378f,0.4708605f, 0.2963688f,1);
            
        }
        
        if(pular_agora == SIM){
            //pular.interactable = false;
            pular_tela.color =  new Color(0.427451f, 0.427451f, 0.427451f, 1);
            quantidade_pular_tela.color = new Color(0.5660378f,0.4708605f, 0.2963688f,1);
        }
    }

    void Update()
    {
        AtualizarVolume();
        DesativaBarras();
        AttImagem();
        MostraAtual();

        if(estado == JOGANDO)
        {
            //Esc
            if (Input.GetKeyDown(KeyCode.Escape)){
                AcessarOpcoes();
            }
        }
        else if (estado == JANELA)
        {
            //Esc + Enter
            if (Input.GetKeyDown(KeyCode.Escape) | Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                if(showJanelaDica){
                    showJanelaDica = false;
                }
                estado = JOGANDO;
            }
        }

        if(one_click){
            if((Time.time - timer_for_double_click) > delay){
                one_click = false;
            }
        }

    }

    // Implementação da Barra de Perguntas

    void MostraAtual(){
        pergunta_atual = Informacoes.GetPerguntaAtual();
        images[pergunta_atual].sprite = sprites_variacoes[0];
        quantidade_5050_tela.text = "x" + quantidade_5050.ToString();
        quantidade_pular_tela.text = "x" + quantidade_pular.ToString();
    }

    void AttImagem(){

        for(int j = 0; j < 10; j++){
            if(perguntas_bool[j] == 1){
                images[j].sprite = sprites_variacoes[1];
            }else if(perguntas_bool[j] == 2){
                images[j].sprite = sprites_variacoes[2];
            }else if(perguntas_bool[j] == 0){
                images[j].sprite = sprites_variacoes[4];
            }else if(perguntas_bool[j] == 3){
                images[j].sprite = sprites_variacoes[3];
            }
        }

        for(int j = 0; j < 10; j++){
            if(ajudas_usadas[j] == 1){
                ajudas[j].sprite = sprites_ajudas[1];
            }else if(ajudas_usadas[j] == 2){
                ajudas[j].sprite = sprites_ajudas[2];
            }else if(ajudas_usadas[j] == 3){
                ajudas[j].sprite = sprites_ajudas[3];
            }else if(ajudas_usadas[j] == 0){
                ajudas[j].sprite = sprites_ajudas[0];
            }
        }
            
    }


    void DesativaBarras(){

        if(quantidade_facil != 5){

            for(int i = 3;i >= quantidade_facil; i--){
                //images[i].sprite = sprites_variacoes[3];
                perguntas_bool[i] = 3;
            }
        }

        if(quantidade_medio != 5){
            for(int i = 6;i >= quantidade_medio + 4; i--){
                // images[i].sprite = sprites_variacoes[3];
                perguntas_bool[i] = 3;
            }
        }

        if(quantidade_dificil != 5){
            for(int i = 9;i >= quantidade_dificil + 7; i--){
                // images[i].sprite = sprites_variacoes[3];
                perguntas_bool[i] = 3;
            }
        }

        

    }

    void pausarAudios()
    {
        audio_pergunta.Stop();
        audio_dica.Stop();
        audio_botao_dica.Stop();
        audio_5050.Stop();
        audio_pular.Stop();
        audio_a0.Stop();
        audio_a1.Stop();
        audio_a2.Stop();
        audio_a3.Stop();
        audio_transicoes.Stop();
        audio_confirmar.Stop();
        audio_opcoes.Stop();
        audio_nao.Stop();
        audio_sim.Stop();
        //parar todos os audios tocando
    }

    private void AtualizarVolume()
    {
        audio_transicoes.volume = Informacoes.GetValueLeituraTexto();
        audio_a0.volume = Informacoes.GetValueLeituraTexto();
        audio_a1.volume = Informacoes.GetValueLeituraTexto();
        audio_a2.volume = Informacoes.GetValueLeituraTexto();
        audio_a3.volume = Informacoes.GetValueLeituraTexto();
        audio_dica.volume = Informacoes.GetValueLeituraTexto();
        audio_pergunta.volume = Informacoes.GetValueLeituraTexto();
        audio_pular.volume = Informacoes.GetValueLeituraTexto();
        audio_5050.volume = Informacoes.GetValueLeituraTexto();
        audio_botao_dica.volume = Informacoes.GetValueLeituraTexto();
        audio_confirmar.volume = Informacoes.GetValueLeituraTexto();
        audio_opcoes.volume = Informacoes.GetValueLeituraTexto();
        audio_nao.volume = Informacoes.GetValueLeituraTexto();
    }
    
    private void InicializarJogo()
    {
        // carregaDados.Load();
        dificuldade_tela.text = "NÍVEL FÁCIL";
        pular_agora = NAO;
        quantidade_pular = 1;
        quantidade_5050 = 2;
        Informacoes.SetStatusPular(NAO);
        pontuacao = 0;
        pergunta_atual = 0;
        Informacoes.SetPerguntaAtual(0);

        if(caminhos == 1){
            Recomecar();
            Informacoes.SetCaminhos(0);
        }else{
            SortearPerguntas();
        }

    }

    private void Recomecar()
    {
        quantidade_facil = Informacoes.GetQuantidadeFacil();
        quantidade_medio = Informacoes.GetQuantidadeMedio();
        quantidade_dificil = Informacoes.GetQuantidadeDificil();
        questao_x_de_y = 0;
        nivel_atual = FACIL;
        perguntas_facil = Informacoes.GetPerguntasFacil();
        respostas_facil = Informacoes.GetRespostasFacil();
        respostas_possiveis_facil = Informacoes.GetRespostasPossiveisFacil();
        dicas_facil = Informacoes.GetDicasFacil();
        perguntas_medio = Informacoes.GetPerguntasMedio();
        respostas_medio = Informacoes.GetRespostasMedio();
        respostas_possiveis_medio = Informacoes.GetRespostasPossiveisMedio();
        dicas_medio = Informacoes.GetDicasMedio();
        perguntas_dificil = Informacoes.GetPerguntasDificil();
        respostas_dificil = Informacoes.GetRespostasDificil();
        respostas_possiveis_dificil = Informacoes.GetRespostasPossiveisDificil();
        dicas_dificil = Informacoes.GetDicasDificil();
        selecionou5050 = 0;
        selecionou_pular = 0;
        Acerto_Consecutivo = false;
        quantidade_acertos = 0;
        audios_perguntas = Informacoes.GetAudiosPerguntas();
        audios_alternativas = Informacoes.GetAudiosAlternativas();
        audios_dicas = Informacoes.GetAudiosDicas();
        perguntas_bool = new int[10];
        ajudas_usadas = new int[10];
        
        MixLista();
        AtualizarVariaveis();
        MisturarRespostas();

    }

    private void SortearPerguntas()
    {
        quantidade_facil = 0;
        quantidade_medio = 0;
        quantidade_dificil = 0;
        Debug.Log("[Jogo] listaDados.Count = " + CarregaDados.listaDados.Count);

        for(int i = 0; i < CarregaDados.listaDados.Count; i++)
        {
            if(CarregaDados.listaDados[i].nivel == "facil"){
                quantidade_facil++;
                CarregaDados.listaDados[i].audio_pergunta = "pf" + i;
                CarregaDados.listaDados[i].audio_dica = "df" + i;
                for(int j = 0; j < 4; j++){
                    CarregaDados.listaDados[i].audio_alternativas[j] = "rf" + i + "_" + j;
                }
            }
            else if(CarregaDados.listaDados[i].nivel == "medio"){
                quantidade_medio++;
                CarregaDados.listaDados[i].audio_pergunta = "pm" + (i - quantidade_facil);
                CarregaDados.listaDados[i].audio_dica = "dm" + (i - quantidade_facil);
                for(int j = 0; j < 4; j++){
                    CarregaDados.listaDados[i].audio_alternativas[j] = "rm" + (i - quantidade_facil) + "_" + j;
                }
            }
            else if(CarregaDados.listaDados[i].nivel == "dificil"){
                quantidade_dificil++;
                CarregaDados.listaDados[i].audio_pergunta = "pd" + (i - quantidade_facil - quantidade_medio);
                CarregaDados.listaDados[i].audio_dica = "dd" + (i - quantidade_facil - quantidade_medio);
                for(int j = 0; j < 4; j++){
                    CarregaDados.listaDados[i].audio_alternativas[j] = "rd" + (i - quantidade_facil - quantidade_medio) + "_" + j;
                }
            }
        //Debug.Log(CarregaDados.listaDados[i].audio_pergunta);    
        }
        
        MixLista();
        AtualizarVariaveis();
        MisturarRespostas();

        quantidade_facil--;
        quantidade_medio--;
        quantidade_dificil--;

        perguntas_bool = new int[10];
        ajudas_usadas = new int[10];


    }

    private void MixLista()
    {
        for(int i = 0; i < quantidade_facil; i++){
            DadosJogo aux = new DadosJogo();
            int limite = quantidade_facil - 1;

            // int randomIndex = random.Next(i, limite); // Gera número entre i e limite - 1
            int randomIndex = GeraNumero(i, limite);

            if(randomIndex != i){
                aux = CarregaDados.listaDados[i];
                CarregaDados.listaDados[i] = CarregaDados.listaDados[randomIndex];
                CarregaDados.listaDados[randomIndex] = aux;
            }
        }
        for(int i = quantidade_facil; i < quantidade_facil + quantidade_medio; i++){
            DadosJogo aux = new DadosJogo();
            int limite = quantidade_facil + quantidade_medio - 1;
            // int randomIndex = random.Next(i, limite); // Gera número entre i e limite - 1
            int randomIndex = GeraNumero(i, limite);
            if(randomIndex != i){
                aux = CarregaDados.listaDados[i];
                CarregaDados.listaDados[i] = CarregaDados.listaDados[randomIndex];
                CarregaDados.listaDados[randomIndex] = aux;
            }
        }
        for(int i = quantidade_facil + quantidade_medio; i < quantidade_facil + quantidade_medio + quantidade_dificil; i++){
            DadosJogo aux = new DadosJogo();
            int limite = quantidade_facil + quantidade_medio + quantidade_dificil - 1;
            // int randomIndex = random.Next(i, limite); // Gera número entre i e limite - 1
            int randomIndex = GeraNumero(i, limite);
            if(randomIndex != i){
                aux = CarregaDados.listaDados[i];
                CarregaDados.listaDados[i] = CarregaDados.listaDados[randomIndex];
                CarregaDados.listaDados[randomIndex] = aux;
            }
        }
    }

    private void AtualizarVariaveis()
    {
        for(int i = 0; i < quantidade_facil; i++){
            perguntas_facil[i] = CarregaDados.listaDados[i].pergunta;
            respostas_facil[i] = 0;
            dicas_facil[i] = CarregaDados.listaDados[i].dica;
            respostas_possiveis_facil[i, 0] = CarregaDados.listaDados[i].resposta;
            respostas_possiveis_facil[i, 1] = CarregaDados.listaDados[i].r2;
            respostas_possiveis_facil[i, 2] = CarregaDados.listaDados[i].r3;
            respostas_possiveis_facil[i, 3] = CarregaDados.listaDados[i].r4;
        }
        for(int i = 0; i < quantidade_medio; i++){
            perguntas_medio[i] = CarregaDados.listaDados[i+quantidade_facil].pergunta;
            respostas_medio[i] = 0;
            dicas_medio[i] = CarregaDados.listaDados[i+quantidade_facil].dica;
            respostas_possiveis_medio[i, 0] = CarregaDados.listaDados[i+quantidade_facil].resposta;
            respostas_possiveis_medio[i, 1] = CarregaDados.listaDados[i+quantidade_facil].r2;
            respostas_possiveis_medio[i, 2] = CarregaDados.listaDados[i+quantidade_facil].r3;
            respostas_possiveis_medio[i, 3] = CarregaDados.listaDados[i+quantidade_facil].r4;
        }
        for(int i = 0; i < quantidade_dificil; i++){
            perguntas_dificil[i] = CarregaDados.listaDados[i+quantidade_facil+quantidade_medio].pergunta;
            respostas_dificil[i] = 0;
            dicas_dificil[i] = CarregaDados.listaDados[i+quantidade_facil+quantidade_medio].dica;
            respostas_possiveis_dificil[i, 0] = CarregaDados.listaDados[i+quantidade_facil+quantidade_medio].resposta;
            respostas_possiveis_dificil[i, 1] = CarregaDados.listaDados[i+quantidade_facil+quantidade_medio].r2;
            respostas_possiveis_dificil[i, 2] = CarregaDados.listaDados[i+quantidade_facil+quantidade_medio].r3;
            respostas_possiveis_dificil[i, 3] = CarregaDados.listaDados[i+quantidade_facil+quantidade_medio].r4;
        }

        //FACIL
        for(int i=0; i<11; i++)
        {
            if((CarregaDados.listaDados[i].nivel == "facil") && (i < CarregaDados.listaDados.Count)){
                audios_perguntas[i] = CarregaDados.listaDados[i].audio_pergunta;
                audios_dicas[i] = CarregaDados.listaDados[i].audio_dica;
                for(int j = 0; j < 4; j++)
                {
                    audios_alternativas[i, j] = CarregaDados.listaDados[i].audio_alternativas[j];
                }
            }
        }
        //MEDIO
        int aux_medio = 0;
        for(int i=11; i<22; i++)
        {
            if(i-11 <= CarregaDados.listaDados.Count && aux_medio < quantidade_medio){
                audios_perguntas[i] = CarregaDados.listaDados[i-11+quantidade_facil].audio_pergunta;
                audios_dicas[i] = CarregaDados.listaDados[i-11+quantidade_facil].audio_dica;
                for(int j = 0; j < 4; j++)
                {
                    audios_alternativas[i, j] = CarregaDados.listaDados[i-11+quantidade_facil].audio_alternativas[j];
                }
                aux_medio++;
            }
        }
        //DIFICIL
        int aux_dificil = 0;
        for(int i=22; i<33; i++)
        {
            if(i-22 <= CarregaDados.listaDados.Count && aux_dificil < quantidade_dificil){
                audios_perguntas[i] = CarregaDados.listaDados[i-22+quantidade_facil+quantidade_medio].audio_pergunta;
                audios_dicas[i] = CarregaDados.listaDados[i-22+quantidade_facil+quantidade_medio].audio_dica;
                for(int j = 0; j < 4; j++)
                {
                    audios_alternativas[i, j] = CarregaDados.listaDados[i-22+quantidade_facil+quantidade_medio].audio_alternativas[j];
                }
                aux_dificil++;
            }
        }
    }


    HashSet<int> gerador = new HashSet<int>();
    public int[] pergunta_trocar = new int[4];

    private void MisturarRespostas()
    {


        for (int i = 0; i < quantidade_facil + 1; i++)
        {   

            string aux_s;
            string aux_audio;
            // int troca1 = random.Next(0, Int32.MaxValue) % 4; // Gera número entre 0 e 3
            // int troca2 = random.Next(0, Int32.MaxValue) % 4; // Gera número entre 0 e 3
            gerador = RandomHashSet.Gera(0, 4);
            pergunta_trocar = gerador.ToArray();
            int troca1 = 0;
            int troca2 = pergunta_trocar[0];

            respostas_facil[i] = troca2;

            aux_s = respostas_possiveis_facil[i, troca1];
            respostas_possiveis_facil[i, troca1] = respostas_possiveis_facil[i, troca2];
            respostas_possiveis_facil[i, troca2] = aux_s;

            aux_audio = audios_alternativas[i, troca1];
            audios_alternativas[i, troca1] = audios_alternativas[i, troca2];
            audios_alternativas[i, troca2] = aux_audio;

        }

        for (int i = 0; i < quantidade_medio + 1; i++)
        {
            string aux_s;
            string aux_audio;
            // int troca1 = random.Next(0, Int32.MaxValue) % 4; // Gera número entre 0 e 3
            // int troca2 = random.Next(0, Int32.MaxValue) % 4; // Gera número entre 0 e 3

            gerador = RandomHashSet.Gera(0, 4);
            pergunta_trocar = gerador.ToArray();
            int troca1 = 0;
            int troca2 = pergunta_trocar[0];

            respostas_medio[i] = troca2;

            aux_s = respostas_possiveis_medio[i, troca1];
            respostas_possiveis_medio[i, troca1] = respostas_possiveis_medio[i, troca2];
            respostas_possiveis_medio[i, troca2] = aux_s;

            aux_audio = audios_alternativas[i+11, troca1];
            audios_alternativas[i+11, troca1] = audios_alternativas[i+11, troca2];
            audios_alternativas[i+11, troca2] = aux_audio;
        }

        for (int i = 0; i < quantidade_dificil + 1; i++)
        {
            string aux_s;
            string aux_audio;
            // int troca1 = random.Next(0, Int32.MaxValue) % 4; // Gera número entre 0 e 3
            // int troca2 = random.Next(0, Int32.MaxValue) % 4; // Gera número entre 0 e 3

            gerador = RandomHashSet.Gera(0, 4);
            pergunta_trocar = gerador.ToArray();
            int troca1 = 0;
            int troca2 = pergunta_trocar[0];

            respostas_dificil[i] = troca2;

            aux_s = respostas_possiveis_dificil[i, troca1];
            respostas_possiveis_dificil[i, troca1] = respostas_possiveis_dificil[i, troca2];
            respostas_possiveis_dificil[i, troca2] = aux_s;

            aux_audio = audios_alternativas[i+22, troca1];
            audios_alternativas[i+22, troca1] = audios_alternativas[i+22, troca2];
            audios_alternativas[i+22, troca2] = aux_audio;
        }
    }

    private void AtualizarPerguntaTela()
    {
        estado = JOGANDO;
        AtualizarAudios();
        botao_pergunta.Select();
        ExibirNaTela();
        ConfigurarBotoes();
        #if UNITY_ANDROID
        #else
            MostrarBarra();
        #endif
    }

    public void LimparDados()
    {
        
    }

    private void SalvarInfos()
    {
        Informacoes.SetQuantidadeFacil(quantidade_facil);
        Informacoes.SetQuantidadeMedio(quantidade_medio);
        Informacoes.SetQuantidadeDificil(quantidade_dificil);
        Informacoes.SetNivel(nivel_atual);
        Informacoes.SetPontos(pontuacao);
        Informacoes.SetPerguntasFacil(perguntas_facil);
        Informacoes.SetRespostasFacil(respostas_facil);
        Informacoes.SetRespostasPossiveisFacil(respostas_possiveis_facil);
        Informacoes.SetDicasFacil(dicas_facil);
        Informacoes.SetPerguntasMedio(perguntas_medio);
        Informacoes.SetRespostasMedio(respostas_medio);
        Informacoes.SetRespostasPossiveisMedio(respostas_possiveis_medio);
        Informacoes.SetDicasMedio(dicas_medio);
        Informacoes.SetPerguntasDificil(perguntas_dificil);
        Informacoes.SetRespostasDificil(respostas_dificil);
        Informacoes.SetRespostasPossiveisDificil(respostas_possiveis_dificil);
        Informacoes.SetDicasDificil(dicas_dificil);
        Informacoes.SetStatus5050(selecionou5050);

        Informacoes.SetAudiosPerguntas(audios_perguntas);
        Informacoes.SetAudiosAlternativas(audios_alternativas);
        Informacoes.SetAudiosDicas(audios_dicas);

        Informacoes.SetAcertoConsecutivo(Acerto_Consecutivo);
        Informacoes.SetQuantidadeAcertos(quantidade_acertos);
        Informacoes.SetPontosGanhos(pontos_ganhos);

        Informacoes.SetNumeroQuestao(questao_x_de_y);
        Informacoes.SetPerguntaAtual(pergunta_atual);
        Informacoes.SetPerguntasRespondidas(perguntas_bool);
        Informacoes.SetAjudasUsadas(ajudas_usadas);

        Informacoes.SetQuantidade5050(quantidade_5050);
        Informacoes.SetQuantidadePular(quantidade_pular);
        Informacoes.SetStatusPular(selecionou_pular);

        if(selecionou5050 == SIM)
            Informacoes.SetTirar(tirar_1, tirar_2);

    }
    
    
    private void PegarInfosSalvas()
    {
        if(Informacoes.GetStatus() == STATUSOPCOES || Informacoes.GetStatus() == PROXPALAVRA){
            questao_x_de_y = Informacoes.GetNumeroQuestao();
        }
        else{
            questao_x_de_y = -1;
        }
        

        if(Informacoes.GetQuantidadePular() != 0 ){
            pular_agora = NAO;
        }else{
            pular_agora = SIM;
        }

        quantidade_facil = Informacoes.GetQuantidadeFacil();
        quantidade_medio = Informacoes.GetQuantidadeMedio();
        quantidade_dificil = Informacoes.GetQuantidadeDificil();
        nivel_atual = Informacoes.GetNivel();
        pontuacao = Informacoes.GetPontos();
        perguntas_facil = Informacoes.GetPerguntasFacil();
        respostas_facil = Informacoes.GetRespostasFacil();
        respostas_possiveis_facil = Informacoes.GetRespostasPossiveisFacil();
        dicas_facil = Informacoes.GetDicasFacil();
        perguntas_medio = Informacoes.GetPerguntasMedio();
        respostas_medio = Informacoes.GetRespostasMedio();
        respostas_possiveis_medio = Informacoes.GetRespostasPossiveisMedio();
        dicas_medio = Informacoes.GetDicasMedio();
        perguntas_dificil = Informacoes.GetPerguntasDificil();
        respostas_dificil = Informacoes.GetRespostasDificil();
        respostas_possiveis_dificil = Informacoes.GetRespostasPossiveisDificil();
        dicas_dificil = Informacoes.GetDicasDificil();
        selecionou5050 = Informacoes.GetStatus5050();
        selecionou_pular = Informacoes.GetStatusPular();
        volume_efeitos = Informacoes.GetValueEfeitos();
        volume_musica = Informacoes.GetValueEfeitos();
        volume_texto = Informacoes.GetValueLeituraTexto();
        Acerto_Consecutivo = Informacoes.GetAcertoConsecutivo();
        quantidade_acertos = Informacoes.GetQuantidadeAcertos();
        audios_perguntas = Informacoes.GetAudiosPerguntas();
        audios_alternativas = Informacoes.GetAudiosAlternativas();
        audios_dicas = Informacoes.GetAudiosDicas();
        pergunta_atual = Informacoes.GetPerguntaAtual();
        perguntas_bool = Informacoes.GetPerguntasRespondidas();
        ajudas_usadas = Informacoes.GetAjudasUsadas();
        quantidade_5050 = Informacoes.GetQuantidade5050();
        quantidade_pular = Informacoes.GetQuantidadePular();

        if(selecionou5050 == SIM){
            tirar_1 = Informacoes.GetTirar1();
            tirar_2 = Informacoes.GetTirar2();
        }
    }

    public void TocarHighlight(int alternativa)
    {
        #if UNITY_ANDROID
            pausarAudios();
            if(alternativa == 0){
                if(alternativas[0].interactable){
                    audio_a0.Play();
                    alternativas[0].Select();
                }
            }
            if (alternativa == 1){
                if(alternativas[1].interactable){
                    audio_a1.Play();
                    alternativas[1].Select();
                }
            }
            if (alternativa == 2){
                if(alternativas[2].interactable){
                    audio_a2.Play();
                    alternativas[2].Select();
                }
            }
            if (alternativa == 3){
                if(alternativas[3].interactable){
                    audio_a3.Play();
                    alternativas[3].Select();
                }
            }
            //Pergunta
            if (alternativa == 4){
                audio_pergunta.Play();
                botao_pergunta.Select();
            }
            //Dica
            if(alternativa == 5){
                dica.Select();
            }
            //5050
            if(alternativa == 6){
                if(ajuda5050.interactable){
                    ajuda5050.Select();
                }
            }
            //Pular
            if(alternativa == 7){
                if(pular.interactable){
                    pular.Select();
                }
            }
        #endif
    }

    public void AtualizarAudios()
    {
        if(nivel_atual == FACIL){
            txt_audio_pergunta = audios_perguntas[questao_x_de_y];
            txt_audio_dica = audios_dicas[questao_x_de_y];
            txt_audio_a0 = audios_alternativas[questao_x_de_y, 0];
            txt_audio_a1 = audios_alternativas[questao_x_de_y, 1];
            txt_audio_a2 = audios_alternativas[questao_x_de_y, 2];
            txt_audio_a3 = audios_alternativas[questao_x_de_y, 3];
        }
        else if(nivel_atual == MEDIO){
            txt_audio_pergunta = audios_perguntas[questao_x_de_y+11];
            txt_audio_dica = audios_dicas[questao_x_de_y+11];
            txt_audio_a0 = audios_alternativas[questao_x_de_y+11, 0];
            txt_audio_a1 = audios_alternativas[questao_x_de_y+11, 1];
            txt_audio_a2 = audios_alternativas[questao_x_de_y+11, 2];
            txt_audio_a3 = audios_alternativas[questao_x_de_y+11, 3];
        }
        else{
            txt_audio_pergunta = audios_perguntas[questao_x_de_y+22];
            txt_audio_dica = audios_dicas[questao_x_de_y+22];
            txt_audio_a0 = audios_alternativas[questao_x_de_y+22, 0];
            txt_audio_a1 = audios_alternativas[questao_x_de_y+22, 1];
            txt_audio_a2 = audios_alternativas[questao_x_de_y+22, 2];
            txt_audio_a3 = audios_alternativas[questao_x_de_y+22, 3];
        }
        StartCoroutine(carregarAudios());
    }

    public IEnumerator carregarAudios(){
        #if UNITY_STANDALONE
            string wwwPlayerFilePath = "file://" + Application.streamingAssetsPath + "/audios/" + txt_audio_pergunta + ".wav";
            string wwwPlayerFilePathB = "file://" + Application.streamingAssetsPath + "/audios/" + txt_audio_a0 + ".wav";
            string wwwPlayerFilePathC = "file://" + Application.streamingAssetsPath + "/audios/" + txt_audio_a1 + ".wav";
            string wwwPlayerFilePathD = "file://" + Application.streamingAssetsPath + "/audios/" + txt_audio_a2 + ".wav";
            string wwwPlayerFilePathE = "file://" + Application.streamingAssetsPath + "/audios/" + txt_audio_a3 + ".wav";
            string wwwPlayerFilePathF = "file://" + Application.streamingAssetsPath + "/audios/" + txt_audio_dica + ".wav";
        #else
            string wwwPlayerFilePath = Application.streamingAssetsPath + "/audios/" + txt_audio_pergunta + ".wav";
            string wwwPlayerFilePathB = Application.streamingAssetsPath + "/audios/" + txt_audio_a0 + ".wav";
            string wwwPlayerFilePathC = Application.streamingAssetsPath + "/audios/" + txt_audio_a1 + ".wav";
            string wwwPlayerFilePathD = Application.streamingAssetsPath + "/audios/" + txt_audio_a2 + ".wav";
            string wwwPlayerFilePathE = Application.streamingAssetsPath + "/audios/" + txt_audio_a3 + ".wav";
            string wwwPlayerFilePathF = Application.streamingAssetsPath + "/audios/" + txt_audio_dica + ".wav";
        #endif
         
        Debug.Log(wwwPlayerFilePathF);

        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(wwwPlayerFilePath, AudioType.WAV)) {
            yield return www.SendWebRequest();
            if (www.isNetworkError) {
            } else {
                audio_pergunta.clip = DownloadHandlerAudioClip.GetContent(www);
            }
        };
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(wwwPlayerFilePathB, AudioType.WAV)) {
            yield return www.SendWebRequest();
            if (www.isNetworkError) {
            } else {
                audio_a0.clip = DownloadHandlerAudioClip.GetContent(www);
            }
        };
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(wwwPlayerFilePathC, AudioType.WAV)) {
            yield return www.SendWebRequest();
            if (www.isNetworkError) {
            } else {
               audio_a1.clip = DownloadHandlerAudioClip.GetContent(www);
            }
        };
         using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(wwwPlayerFilePathD, AudioType.WAV)) {
            yield return www.SendWebRequest();
            if (www.isNetworkError) {
            } else {
               audio_a2.clip = DownloadHandlerAudioClip.GetContent(www);
            }
        };
         using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(wwwPlayerFilePathE, AudioType.WAV)) {
            yield return www.SendWebRequest();
            if (www.isNetworkError) {
            } else {
               audio_a3.clip = DownloadHandlerAudioClip.GetContent(www);
            }
        };
         using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(wwwPlayerFilePathF, AudioType.WAV)) {
            yield return www.SendWebRequest();
            if (www.isNetworkError) {
            } else {
               audio_dica.clip = DownloadHandlerAudioClip.GetContent(www);
            }
        };
        audio_pergunta.Play();
    }

    public void ObjetoSelecionado(int FlagSelecionado){
        Selecionado = FlagSelecionado;
    }

    public void EscolherAlternativa(string alternativa)
    {
        alternativa_escolhida = Convert.ToInt32(alternativa);
        #if UNITY_ANDROID
            if(!one_click){
                one_click = true;
                timer_for_double_click = Time.time;
                
                if(alternativa_escolhida == 0){
                    audio_a0.Play();
                }
                else if (alternativa_escolhida == 1){
                    audio_a1.Play();
                }
                else if (alternativa_escolhida == 2){
                    audio_a2.Play();
                }
                else if (alternativa_escolhida == 3){
                    audio_a3.Play();
                }
                else if (alternativa_escolhida == 4)
                    audio_pergunta.Play();
            }
            else{
                one_click = false;

                alternativas[alternativa_escolhida].Select();
                confirmar.interactable = true;
                confirmar.interactable = false;
                MostrarPanelConfirmar();
            }
        #else
            alternativa_escolhida = Convert.ToInt32(alternativa);
            alternativas[alternativa_escolhida].Select();
            confirmar.interactable = true;

            confirmar.interactable = false;

            MostrarPanelConfirmar();
        #endif
    }


    public void MostrarPanelConfirmar(){
        Panel_confirmar_anim.SetBool("showPanel", true);
        audio_confirmar.Play();
        botao_panel_sim.Select();
        dica.enabled = false;
        audio_botao_dica.enabled = false;
        ajuda5050.enabled = false;
        audio_5050.enabled = false;
        pular.enabled = false;
        audio_pular.enabled = false;
        botao_pergunta.enabled = false;
        audio_pergunta.enabled = false;
        alternativas[0].enabled = false;
        audio_a0.enabled = false;
        alternativas[1].enabled = false;
        audio_a1.enabled = false;
        alternativas[2].enabled = false;
        audio_a2.enabled = false;
        alternativas[3].enabled = false;
        audio_a3.enabled = false;
        opcoes.enabled = false;
        audio_opcoes.enabled = false;
        
        /*botao_pergunta.gameObject.SetActive(false);
        alternativas[0].gameObject.SetActive(false);
        alternativas[1].gameObject.SetActive(false);
        alternativas[2].gameObject.SetActive(false);
        alternativas[3].gameObject.SetActive(false);
        dica.gameObject.SetActive(false);
        ajuda5050.gameObject.SetActive(false);
        pular.gameObject.SetActive(false);
        */
        
    }

    public void EsconderPanelConfirmar(){
        Panel_confirmar_anim.SetBool("showPanel", false);
        dica.enabled = true;
        audio_botao_dica.enabled = true;
        ajuda5050.enabled = true;
        audio_5050.enabled = true;
        pular.enabled = true;
        audio_pular.enabled = true;
        botao_pergunta.enabled = true;
        audio_pergunta.enabled = true;
        alternativas[0].enabled = true;
        audio_a0.enabled = true;
        alternativas[1].enabled = true;
        audio_a1.enabled = true;
        alternativas[2].enabled = true;
        audio_a2.enabled = true;
        alternativas[3].enabled = true;
        audio_a3.enabled = true;
        opcoes.enabled = true;
        audio_opcoes.enabled = true;
    }
    
    public void ConfirmarAlternativa()
    {
        //confirmar.interactable = false;

        if(selecionou5050 == SIM){
            selecionou5050 = NAO;
        }

        SomarPontuacao();
        ExibirCertoOuErrado();
        EsconderPanelConfirmar();
    }

    public void NaoConfirmarAlternativa(){
        EsconderPanelConfirmar();
        botao_pergunta.Select();
        
        // alternativas[alternativa_escolhida].Select(); Não precisa falar duas vezes
    }

    public void Funcao5050()
    {
        if(quantidade_5050 > 0){
            #if UNITY_ANDROID
            if(!one_click){
                one_click = true;
                timer_for_double_click = Time.time;

                pausarAudios();
                audio_5050.Play();
            }
            else{
                one_click = false;

                selecionou5050 = SIM;
                confirmar.interactable = false;

                // tirar_1 = random.Next(0, Int32.MaxValue) % 4; // Gera número entre 0 e 3
                tirar_1 = GeraNumero(0, 4);

                while (tirar_1 == alternativa_correta)
                {
                    // tirar_1 = random.Next(0, Int32.MaxValue) % 4; // Gera número entre 0 e 3
                    tirar_1 = GeraNumero(0, 4);
                }
                // tirar_2 = random.Next(0, Int32.MaxValue) % 4; // Gera número entre 0 e 3
                tirar_2 = GeraNumero(0, 4);
                while (tirar_1 == tirar_2 || tirar_2 == alternativa_correta)
                {
                    // tirar_2 = random.Next(0, Int32.MaxValue) % 4; // Gera número entre 0 e 3
                    tirar_2 = GeraNumero(0, 4);
                }
                alternativas[tirar_1].interactable = false;
                alternativas[tirar_2].interactable = false;
                

                ajuda5050.interactable = false;
                for(int i=0; i<4; i++){
                    if(alternativas[i].interactable)
                    {
                        alternativas[i].Select();
                        break;
                    }
                }
            }
        #else
            
            audio_5050.Play();
            selecionou5050 = SIM;
            confirmar.interactable = false;

            if(ajudas_usadas[pergunta_atual]==0){
                ajudas_usadas[pergunta_atual] = 1;
            }else{
                ajudas_usadas[pergunta_atual] = 3;
            }

            // tirar_1 = random.Next(0, Int32.MaxValue) % 4; // Gera número entre 0 e 3
            tirar_1 = GeraNumero(0, 4);

            while (tirar_1 == alternativa_correta)
            {
                // tirar_1 = random.Next(0, Int32.MaxValue) % 4; // Gera número entre 0 e 3
                tirar_1 = GeraNumero(0, 4);
            }
            // tirar_2 = random.Next(0, Int32.MaxValue) % 4; // Gera número entre 0 e 3
            tirar_2 = GeraNumero(0, 4);
            while (tirar_1 == tirar_2 || tirar_2 == alternativa_correta)
            {
                // tirar_2 = random.Next(0, Int32.MaxValue) % 4; // Gera número entre 0 e 3
                tirar_2 = GeraNumero(0, 4);
            }
            alternativas[tirar_1].interactable = false;
            alternativas[tirar_2].interactable = false;

            if(tirar_1 == 0){
                alternativa1_tela.color =  new Color(0.427451f, 0.427451f, 0.427451f, 1);
            }else if(tirar_1 == 1){
                alternativa2_tela.color =  new Color(0.427451f, 0.427451f, 0.427451f, 1);
            }else if(tirar_1 == 2){
                alternativa3_tela.color =  new Color(0.427451f, 0.427451f, 0.427451f, 1);
            }else{
                alternativa4_tela.color =  new Color(0.427451f, 0.427451f, 0.427451f, 1);
            }
            
            if(tirar_2 == 0){
                alternativa1_tela.color =  new Color(0.427451f, 0.427451f, 0.427451f, 1);
            }else if(tirar_2 == 1){
                alternativa2_tela.color =  new Color(0.427451f, 0.427451f, 0.427451f, 1);
            }else if(tirar_2 == 2){
                alternativa3_tela.color =  new Color(0.427451f, 0.427451f, 0.427451f, 1);
            }else{
                alternativa4_tela.color =  new Color(0.427451f, 0.427451f, 0.427451f, 1);
            }

            ajuda5050_tela.color = new Color(0.427451f, 0.427451f, 0.427451f, 1);
            quantidade_5050_tela.color = new Color(0.5660378f,0.4708605f, 0.2963688f,1);

            risco[tirar_1].SetActive(true);
            risco[tirar_2].SetActive(true);

            ajuda5050.interactable = false;
            for(int i=0; i<4; i++){
                if(alternativas[i].interactable)
                {
                    alternativas[i].Select();
                    break;
                }
            }
        #endif

            quantidade_5050--;
        }else{
            ajuda5050.interactable = false;
            
        }

        
    }

    public void FuncaoDica()
    {
        #if UNITY_ANDROID
            if(!one_click){
                one_click = true;
                timer_for_double_click = Time.time;

                pausarAudios();
                audio_botao_dica.Play();
            }
            else{
                one_click = false;

                confirmar.interactable = false;
                estado = JANELA;
                audio_dica.Play();
                botao_panel.Select();
                CriarJanelaDica();
            }
        #else
            
            confirmar.interactable = false;
            estado = JANELA;
            botao_panel.Select();
            
            audio_dica.Play();
            
            //botao_panel.Select();
            CriarJanelaDica();

            audio_botao_dica.Play();
        #endif
    }

    public void FuncaoPular()
    {
        if(quantidade_pular >0){
            #if UNITY_ANDROID
            if(!one_click){
                one_click = true;
                timer_for_double_click = Time.time;

                pausarAudios();
                audio_pular.Play();
            }
            else{
                one_click = false;

                Informacoes.SetStatusPular(SIM);
                selecionou_pular = SIM;
                confirmar.interactable = false;
                pular.interactable = false;
                pular_agora = SIM;
                PegarProximaQuestao();
                AtualizarPerguntaTela();
                alternativas[0].Select();
            }
        #else

            audio_pular.Play();

            if(ajudas_usadas[pergunta_atual]==0){
                ajudas_usadas[pergunta_atual] = 2;
            }else{
                ajudas_usadas[pergunta_atual] = 3;
            }

            Informacoes.SetStatusPular(SIM);
            selecionou_pular = SIM;
            confirmar.interactable = false;
            pular.interactable = false;
            pular_agora = SIM;
            PegarProximaQuestao();
            AtualizarPerguntaTela();
            alternativas[0].Select();

            alternativa1_tela.color =  new Color(0.8113208f, 0.8113208f,0.8113208f, 1);
            alternativa2_tela.color =  new Color(0.8113208f, 0.8113208f,0.8113208f, 1);
            alternativa3_tela.color =  new Color(0.8113208f, 0.8113208f,0.8113208f, 1);
            alternativa4_tela.color =  new Color(0.8113208f, 0.8113208f,0.8113208f, 1);

            if(selecionou5050 == SIM && quantidade_5050 > 0){
            alternativas[tirar_1].interactable = true;
            alternativas[tirar_2].interactable = true;
            ajuda5050_tela.color = new Color(0.8113208f, 0.8113208f,0.8113208f, 1);
            quantidade_5050_tela.color = new Color(1,0.8573965f,0.5990566f,1);
            ajuda5050.interactable = true;
            selecionou5050 = NAO;
            }

            pular_tela.color = new Color(0.427451f, 0.427451f, 0.427451f, 1);
            quantidade_pular_tela.color = new Color(0.5660378f,0.4708605f, 0.2963688f,1);

        #endif
            quantidade_pular--;
        }else{
            pular.interactable = false;
            //pular_tela.color = new Color(0.427451f, 0.427451f, 0.427451f, 1);
        }
    }

    private void SomarPontuacao()
    {

        if (nivel_atual == FACIL)
        {
            if(alternativa_escolhida == respostas_facil[questao_x_de_y])
            {
                pontos_ganhos = 10;

                if(Acerto_Consecutivo){
                    pontos_ganhos += 5;
                    quantidade_acertos ++;
                }
                else{
                    Acerto_Consecutivo = true;
                }
            }
            else
            {
                pontos_ganhos = 0;

                Acerto_Consecutivo = false;
            }
        }
        else if (nivel_atual == MEDIO)
        {
            if(alternativa_escolhida == respostas_medio[questao_x_de_y])
            {
                pontos_ganhos = 15;

                if(Acerto_Consecutivo){
                    pontos_ganhos += 5;
                    quantidade_acertos ++;
                }
                else{
                    Acerto_Consecutivo = true;
                }
            }
            else
            {
                pontos_ganhos = 0;

                Acerto_Consecutivo = false;
            }
        }
        else
        {
            if(alternativa_escolhida == respostas_dificil[questao_x_de_y])
            {
                pontos_ganhos = 20;

                if(Acerto_Consecutivo){
                    pontos_ganhos += 5;
                    quantidade_acertos ++;
                }
                else{
                    Acerto_Consecutivo = true;
                }
            }
            else
            {
                pontos_ganhos = 0;

                Acerto_Consecutivo = false;
            }
        }
        
        //Informacoes.SetPontosGanhos(pontos_ganhos);
        pontuacao += pontos_ganhos;
    }

    public int AtualizarNivel()
    {
        PegarInfosSalvas();
        //questao_x_de_y ++;
        Debug.Log(questao_x_de_y);
        Debug.Log(quantidade_facil);
        if (pular_agora == NAO)
        {
            if (nivel_atual == FACIL && questao_x_de_y >= quantidade_facil-1)
            {
                Informacoes.SetStatus(MEIO);
                nivel_atual = MEDIO;
                SalvarInfos();
                return MUDOU_NIVEL;
            }
            else if (nivel_atual == MEDIO && questao_x_de_y >= quantidade_medio-1)
            {
                Informacoes.SetStatus(MEIO);
                nivel_atual = DIFICIL;
                SalvarInfos();
                return MUDOU_NIVEL;
            }
            SalvarInfos();
            return NAO_MUDOU_NIVEL;
        }
        else if(selecionou_pular == 2){

            if (nivel_atual == FACIL && questao_x_de_y >= quantidade_facil-1)
            {
                Informacoes.SetStatus(MEIO);
                nivel_atual = MEDIO;
                SalvarInfos();
                return MUDOU_NIVEL;
            }
            else if (nivel_atual == MEDIO && questao_x_de_y >= quantidade_medio-1)
            {
                Informacoes.SetStatus(MEIO);
                nivel_atual = DIFICIL;
                SalvarInfos();
                return MUDOU_NIVEL;
            }
            SalvarInfos();
            return NAO_MUDOU_NIVEL;

        }
        else
        {
            if (nivel_atual == FACIL && questao_x_de_y >= quantidade_facil)
            {
                Informacoes.SetStatus(MEIO);
                nivel_atual = MEDIO;
                selecionou_pular = 2;
                SalvarInfos();
                return MUDOU_NIVEL;
            }
            else if (nivel_atual == MEDIO && questao_x_de_y >= quantidade_medio)
            {
                Informacoes.SetStatus(MEIO);
                nivel_atual = DIFICIL;
                selecionou_pular = 2;
                SalvarInfos();
                return MUDOU_NIVEL;
            }
            SalvarInfos();
            return NAO_MUDOU_NIVEL;
        }
        
    }

    private void ExibirNaTela()
    {

        if (nivel_atual == FACIL)
        {
            ExibirNaTelaFacil();
        }
        else if (nivel_atual == MEDIO)
        {
            ExibirNaTelaMedio();
        }
        else
        {
            ExibirNaTelaDificil();
        }
    }

    private void ExibirNaTelaFacil()
    {
        dificuldade_tela.text = "NÍVEL FÁCIL";
        if (pular_agora == SIM){
            numero_questao_tela.text = "Questão " + (questao_x_de_y).ToString() + " de " + (quantidade_facil + quantidade_medio + quantidade_dificil).ToString();}
        else
            numero_questao_tela.text = "Questão " + (questao_x_de_y + 1).ToString() + " de " + (quantidade_facil + quantidade_medio + quantidade_dificil).ToString();
        pergunta_tela.text = perguntas_facil[questao_x_de_y];
        alternativa_correta = respostas_facil[questao_x_de_y];
        
        //alternativas[0].Select();
        alternativa1_tela.text = respostas_possiveis_facil[questao_x_de_y, 0];
        alternativa2_tela.text = respostas_possiveis_facil[questao_x_de_y, 1];
        alternativa3_tela.text = respostas_possiveis_facil[questao_x_de_y, 2];
        alternativa4_tela.text = respostas_possiveis_facil[questao_x_de_y, 3];

        pontuacao_tela.text = "Pontos: " + pontuacao.ToString();

        if(Acerto_Consecutivo){
            valorQuestao.text = "Valendo: 10 + 5";
        }
        else{
            valorQuestao.text = "Valendo: 10";
        }

    }

    private void ExibirNaTelaMedio()
    {
        dificuldade_tela.text = "NÍVEL MÉDIO";
        if (selecionou_pular == 2)
            numero_questao_tela.text = "Questão " + (questao_x_de_y + 1 + quantidade_facil).ToString() + " de " + (quantidade_facil + quantidade_medio + quantidade_dificil).ToString();
        else if (pular_agora == SIM)
            numero_questao_tela.text = "Questão " + (questao_x_de_y + quantidade_facil).ToString() + " de " + (quantidade_facil + quantidade_medio + quantidade_dificil).ToString();
        else
            numero_questao_tela.text = "Questão " + (questao_x_de_y + 1 + quantidade_facil).ToString() + " de " + (quantidade_facil + quantidade_medio + quantidade_dificil).ToString();
        pergunta_tela.text = perguntas_medio[questao_x_de_y];
        alternativa_correta = respostas_medio[questao_x_de_y];

        //alternativas[0].Select();
        alternativa1_tela.text = respostas_possiveis_medio[questao_x_de_y, 0];
        alternativa2_tela.text = respostas_possiveis_medio[questao_x_de_y, 1];
        alternativa3_tela.text = respostas_possiveis_medio[questao_x_de_y, 2];
        alternativa4_tela.text = respostas_possiveis_medio[questao_x_de_y, 3];

        pontuacao_tela.text = "Pontos: " + pontuacao.ToString();

        if(Acerto_Consecutivo){
            valorQuestao.text = "Valendo: 15 + 5";
        }
        else{
            valorQuestao.text = "Valendo: 15";
        }

    }

    private void ExibirNaTelaDificil()
    {
        dificuldade_tela.text = "NÍVEL DIFÍCIL";
        if (selecionou_pular == 2)
            numero_questao_tela.text = "Questão " + (questao_x_de_y + 1 + quantidade_facil + quantidade_medio).ToString() + " de " + (quantidade_facil + quantidade_medio + quantidade_dificil).ToString();
        else if (pular_agora == SIM)
            numero_questao_tela.text = "Questão " + (questao_x_de_y  + quantidade_facil + quantidade_medio).ToString() + " de " + (quantidade_facil + quantidade_medio + quantidade_dificil).ToString();
        else
            numero_questao_tela.text = "Questão " + (questao_x_de_y + 1 + quantidade_facil + quantidade_medio).ToString() + " de " + (quantidade_facil + quantidade_medio + quantidade_dificil).ToString();
        pergunta_tela.text = perguntas_dificil[questao_x_de_y];
        alternativa_correta = respostas_dificil[questao_x_de_y];
            
        //alternativas[0].Select();
        alternativa1_tela.text = respostas_possiveis_dificil[questao_x_de_y, 0];
        alternativa2_tela.text = respostas_possiveis_dificil[questao_x_de_y, 1];
        alternativa3_tela.text = respostas_possiveis_dificil[questao_x_de_y, 2];
        alternativa4_tela.text = respostas_possiveis_dificil[questao_x_de_y, 3];

        pontuacao_tela.text = "Pontos: " + pontuacao.ToString();

        if(Acerto_Consecutivo){
            valorQuestao.text = "Valendo: 20 + 5";
        }
        else{
            valorQuestao.text = "Valendo: 20";
        }

    }

    private void PegarProximaQuestao()
    {
        questao_x_de_y ++;

    }

    private void ConfigurarBotoes()
    {
        for (int i = 0; i < 4; i++)
        {
            alternativas[i].interactable = true;
        }
        confirmar.interactable = false;
        if (quantidade_5050 == 0)
        {
            ajuda5050.interactable = false;
            ajuda5050_tela.color = new Color(0.427451f, 0.427451f, 0.427451f, 1);
            quantidade_5050_tela.color = new Color(0.5660378f,0.4708605f, 0.2963688f,1);
        }
        if (quantidade_pular == 0)
        {
            pular.interactable = false;
        }
    }

    private void ExibirTelaNivel()
    {
        questao_x_de_y --;
        SalvarInfos();
        SceneManager.LoadScene("Nivel");
    }

    public void Voltar()
    {
        SceneManager.LoadScene("Menu");
    }

    public void AcessarOpcoes()
    {
        #if UNITY_ANDROID
            if(!one_click){
                one_click = true;
                timer_for_double_click = Time.time;

                //Audio opcoes
            }
            else{
                one_click = false;

                Informacoes.SetStatus(STATUSOPCOES);
                SalvarInfos();
                SceneManager.LoadScene("Opcoes");
            }
        #else
            Informacoes.SetStatus(STATUSOPCOES);
            SalvarInfos();
            SceneManager.LoadScene("Opcoes");
        #endif
    }

    private int VerificarFim()
    {

        if (pular_agora == NAO)
        {
            if (questao_x_de_y == quantidade_dificil && nivel_atual == DIFICIL)
            {
                Informacoes.SetPontos(pontuacao);
                //SceneManager.LoadScene("Fim");
                return 1;
            }
            else
            {
                return 0;
            }
        }
        else if(selecionou_pular == 2)
        {
            if (questao_x_de_y == quantidade_dificil && nivel_atual == DIFICIL)
            {
                Informacoes.SetPontos(pontuacao);
                //SceneManager.LoadScene("Fim");
                return 1;
            }
            else
            {
                return 0;
            }
        }
        else
        {
            if (questao_x_de_y == quantidade_dificil + 1 && nivel_atual == DIFICIL)
            {
                Informacoes.SetPontos(pontuacao);
                //SceneManager.LoadScene("Fim");
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }

    private GUIStyle guiStyle = new GUIStyle();

    private void OnGUI () 
    {
        
    }

    private void CriarJanelaDica ()
    {
        Panel_anim.SetBool("showPanel", true);
        panel_title.text = "DICA";
        if (nivel_atual == FACIL)
        {
            panel_text.text = dicas_facil[questao_x_de_y];
        }
        else if (nivel_atual == MEDIO)
        {
            panel_text.text = dicas_medio[questao_x_de_y];
        }
        else
        {
            panel_text.text = dicas_dificil[questao_x_de_y];
        }
    }

    public void EsconderPanel()
    {

        if(showCerto){
            showCerto = false;
            ExibirTransicaoDeNivel();
        }
        else if(showErrado){
            showErrado = false;
            ExibirTransicaoDeNivel();
        }
        else{
            dica.Select();
        }

        Panel_anim.SetBool("showPanel", false);
        estado = JOGANDO;
    }

    private void CriarJanelaCerto ()
    {
        Informacoes.SetStatus(PROXPALAVRA);
        SalvarInfos();
        SceneManager.LoadScene("respostaCorreta");


    }

    private void CriarJanelaErrado ()
    {
        Informacoes.SetStatus(PROXPALAVRA);
        SalvarInfos();
        SceneManager.LoadScene("respostaErrada");

    }

    

    private void VerificarIntervalo(){
         if(pergunta_atual >= quantidade_facil && nivel_atual == FACIL){
            pergunta_atual += (4 - quantidade_facil);
        }

        if(pergunta_atual >= (quantidade_medio + 4) && nivel_atual == MEDIO){
            pergunta_atual += (3 - quantidade_medio);
        }
    }

    private void ExibirCertoOuErrado()
    {
        estado = JANELA;
        botao_panel.Select();

        if (nivel_atual == FACIL)
        {
            if (alternativa_escolhida == respostas_facil[questao_x_de_y])
        {
            showCerto = true;
            perguntas_bool[pergunta_atual] = 1;
            pergunta_atual += 1;
            VerificarIntervalo();
            CriarJanelaCerto();

        }
        else
        {
            showErrado = true;
            perguntas_bool[pergunta_atual] = 2;
            pergunta_atual += 1;
            VerificarIntervalo();
            CriarJanelaErrado();
        }
        }
        else if (nivel_atual == MEDIO)
        {

        if (alternativa_escolhida == respostas_medio[questao_x_de_y])
        {
            showCerto = true;
            perguntas_bool[pergunta_atual] = 1;
            pergunta_atual += 1;
            VerificarIntervalo();
            CriarJanelaCerto();

        }
        else
        {
            showErrado = true;
            perguntas_bool[pergunta_atual] = 2;
            pergunta_atual += 1;
            VerificarIntervalo();
            CriarJanelaErrado();
        }

        }
        else if(nivel_atual == DIFICIL)
        {

        if (alternativa_escolhida == respostas_dificil[questao_x_de_y])
        {
            showCerto = true;
            perguntas_bool[pergunta_atual] = 1;
            pergunta_atual += 1;
            VerificarIntervalo();
            CriarJanelaCerto();

        }
        else
        {
            showErrado = true;
            perguntas_bool[pergunta_atual] = 2;
            pergunta_atual += 1;
            VerificarIntervalo();
            CriarJanelaErrado();
        }
        }
        
    }

    private void ExibirTransicaoDeNivel()
    {
        if (AtualizarNivel() == MUDOU_NIVEL)
        {
            ExibirTelaNivel();
        }
        else if (VerificarFim() == 0)
        {
            AtualizarPerguntaTela();
        }
    }

    private void MostrarBarra() //quando são 9 perguntas tá dando problema
    {
        //vermelho
        //+125 verde
        //-75 amarelo
        
        TirarBarras();

        int qual_questao;
        if (pular_agora == SIM)
            qual_questao = questao_x_de_y - 1;
        else
            qual_questao = questao_x_de_y;

        if (nivel_atual == FACIL)
        {
            double conta = 1.0 * qual_questao / quantidade_facil;
            if(conta != 0)
            {
                //conta = Math.Ceiling(conta);
                if (quantidade_facil == 2 || quantidade_facil == 5 || quantidade_facil == 10)
                    barra_facil[Convert.ToInt32(conta * 10) - 1].SetActive(true);
                else if (quantidade_facil == 3 || quantidade_facil == 6 || quantidade_facil == 9)
                    barra_facil[Convert.ToInt32(conta * 10) + 8].SetActive(true);
                else if (quantidade_facil == 4)
                    barra_facil[(qual_questao*2) + 16].SetActive(true);
                else if (quantidade_facil == 8)
                    barra_facil[qual_questao + 16].SetActive(true);
                //else if (quantidade_facil == 7)
                    //barra_facil[Convert.ToInt32(conta * 10) + 23].SetActive(true);
            }
        }

        else if (nivel_atual == MEDIO)
        {
            double conta = 1.0 * qual_questao / quantidade_medio;
            if(conta != 0)
            {
                if (quantidade_medio == 2 || quantidade_medio == 5 || quantidade_medio == 10)
                    barra_medio[Convert.ToInt32(conta * 10) - 1].SetActive(true);
                else if (quantidade_medio == 3 || quantidade_medio == 6 || quantidade_medio == 9)
                    barra_medio[Convert.ToInt32(conta * 10) + 8].SetActive(true);
                else if (quantidade_medio == 4)
                    barra_medio[(qual_questao*2) + 16].SetActive(true);
                else if (quantidade_medio == 8)
                    barra_medio[qual_questao + 16].SetActive(true);
                //else if (quantidade_medio == 7)
                    //barra_medio[Convert.ToInt32(conta * 10) + 23].SetActive(true);
            }
        }

        else if (nivel_atual == DIFICIL)
        {
            double conta = 1.0 * qual_questao / quantidade_dificil;
            if(conta != 0)
            {
                if (quantidade_dificil == 2 || quantidade_dificil == 5 || quantidade_dificil == 10)
                    barra_dificil[Convert.ToInt32(conta * 10) - 1].SetActive(true);
                else if (quantidade_dificil == 3 || quantidade_dificil == 6 || quantidade_dificil == 9)
                    barra_dificil[Convert.ToInt32(conta * 10) + 8].SetActive(true);
                else if (quantidade_dificil == 4)
                    barra_dificil[(qual_questao*2) + 16].SetActive(true);
                else if (quantidade_dificil == 8)
                    barra_dificil[qual_questao + 16].SetActive(true);
                //else if (quantidade_dificil == 7)
                    //barra_dificil[Convert.ToInt32(conta * 10) + 23].SetActive(true);
            }
        }
    }

    private void TirarBarras()
    {
        for (int i = 0; i < 24; i++)
        {
            barra_facil[i].SetActive(false);
            barra_medio[i].SetActive(false);
            barra_dificil[i].SetActive(false);
        }
    }

    private int GeraNumero(int min, int max) {
        int valor = min;
        if (min < max - 1) {
            valor = min + random.Next(0, Int32.MaxValue) % (max - min);
        }
        return valor;
    }

    public void TocarPergunta(){
        audio_pergunta.Play();
    }
}
