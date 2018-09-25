using System.Collections.Generic;
using BlackBoxPredicter.Dto;

namespace BlackBoxPredicter
{
   
    class Program
    {
        static void Main(string[] args)
        {
            var dates = DatesProvider.GetDates();
            var cycles = BlackBox.CalculateCycleTimes(dates);
            var percintels = BlackBox.CalculatePercentiles(cycles);
            var histogram = BlackBox.GenerateHistogramm(cycles, percintels);
            {
                new HistogramEntry(2, 2, 0.125),
                new HistogramEntry(3, 3, 0.625)
            };
            DisplayAdapter.Display(testData);
        }
    }
}
