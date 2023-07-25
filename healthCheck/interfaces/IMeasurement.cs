using System;
using System.Collections.Generic;
using System.Linq;

namespace healthCheck.interfaces
{
    public interface IMeasurement
    {
        public List<ScoreMapping> getScoreMapping() => throw new NotImplementedException() ;
        int getScore(int value) => throw new NotImplementedException() ;

        private static int getMinimumValidValue(List<ScoreMapping> scoreMappingList)
        {
            return scoreMappingList.OrderByDescending(i => -i.MinValue).ToList()[0].MinValue + 1;
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
}
