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
            var histogram = BlackBox.GenerateHistogramm(percintels);
            histogram.MarkerValue = 83.0f;

            histogram.MarkerIndex = BlackBox.DetectMarkerIndex(histogram);




            // DisplayAdapter.Display(histogram);
        }
    }
}
