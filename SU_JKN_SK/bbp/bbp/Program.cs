namespace bbp
{
    class Program
    {
        #region Private methods

        static void Main(string[] args)
        {
            const float reliabilityLevel = 83f;
            var testData = DataProvider.GetUserStories();

            var result = Predictor.Predict(testData, reliabilityLevel);
            foreach (var r in result)
                ConsoleAdapter.Output($"{r.Duration}: {r.AccumulatedPercentile:F3} - {r.Frequency}\n");
        }

        #endregion
    }
}