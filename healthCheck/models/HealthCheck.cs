public class HealthCheck
{
    public List<Measurement> measurements {get;set;}
}

public class ScoreData 
{
    public int Score {get; set;}
    public string ErrorMessage {get; set;}
    public ScoreData(int score, string errorMessage)
    {
        Score = score;
        ErrorMessage = errorMessage;
    }
}
