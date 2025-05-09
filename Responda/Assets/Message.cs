using System;
using UnityEngine;


// <summary>
// Classe Message: Representa uma mensagem genérica que pode ser enviada.
// Pode ser estendida para criar mensagens específicas, como SaveRanking ou EntrarSessao.
// </summary>

[Serializable]
public class Message
{
    
}

[Serializable]
public class EntrarSessao : Message
{
    public string secret;

    public string messageType;
    

    public EntrarSessao(string messageType,  string secret)
    {
        this.messageType = messageType;
        this.secret = secret;
        
    }
}

// Exemplo de mensagem de chat
[Serializable]
public class MensagemChat : Message
{
    public string texto;
    public int sessionId;
    public int gameId;
  
    

    public MensagemChat( int sessionId, int gameId, string texto)
    {
        this.sessionId = sessionId;
        this.gameId = gameId;
        this.texto = texto;
    }
}




[Serializable]
public class SaveRankingStatsMessage : Message
{
    public int exportedResourceId;
    public int score;

    public SaveRankingStatsMessage(int exportedResourceId, int score)
    {
        this.exportedResourceId = exportedResourceId;
        this.score = score;
    }
}



[Serializable]
public class saveChallengeStatsMessage : Message
{
    public int exportedResourceId;
    public string question;
    public string correctAnswer;
    public int challengeId;
    public string[] choices = new string[4];
    public int answer;
    public bool win;
    public int levelSize;
    public int levelId;
    public string levelName;
    public string challengeType;

    public saveChallengeStatsMessage(
        int exportedResourceId,
        string question,
        string correctAnswer,
        int challengeId,
        string[] choices,
        int answer,
        bool win,
        int levelSize,
        int levelId,
        string levelName,
        string challengeType)
    {
        this.exportedResourceId = exportedResourceId;
        this.question = question;
        this.correctAnswer = correctAnswer;
        this.levelName = levelName;
        this.choices = choices;
        this.challengeId = challengeId;
        this.answer = answer;
        this.win = win;
        this.levelSize = levelSize;
        this.levelId = levelId;
        this.challengeType = challengeType;
    }
    
}




