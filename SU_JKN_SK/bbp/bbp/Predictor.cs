using System.Collections.Generic;
using System.Linq;
using bbp.dto;

namespace bbp
{
    internal static class Predictor
    {
        public static IEnumerable<PredictorResult> Predict(IEnumerable<UserStory> data)
        {
            var sortedGroupedUserStories = GroupSortedUserStories(data).ToArray();
            return CalculateHistogramData(sortedGroupedUserStories);
        }

        #region Private methods

        private static IEnumerable<IGrouping<int, UserStory>> GroupSortedUserStories(IEnumerable<UserStory> data)
            => data.OrderBy(u => u.Duration)
                   .GroupBy(u => u.Duration);

        private static IEnumerable<PredictorResult> CalculateHistogramData(IGrouping<int, UserStory>[] sortedGroupedUserStories)
        {
            var percentageIncrement = 1f / sortedGroupedUserStories.Sum(u => u.Count());
            var position = 0;

            return sortedGroupedUserStories.Select(u =>
            {
                position += u.Count();
                return new PredictorResult(u.Key, position * percentageIncrement, u.Count());
            });
        }

        #endregion
    }
}