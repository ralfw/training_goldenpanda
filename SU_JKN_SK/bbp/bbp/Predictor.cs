using System;
using System.Collections.Generic;
using System.Linq;
using bbp.dto;

namespace bbp
{
    internal static class Predictor
    {
        public static IEnumerable<int> CalculateSortedDurations(IEnumerable<UserStory> data)
        {
            var unsortedDurations = CalcDurations(data);
            var sortedDurations = SortDurations(unsortedDurations);
            return sortedDurations;
        }

        #region Private methods

        private static int CalcDuration(DateTime startDate, DateTime endDate) => (endDate - startDate).Days + 1;


        private static IEnumerable<int> CalcDurations(IEnumerable<UserStory> data)
            => data.Select(d => CalcDuration(d.StartDate, d.EndDate));


        private static IEnumerable<int> SortDurations(IEnumerable<int> unsortedDurations) => unsortedDurations.OrderBy(i => i);

        #endregion
    }
}