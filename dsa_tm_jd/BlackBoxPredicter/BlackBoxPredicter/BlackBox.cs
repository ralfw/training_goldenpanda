using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackBoxPredicter
{
    public class BlackBox
    {
        public static IList<int> CalculateCycleTimes(IList<Tuple<DateTime, DateTime>> dates)
        {
            return dates.Select(o => CalcCycleTime(o.Item1, o.Item2))
                        .OrderBy(o => o)
                        .ToList();

            int CalcCycleTime(DateTime start, DateTime end) => (end - start).Days + 1;
        }
    }
}