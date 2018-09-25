using System;
using System.Collections.Generic;
using System.Linq;
using BlackBoxPredicter.Dto;

namespace BlackBoxPredicter
{
   
    class Program
    {
        static void Main(string[] args)
        {
            var dates = DatesProvider.GetDates();
            var cycleTimes = BlackBox.CalculateCycleTimes(dates);

            var tempData = GetTempData(cycleTimes);
            // TODO
            DisplayAdapter.Display(tempData);
        }

        private static IEnumerable<Tuple<int, double>> GetTempData(IEnumerable<int> cycleTimes)
        {
            var temp = 0.1;
            return cycleTimes.Select(t => new Tuple<int, double>(t, temp += 0.2 ));
        }
    }
}
