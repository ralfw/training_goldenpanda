namespace bbp
{
    class Program
    {
        #region Private methods

        static void Main(string[] args)
        {
            var testData = DataProvider.GetUserStories();

            var result = Predictor.Predict(testData);
            foreach (var r in result)
                ConsoleAdapter.Output($"{r.Duration}: {r.AccumulatedPercentile:F3} - {r.Frequency}\n");
        }

        #endregion
    }
}