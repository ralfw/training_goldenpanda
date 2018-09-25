using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackBoxPredicter
{
    public class BlackBox
    {
        public static IList<int> CalculateCycles(IList<Tuple<DateTime, DateTime>> dates)
        {
            return dates.Select(o => (o.Item2 - o.Item1).TotalDays+1)
                        .Select(Convert.ToInt32).OrderBy(o => o).ToList(); 

        }

    }
}
