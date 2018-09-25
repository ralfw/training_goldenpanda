namespace BlackBoxPredicter
{
   
    class Program
    {
        static void Main(string[] args)
        {
            var dates = DatesProvider.GetDates();
            var cycles = BlackBox.CalculateCycleTimes(dates);
            var percintels = BlackBox.CalculatePercentiles(cycles);
            DisplayAdapter.Display(percintels);
        }
    }
}
