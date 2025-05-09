using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;

// <summary>
// Classe Login_Handler: Gerencia o processo de login do usuário.
// </summary>

public class Login_Handler : MonoBehaviour
{
    public TMP_InputField usernameField;
    public TMP_InputField passwordField;

    private MessageSender messageSender;

    private string cookieValue = "";
    

    // Esse método será chamado quando o usuário clicar no botão "Login"
    public void OnLoginButtonClicked()
    {
        string username = usernameField.text;
        string password = passwordField.text;

        Debug.Log("Username: " + username + ", Password: " + password);

        CarregaDados.Load(this);

        StartCoroutine(LoginCoroutine(username, password));

        
    }

    
    private IEnumerator LoginCoroutine(string username, string password)
    {
       
       string url =  CarregaDados.url + "/group/isLogged?username="+ username+ "&password=" +password;

        
        WWWForm form = new WWWForm();
        form.AddField("choice", "login");

        Debug.Log("Enviando requisição para " + url);
        
        yield return StartCoroutine(MessageSender.Instance.SendForm(form, url));
        
       
        SceneManager.LoadScene("Menu");
    }
      

    
}
