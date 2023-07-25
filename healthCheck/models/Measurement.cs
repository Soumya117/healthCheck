using System.Linq;
using healthCheck.interfaces;

public class Measurement
{
    public MeasurementType type { get; set; }
    public int value { get; set; }
}

public class ScoreMapping{
    public int MinValue;
    public int MaxValue;
    public int Score;

    public ScoreMapping(int minValue, int maxValue, int score)
    {
        MinValue = minValue;
        MaxValue = maxValue;
        Score = score;
    }
}

public class Temperature : IMeasurement
{
    public List<ScoreMapping> getScoreMapping()
    {
        List<ScoreMapping> scoreMappingList  = new List<ScoreMapping>();
        scoreMappingList.Add(new ScoreMapping(31, 35, 3));
        scoreMappingList.Add(new ScoreMapping(35, 36, 1));
        scoreMappingList.Add(new ScoreMapping(36, 38, 0));
        scoreMappingList.Add(new ScoreMapping(38, 39, 1));
        scoreMappingList.Add(new ScoreMapping(39, 42, 2));
        return scoreMappingList;
    }

    public int getScore(int value)
    {
        return IMeasurement.calculateScoreFromMapping(value, getScoreMapping(), "TEMP");
    }
}

public class HeartRate : IMeasurement
{
      public List<ScoreMapping> getScoreMapping()
    {
        List<ScoreMapping> scoreMappingList  = new List<ScoreMapping>();
        scoreMappingList.Add(new ScoreMapping(25, 40, 3));
        scoreMappingList.Add(new ScoreMapping(40, 50, 1));
        scoreMappingList.Add(new ScoreMapping(50, 90, 0));
        scoreMappingList.Add(new ScoreMapping(90, 110, 1));
        scoreMappingList.Add(new ScoreMapping(110, 130, 2));
        scoreMappingList.Add(new ScoreMapping(130, 220, 3));
        return scoreMappingList;
    }
    

    public int getScore(int value)
    {
        return IMeasurement.calculateScoreFromMapping(value, getScoreMapping(), "HR");
    }
}

public class RespitoryRate : IMeasurement
{
      public List<ScoreMapping> getScoreMapping()
    {
        List<ScoreMapping> scoreMappingList  = new List<ScoreMapping>();
        scoreMappingList.Add(new ScoreMapping(3, 8, 3));
        scoreMappingList.Add(new ScoreMapping(8, 11, 1));
        scoreMappingList.Add(new ScoreMapping(11, 20, 0));
        scoreMappingList.Add(new ScoreMapping(20, 24, 2));
        scoreMappingList.Add(new ScoreMapping(24, 60, 3));
        return scoreMappingList;
    }

    public int getScore(int value)
    {
        return IMeasurement.calculateScoreFromMapping(value, getScoreMapping(), "RR");
    }

} 
