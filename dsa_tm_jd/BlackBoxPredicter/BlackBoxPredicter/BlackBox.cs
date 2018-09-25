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
    }
}