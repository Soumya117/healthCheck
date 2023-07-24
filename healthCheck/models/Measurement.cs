using System.Linq;

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

public interface MeasurementScoreMapping
{
    public List<ScoreMapping> getScoreMapping() => throw new NotImplementedException() ;
    int getScore(int value) => throw new NotImplementedException() ;

    private static int getMinimumValidValue(List<ScoreMapping> scoreMappingList)
    {
        return scoreMappingList.OrderByDescending(i => i.MinValue).ToList()[0].MinValue;
    }
    private static int getMaximumValidValue(List<ScoreMapping> scoreMappingList)
    {
        return scoreMappingList.OrderByDescending(i => i.MaxValue).ToList()[0].MaxValue;
    }

    public static int calculateScoreFromMapping(int value, List<ScoreMapping> scoreMappingList, string measurementType)
    {
        foreach(ScoreMapping scoreMap in scoreMappingList)
        {
           if (value > scoreMap.MinValue  && value <= scoreMap.MaxValue)
            {
              return scoreMap.Score;
            }
        }
        string errorMessage = string.Format("Value {0} for {1} is invalid. Please enter a value between {2} and {3}.",
             value, measurementType, getMinimumValidValue(scoreMappingList), getMaximumValidValue(scoreMappingList)
        );
        throw new InvalidValueException(errorMessage);
    }
}

public class Temperature : MeasurementScoreMapping
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
        return MeasurementScoreMapping.calculateScoreFromMapping(value, getScoreMapping(), "TEMP");
    }
}

public class HeartRate : MeasurementScoreMapping
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
        return MeasurementScoreMapping.calculateScoreFromMapping(value, getScoreMapping(), "HR");
    }
}

public class RespitoryRate : MeasurementScoreMapping
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
        return MeasurementScoreMapping.calculateScoreFromMapping(value, getScoreMapping(), "RR");
    }

} 