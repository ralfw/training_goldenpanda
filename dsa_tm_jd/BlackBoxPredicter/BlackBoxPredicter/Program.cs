using BlackBoxPredicter.Dto;

namespace BlackBoxPredicter
{
    class Program
    {
        static void Main(string[] args)
        {
            const float markerValue = 83.0f;
            var dates = DatesProvider.GetDates();

            var cycleTimes = BlackBox.CalculateCycleTimes(dates);
            var percentiles = BlackBox.CalculatePercentiles(cycleTimes);
            var histogram = BlackBox.CalculateHistogram(percentiles, markerValue);

            DisplayAdapter.Display(histogram);
        }
    }
}
