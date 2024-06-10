using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    [SerializeField] GameObject mask;
    Transform t;
    Transform b;


    private void Start()
    {
        t = spotlight.transform;
        b = btn.transform;
    }
    public void GoToNextStep()
    {
        switch (step)
        {
            case 0:
                spotlight.transform.position = new Vector3(t.position.x, -1.2f, 0) ;
                spotlight.transform.localScale = new Vector3(7.23f, 2f, 1);
                
                btn.transform.localScale = new Vector3(1, 1, 1);
                btn.transform.localPosition = new Vector3(320, -124, 0);
                //btn.transform.localPosition = new Vector3(320, -124, 0);
                txt.text = "Aqui estão as possíveis respostas para as perguntas.";
                
                break;
            case 1:
                txt.text = "Escolha a primeira opção clicando no quadrado.";
                btn.transform.position = checks[0].transform.position;
                btn.transform.localScale = new Vector3(1, 1, 1);
                btn.GetComponent<Image>().enabled = false;
                break;

            case 2:
                txt.text = "Muito bem!";
                btn.transform.localScale = new Vector3(1, 1, 1);
                btn.transform.localPosition = new Vector3(320, -124, 0);
                btn.GetComponent<Image>().enabled = true;
                break;
            case 3:
                spotlight.transform.position = new Vector3(t.position.x, -1.2f, 0);
                spotlight.transform.localScale = new Vector3(7.23f, 2f, 1);

                btn.transform.localScale = new Vector3(1, 1, 1);
                btn.transform.localPosition = new Vector3(320, -124, 0);
                //btn.transform.localPosition = new Vector3(320, -124, 0);
                txt.text = "Leia a pergunta e escolha a que você acha que seja a resposta!";
                txtPergunta.text = "Qual é a cor vermelha?";
                checks[2].GetComponent<Image>().color = Color.red;
                perguntas[2].GetComponent<Text>().color = Color.red;
                mask.SetActive(false);
                btn.GetComponent<Image>().enabled = false;
                for (int i = 0; i < checks.Count; i++)
                {
                    buttons[i].SetActive(true);
                    buttons[i].transform.position = checks[i].transform.position;
                }
                spotlight.transform.localScale = new Vector3(17.23f, 12f, 1);
                break;
            case 4:
                txt.text = "Muito bem!\n Você já aprendeu a escolher a sua resposta!";
                btn.transform.localScale = new Vector3(1, 1, 1);
                btn.transform.localPosition = new Vector3(320, -124, 0);
                btn.GetComponent<Image>().enabled = true;
                break;
        }

        step++;

    }
}
