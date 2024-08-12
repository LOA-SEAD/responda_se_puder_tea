using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    int step = 0;
    [SerializeField] GameObject spotlight;
    [SerializeField] GameObject btn;
    [SerializeField] TextMeshProUGUI txt;
    [SerializeField] List<GameObject> checks;
    [SerializeField] List<GameObject> buttons;
    [SerializeField] List<Text> perguntas;
    [SerializeField] Text txtPergunta;
    [SerializeField] List<Button> Dica5050;
    [SerializeField] GameObject mask;
    Transform t;
    Transform b;

    public Animator Panel_anim;
    public TextMeshProUGUI panel_title;
    public TextMeshProUGUI panel_text;

    private void Start()
    {
        t = spotlight.transform;
        b = btn.transform;
        foreach (GameObject b in checks)
        {
            b.GetComponent<Button>().interactable = false ;
        }

    }

    public void MostrarDica()
    {
        Panel_anim.SetBool("showPanel", true);
        panel_text.gameObject.SetActive(true);
        panel_text.text = "Um quilograma possui 1000 gramas!";

    }

    public void EscondeDica()
    {
        Panel_anim.SetBool("showPanel", false);
    }

    public void Mostrar5050()
    {
        for(int i = 2; i <= 3; i++)
        {

            checks[i].GetComponent<Button>().interactable = false;

        }

    }
    public void GoToNextStep()
    {
        int l = 1;
        switch (step)
        {
            case 0:
                spotlight.transform.position = new Vector3(t.position.x, -1.2f, 0) ;
                spotlight.transform.localScale = new Vector3(7.23f, 2f, 1);
                
                btn.transform.localScale = new Vector3(1, 1, 1);
                //btn.transform.localPosition = new Vector3(320, -158, 0);
                //btn.transform.localPosition = new Vector3(320, -124, 0);
                txtPergunta.text = "Aqui estão as possíveis respostas para as perguntas.";
                
                break;
            case 1:
                txtPergunta.text = "Escolha a primeira opção clicando no quadrado.";
                //btn.transform.position = checks[0].transform.position;
                btn.transform.localScale = new Vector3(1, 1, 1);
                btn.SetActive(false);
                checks[0].GetComponent<Button>().interactable = true;
                break;

            case 2:
                txtPergunta.text = "Muito bem!";
                btn.transform.localScale = new Vector3(1, 1, 1);
                
                btn.GetComponent<Image>().enabled = true;
                btn.SetActive(true);
                checks[0].GetComponent<Button>().interactable = false;
                break;
            case 3:
                spotlight.gameObject.SetActive(false);

                btn.transform.localScale = new Vector3(1, 1, 1);
                
                //btn.transform.localPosition = new Vector3(320, -124, 0);
                txtPergunta.text = "Leia a pergunta e escolha a que você acha que seja a resposta!\n";
                txtPergunta.text = txtPergunta.text + "Quantos lados tem um triângulo?";

                
                foreach (GameObject b in checks)
                {
                    b.GetComponent<Button>().interactable = true;
                    b.transform.GetChild(0).GetComponent<Text>().text = l.ToString() ;
                    l++;
                }
                mask.SetActive(false);
                btn.SetActive(false);
                spotlight.transform.localScale = new Vector3(17.23f, 12f, 1);

                panel_text.gameObject.SetActive(true);
                break;
            case 4:
                foreach (GameObject b in checks)
                {
                    b.GetComponent<Button>().interactable = false;
                    b.transform.GetChild(0).GetComponent<Text>().text = l.ToString();
                    l++;
                }
                txtPergunta.text = "Muito bem!\n Você já aprendeu a escolher a sua resposta!";
                btn.transform.localScale = new Vector3(1, 1, 1);
                
                btn.SetActive(true);
                break;

            case 5:
                spotlight.gameObject.SetActive(false);

                btn.transform.localScale = new Vector3(1, 1, 1);
                
                txtPergunta.text = "Se tiver dúvidas, basta clicar nos botões de ajuda abaixo!\n";

                foreach (Button b in Dica5050)
                {
                    b.gameObject.SetActive(true);
                    b.interactable = false;
                }


                foreach (GameObject b in checks)
                {
                    b.GetComponent<Button>().interactable = false;
                    b.transform.GetChild(0).GetComponent<Text>().text = "";
                    
                }
                mask.SetActive(true);

                spotlight.transform.localScale = new Vector3(17.23f, 12f, 1);

                panel_text.gameObject.SetActive(true);
                btn.SetActive(true);
                break;
            case 6:

                spotlight.gameObject.SetActive(false);

                btn.transform.localScale = new Vector3(1, 1, 1);
                
                //btn.transform.localPosition = new Vector3(320, -124, 0);
                txtPergunta.text = "Quantos quilometros tem em um metro?";


                foreach (GameObject b in checks)
                {
                    b.GetComponent<Button>().interactable = true;
                    
                    l++;
                }
                checks[0].transform.GetChild(0).GetComponent<Text>().text = "60";
                checks[1].transform.GetChild(0).GetComponent<Text>().text = "1.000";
                checks[2].transform.GetChild(0).GetComponent<Text>().text = "1.024";
                checks[3].transform.GetChild(0).GetComponent<Text>().text = "10.000";
                mask.SetActive(false);
                btn.SetActive(false);
                spotlight.transform.localScale = new Vector3(17.23f, 12f, 1);

                foreach (Button b in Dica5050)
                {
                    b.gameObject.SetActive(true);
                    b.interactable = true;
                }
                panel_text.gameObject.SetActive(true);
                break;

            case 7:
                foreach (Button b in Dica5050)
                {
                    b.gameObject.SetActive(true);
                    b.interactable = false;
                }


                foreach (GameObject b in checks)
                {
                    b.GetComponent<Button>().interactable = false;
                    b.transform.GetChild(0).GetComponent<Text>().text = "";

                }

                txtPergunta.text = "Ótimo! Você já pegou o jeito!\n Já pode finalizar o Tutorial!";
                btn.SetActive(true);
                break;
            case 8:
                SceneManager.LoadScene("Menu");
                break;
        }

        step++;

    }
}
