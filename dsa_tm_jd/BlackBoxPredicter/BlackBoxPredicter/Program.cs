using System.Linq;
using BlackBoxPredicter.Dto;

namespace BlackBoxPredicter
{
    class Program
    {
        static void Main(string[] args)
        {
            var dates = DatesProvider.GetDates();
            var cycles = BlackBox.CalculateCycleTimes(dates);
            var percentiles = BlackBox.CalculatePercentiles(cycles);
            var histogram = BlackBox.GenerateHistogramm(percentiles);

            var histogram2 = new Histogram()
            {
                Entries = histogram.ToList(),
                MarkerIndex = 2,
                MarkerValue = 83.3f
            };

            DisplayAdapter.Display(histogram2);
        }
    }
}
