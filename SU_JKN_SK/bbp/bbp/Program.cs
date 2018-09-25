using System;
using System.Collections.Generic;

namespace bbp
{
    class Program
    {
        #region Private methods

        static void Main(string[] args)
        {
            var testData = GetTestData();

            var sortedDurations = Predictor.Predict(testData);
            ConsoleAdapter.Output(sortedDurations);
        }

        private static List<Tuple<string, string>> GetTestData()
        {
            var testdata = new List<Tuple<string, string>>
            {
                new Tuple<string, string>("2018-01-01", "2018-01-02"),
                new Tuple<string, string>("2018-01-01", "2018-01-03"),
                new Tuple<string, string>("2018-01-01", "2018-01-02"),
                new Tuple<string, string>("2018-01-01", "2018-01-07"),
                new Tuple<string, string>("2018-01-01", "2018-01-03"),
                new Tuple<string, string>("2018-01-01", "2018-01-05"),
                new Tuple<string, string>("2018-01-01", "2018-01-03"),
                new Tuple<string, string>("2017-12-30", "2018-01-02")
            };
            return testdata;
        }

        #endregion
    }
}