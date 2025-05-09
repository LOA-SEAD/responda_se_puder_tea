using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using NativeWebSocket;  
using System.Reflection;

using System;



// <summary>
// Classe MessageSender: Gerencia o envio de mensagens para um servidor HTTP.
// Inclui métodos para criar formulários a partir de mensagens e enviar requisições HTTP.
// </summary>
public class MessageSender : MonoBehaviour
{
    //  Singleton 
    // Evita que a classe seja instanciada mais de uma vez.
    // Mantém a instância viva entre as cenas.
    private static MessageSender _instance;

    public static string cookieValue = "";
    public static MessageSender Instance {
        get {
            if (_instance == null)
            {
                GameObject go = new GameObject("MessageSender");
                _instance = go.AddComponent<MessageSender>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    

    
    private void Awake()
    {
        // Padrão singleton
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (_instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
    }

    
    
    

    
    // Refletion para deixar generico.
    // Cria um WWWForm a partir de uma mensagem genérica.
    // Utiliza reflexão para pegar os campos públicos da mensagem e adicioná-los ao formulário.
    private WWWForm CreateFormFromMessage<T>(T message) where T : Message
    {
        WWWForm form = new WWWForm();
        // pega campos publicos.
        FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Instance);
        foreach (FieldInfo field in fields)
        {
            object value = field.GetValue(message);
            if (value != null)
            {
                if(value.GetType().IsArray)
                {
                    string fieldType = field.FieldType.ToString();
                    Debug.Log("Tipo do campo: " + fieldType);

                    for(int i = 0; i < ((Array)value).Length; i++)
                    {
                        //caso seja um array, adiciona o campo ao form.
                        form.AddField(field.Name , ((Array)value).GetValue(i).ToString());
                        Debug.Log("Adicionando campo ao form: " + field.Name + "[" + i + "] = " + ((Array)value).GetValue(i).ToString());
                    }
                    
                }
                else{
                    //caso não seja um array, adiciona o campo ao form.
                    form.AddField(field.Name, value.ToString());
                    Debug.Log("Adicionando campo ao form: " + field.Name + " = " + value.ToString());
                }
                
            }
            
            
        }
        return form;
    }

    
    //<summary>
    //método chamado para enviar uma mensagem para o servidor HTTP.
    // ...
    public IEnumerator Send<T>(T message, string url) where T : Message
    {
        WWWForm form = CreateFormFromMessage(message);
        Debug.Log("Enviando requisição para " + url);
        yield return StartCoroutine(SendForm(form, url));
    }

    // Envia um WWWForm para o servidor HTTP.
    // Utiliza UnityWebRequest para enviar a requisição.
    public IEnumerator SendForm(WWWForm form, string url)
    {
        using (UnityWebRequest request = UnityWebRequest.Post(url, form))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Erro na requisição HTTP (form): " + request.error);
            }
            else
            {
                Debug.Log("Deu certo a requisição do form " + url);
                Debug.Log("Resposta HTTP (form): " + request.downloadHandler.text);
                Debug.Log("Status code: " + request.responseCode);
                // Se precisar capturar o cookie:
                string cookieValue = request.GetResponseHeader("Set-Cookie");
                Debug.Log("Cookie retornado pelo servidor: " + cookieValue);
            }
        }
    }


}
