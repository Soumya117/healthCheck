using System.Net;

public class Service{
    public static ScoreResponse createScoreResponse(HealthCheck healthCheck)
    {
        List<Measurement> measurements = healthCheck.measurements;
        int totalScore = 0;
        string assemblyName = typeof(MeasurementScoreMapping).Assembly.GetName().Name;
        List<string>processedMeasurementType = new List<string>();

        if (measurements.Count == 0)
        {
            return new ScoreResponse(-1, HttpStatusCode.NoContent,"No measurements provided");
        }

        foreach (Measurement measurement in measurements)
        {
            string measurementObjectName = Utilities.ToEnumString(measurement.type) + ", " + assemblyName;
            var objectType = Type.GetType(measurementObjectName);
            dynamic measurementTypeObject = Activator.CreateInstance(objectType) as MeasurementScoreMapping;
            
            try {
                if (processedMeasurementType.Contains(measurementObjectName))
                {
                    return new ScoreResponse(-1, HttpStatusCode.BadRequest, "Duplicate measurement types provided");
                }

                int score = measurementTypeObject.getScore(measurement.value);
                
                totalScore += score;
                processedMeasurementType.Add(measurementObjectName);
            }
            catch (InvalidValueException e) {
                return new ScoreResponse(-1, HttpStatusCode.BadRequest, e.Message);
            }
        }
        return new ScoreResponse(totalScore, HttpStatusCode.OK, "");
    }
}