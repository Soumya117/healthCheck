using System.Net;

public class ScoreResponse
{
    public int Score {get; set;}
    public string ErrorMessage{get; set;}

    public HttpStatusCode StatusCode{get; set;}
    public ScoreResponse(int score, HttpStatusCode statusCode, string errorMessage)
    {
        Score = score;
        StatusCode = statusCode;
        ErrorMessage = errorMessage;
    }
}