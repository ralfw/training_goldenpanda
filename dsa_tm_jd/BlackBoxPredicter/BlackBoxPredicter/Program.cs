using System.Collections.Generic;
using BlackBoxPredicter.Dto;

namespace BlackBoxPredicter
{
    class Program
    {
        static void Main(string[] args)
        {
            float markerValue = 83.0f;

            var dates = DatesProvider.GetDates();
            var cycles = BlackBox.CalculateCycleTimes(dates);
            var percintels = BlackBox.CalculatePercentiles(cycles);
            var histogramEntries = BlackBox.GenerateHistogramEntries(percintels);

            Histogram histogram = BlackBox.GenerateHistogramm(histogramEntries, markerValue);

            DisplayAdapter.Display(histogram);
        }
    }
}
