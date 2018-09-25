using System;
using System.Collections.Generic;
using System.Linq;
using BlackBoxPredicter.Dto;

namespace BlackBoxPredicter
{
    public class BlackBox
    {
        internal static IList<int> CalculateCycleTimes(IEnumerable<UserStory> userStories)
        {
            return userStories.Select(o => CalcCycleTime(o.Start, o.End))
                        .OrderBy(o => o)
                        .ToList();

            int CalcCycleTime(DateTime start, DateTime end) => (end - start).Days + 1;
        }

        internal static IEnumerable<Tuple<int, double>> FindHighestPercentils(IEnumerable<Tuple<int, double>> percentils)
        {
            IList<Tuple<int, double> >  result = new List<Tuple<int, double>>();

            foreach (var percentil in percentils)
            {
                if (result.All(o => o.Item1 != percentil.Item1))
                {
                    result.Add(new Tuple<int, double>(percentil.Item1,percentil.Item2));
                }

                var itm = result.First(o => o.Item1 == percentil.Item1);
                if (itm.Item2 < percentil.Item2)
                {
                    result.Remove(itm);
                    result.Add(new Tuple<int, double>(percentil.Item1, percentil.Item2));                    
                }
            }

            return result;
        }

        internal static IEnumerable<Tuple<int, double>> CalculatePercentiles(IEnumerable<int> cycleTimes)
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