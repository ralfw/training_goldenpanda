namespace bbp
{
    class Program
    {
        #region Private methods

        static void Main(string[] args)
        {
            const float reliabilityLevel = .83f;
            var testData = DataProvider.GetUserStories();

            var result = Predictor.Predict(testData, reliabilityLevel);
            ConsoleAdapter.Output(result, reliabilityLevel);
        }

        #endregion
    }
}