using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CursosBlock : MonoBehaviour
{
    // Start is called before the first frame update
    public Button[] buttons;
    private Navigation[] originalNavigations;

    public Slider[] sliders;

    private Navigation[] originalSliderNavigations;

    void Start()
    {
        // Armazena as configurações de navegação originais dos botões
        originalNavigations = new Navigation[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            originalNavigations[i] = buttons[i].navigation;
            //DisableNavigation(buttons[i]);
        }

        originalSliderNavigations = new Navigation[sliders.Length];
        for (int i = 0; i < sliders.Length; i++)
        {
            originalSliderNavigations[i] = sliders[i].navigation;
            //DisableNavigation(sliders[i]);
        }
    }

    
    void DisableForNavigation()
    {

        for (int i = 0; i < buttons.Length; i++)
        {
            DisableNavigation(buttons[i]);
        }
    }

    void DisableForNavigationSliders(){
        for (int i = 0; i < sliders.Length; i++)
        {
            DisableNavigation(sliders[i]);
        }
    }

    void DisableNavigation(Selectable button){

        Navigation nav = button.navigation;
        nav.mode = Navigation.Mode.None; // Desativa a navegação do botão
        button.navigation = nav;

    }

    public void EnableNavigation()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].navigation = originalNavigations[i]; // Restaura as configurações originais
        }

        for (int i = 0; i < sliders.Length; i++)
        {
            sliders[i].navigation = originalSliderNavigations[i];
        }
    }

    void Update()
    {
        if(Informacoes.GetCursosBlock() == 1)
        {
            Cursor.lockState = CursorLockMode.Locked; // Trava o cursor no centro da tela
            Cursor.visible = false; // Oculta o cursor
            EnableNavigation();
        }else if(Informacoes.GetCursosBlock() == 2)
        {
            DisableForNavigation();
            DisableForNavigationSliders();
        }
        
        else{
            
            EnableNavigation();
            Cursor.lockState = CursorLockMode.None; // Libera o cursor
            Cursor.visible = true; // Mostra o cursor
        }
    }

    void BloquearTeclado(){

        if(Input.GetKey(KeyCode.Tab)){
            return;
        }
        if(Input.GetKeyDown(KeyCode.Tab)){
            return;
        }
        if(Input.GetKeyUp(KeyCode.Tab)){
            return;
        }
    }

}
