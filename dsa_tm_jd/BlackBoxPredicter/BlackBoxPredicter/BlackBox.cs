using System;
using System.Collections.Generic;
using System.Linq;
using BlackBoxPredicter.Dto;

namespace BlackBoxPredicter
{
    public class BlackBox
    {
        public static IList<int> CalculateCycleTimes(IEnumerable<UserStory> userStories)
        {
            return userStories.Select(o => CalcCycleTime(o.Start, o.End))
                        .OrderBy(o => o)
                        .ToList();

            int CalcCycleTime(DateTime start, DateTime end) => (end - start).Days + 1;
        }

        public static IEnumerable<Tuple<int, double>> CalculatePercintles(IEnumerable<int> cycleTimes)
        {
            IList<Tuple<int, double>> result = new List<Tuple<int, double>>();

            var cycleTimesArray = cycleTimes.ToArray();

            for (int i = 0; i < cycleTimesArray.Length; i++)
            {
                result.Add(new Tuple<int, double>(cycleTimesArray[i], (i+1.0)/cycleTimesArray.Length));                                    
            }

            return result;
        }
    }
}