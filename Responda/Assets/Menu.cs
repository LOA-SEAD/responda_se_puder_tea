using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    const int ORIGEM_MENU = 0;
    const int ORIGEM_JOGO = 1;

    public Button botao_instrucoes;
    public Button botao_jogar;
    public Button botao_opcoes;
    public Button botao_creditos;
    public Button botao_sair;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("[Menu] Start - Inicio");
        CarregaDados.Load(this);
        Debug.Log("[Menu] Quantidade itens: " + CarregaDados.listaDados.Count);

        #if UNITY_ANDROID
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            //Screen.fullScreen = false;
        #endif
        Informacoes.SetOrigem(ORIGEM_MENU);
        Debug.Log("[Menu] Start - Fim");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HighlightMenu (int botao){
        #if UNITY_ANDROID
            if(botao == 0){
                botao_instrucoes.Select();
            }
            if(botao == 1){
                botao_jogar.Select();
            }
            if(botao == 2){
                botao_opcoes.Select();
            }
            if(botao == 3){
                botao_creditos.Select();
            }
            if(botao == 4){
                botao_sair.Select();
            }
        #endif
    }
}
