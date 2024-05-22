using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Networking;

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
    public Button dica;
    public Button pular;

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

    int quantidade_facil;
    int quantidade_medio;
    int quantidade_dificil;
    string[] perguntas_facil = new string[11];
    string[] perguntas_medio = new string[11];
    string[] perguntas_dificil = new string[11];
    int[] respostas_facil = new int[11];
    int[] respostas_medio = new int[11];
    int[] respostas_dificil = new int[11];
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
    int pular_agora;

    int questao_x_de_y;
    int selecionou5050 = NAO;
    int selecionou_pular = NAO;
    int nivel_atual = FACIL;
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
    
    System.Random random = new System.Random();

    //Utilizados para o double touch
    bool one_click = false;
    bool timer_running;
    float timer_for_double_click;

    float delay = 5.0f;
    
    void Start()
    {
        Debug.Log("[Jogo] Start - Inicio");
        CarregaDados.Load(this);
        Debug.Log("[Jogo] Quantidade itens: " + CarregaDados.listaDados.Count);
        Debug.Log("[Jogo] Start - Fim");
        
        #if UNITY_ANDROID
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            //Screen.fullScreen = false;
        #endif
        //TirarBarras();
        AtualizarVolume();

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
        
    }

    void Update()
    {
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
    }
    
    private void InicializarJogo()
    {
        // carregaDados.Load();
        dificuldade_tela.text = "NÍVEL FÁCIL";
        pular_agora = NAO;
        Informacoes.SetStatusPular(NAO);
        pontuacao = 0;
        SortearPerguntas();
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

    private void MisturarRespostas()
    {
        for (int i = 0; i < quantidade_facil + 1; i++)
        {
            string aux_s;
            string aux_audio;
            // int troca1 = random.Next(0, Int32.MaxValue) % 4; // Gera número entre 0 e 3
            // int troca2 = random.Next(0, Int32.MaxValue) % 4; // Gera número entre 0 e 3
            int troca1 = GeraNumero(0, 4);
            int troca2 = GeraNumero(0, 4);
            if (respostas_facil[i] == troca1)
            {
                respostas_facil[i] = troca2;
            }
            else if (respostas_facil[i] == troca2)
            {
                respostas_facil[i] = troca1;
            }
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
            int troca1 = GeraNumero(0, 4);
            int troca2 = GeraNumero(0, 4);
            if (respostas_medio[i] == troca1)
            {
                respostas_medio[i] = troca2;
            }
            else if (respostas_medio[i] == troca2)
            {
                respostas_medio[i] = troca1;
            }
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
            int troca1 = GeraNumero(0, 4);
            int troca2 = GeraNumero(0, 4);
            if (respostas_dificil[i] == troca1)
            {
                respostas_dificil[i] = troca2;
            }
            else if (respostas_dificil[i] == troca2)
            {
                respostas_dificil[i] = troca1;
            }
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
        Informacoes.SetPontosGanhos(pontos_ganhos);

        Informacoes.SetNumeroQuestao(questao_x_de_y);
    }
    
    private void PegarInfosSalvas()
    {
        if(Informacoes.GetStatus() == STATUSOPCOES || Informacoes.GetStatus() == PROXPALAVRA){
            questao_x_de_y = Informacoes.GetNumeroQuestao();
        }
        else{
            questao_x_de_y = -1;
        }
        pular_agora = NAO;
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
        audios_perguntas = Informacoes.GetAudiosPerguntas();
        audios_alternativas = Informacoes.GetAudiosAlternativas();
        audios_dicas = Informacoes.GetAudiosDicas();
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
        botao_panel_sim.Select();
    }

    public void EsconderPanelConfirmar(){
        Panel_confirmar_anim.SetBool("showPanel", false);
    }
    
    public void ConfirmarAlternativa()
    {
        confirmar.interactable = false;
        SomarPontuacao();
        ExibirCertoOuErrado();
        EsconderPanelConfirmar();
    }

    public void NaoConfirmarAlternativa(){
        EsconderPanelConfirmar();
        // alternativas[alternativa_escolhida].Select(); Não precisa falar duas vezes
    }

    public void Funcao5050()
    {
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
            }
        #else
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
            audio_dica.Play();
            botao_panel.Select();
            CriarJanelaDica();
        #endif
    }

    public void FuncaoPular()
    {
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
            Informacoes.SetStatusPular(SIM);
            selecionou_pular = SIM;
            confirmar.interactable = false;
            pular.interactable = false;
            pular_agora = SIM;
            PegarProximaQuestao();
            AtualizarPerguntaTela();
            alternativas[0].Select();
        #endif
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
        else
        {
            if (nivel_atual == FACIL && questao_x_de_y >= quantidade_facil)
            {
                Informacoes.SetStatus(MEIO);
                nivel_atual = MEDIO;
                SalvarInfos();
                return MUDOU_NIVEL;
            }
            else if (nivel_atual == MEDIO && questao_x_de_y >= quantidade_medio)
            {
                Informacoes.SetStatus(MEIO);
                nivel_atual = DIFICIL;
                SalvarInfos();
                return MUDOU_NIVEL;
            }
            SalvarInfos();
            return NAO_MUDOU_NIVEL;
        }
        
    }

    private void ExibirNaTela()
    {
        for(int i = 0; i < 4; i++)
            risco[i].SetActive(false);

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
        if (pular_agora == SIM)
            numero_questao_tela.text = "Questão " + (questao_x_de_y).ToString() + " de " + quantidade_facil.ToString();
        else
            numero_questao_tela.text = "Questão " + (questao_x_de_y + 1).ToString() + " de " + quantidade_facil.ToString();
        pergunta_tela.text = perguntas_facil[questao_x_de_y];
        alternativa_correta = respostas_facil[questao_x_de_y];
        
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
        if (pular_agora == SIM)
            numero_questao_tela.text = "Questão " + (questao_x_de_y).ToString() + " de " + quantidade_medio.ToString();
        else
            numero_questao_tela.text = "Questão " + (questao_x_de_y + 1).ToString() + " de " + quantidade_medio.ToString();
        pergunta_tela.text = perguntas_medio[questao_x_de_y];
        alternativa_correta = respostas_medio[questao_x_de_y];
        
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
        if (pular_agora == SIM)
            numero_questao_tela.text = "Questão " + (questao_x_de_y).ToString() + " de " + quantidade_dificil.ToString();
        else
            numero_questao_tela.text = "Questão " + (questao_x_de_y + 1).ToString() + " de " + quantidade_dificil.ToString();
        pergunta_tela.text = perguntas_dificil[questao_x_de_y];
        alternativa_correta = respostas_dificil[questao_x_de_y];
            
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
        if (selecionou5050 == SIM)
        {
            ajuda5050.interactable = false;
        }
        if (selecionou_pular == SIM)
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
                SceneManager.LoadScene("Fim");
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
                SceneManager.LoadScene("Fim");
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

    private void ExibirCertoOuErrado()
    {
        estado = JANELA;
        botao_panel.Select();
        if (alternativa_escolhida == respostas_facil[questao_x_de_y])
        {
            showCerto = true;
            CriarJanelaCerto();
        }
        else
        {
            showErrado = true;
            CriarJanelaErrado();
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
