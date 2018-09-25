﻿using System.Collections.Generic;
using bbp.dto;

namespace bbp
{
    class Program
    {
        #region Private methods

        static void Main(string[] args)
        {
            var testData = GetTestData();

            var sortedDurations = Predictor.CalculateSortedDurations(testData);
            ConsoleAdapter.Output(sortedDurations);
        }

        private static List<UserStory> GetTestData()
        {
            var testdata = new List<UserStory>
            {
                new UserStory("2018-01-01", "2018-01-02"),
                new UserStory("2018-01-01", "2018-01-03"),
                new UserStory("2018-01-01", "2018-01-02"),
                new UserStory("2018-01-01", "2018-01-07"),
                new UserStory("2018-01-01", "2018-01-03"),
                new UserStory("2018-01-01", "2018-01-05"),
                new UserStory("2018-01-01", "2018-01-03"),
                new UserStory("2017-12-30", "2018-01-02")
            };
            return testdata;
        }

        #endregion
    }
}