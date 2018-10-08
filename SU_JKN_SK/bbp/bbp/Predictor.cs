using System.Collections.Generic;
using System.Linq;
using bbp.dto;

namespace bbp
{
    internal static class Predictor
    {
        public static IPredictorResultEnumerable Predict(IEnumerable<UserStory> data, float reliabilityLevel)
        {
            var sortedGroupedUserStories = GroupSortedUserStories(data).ToArray();
            var predictorResult = CalculateHistogramData(sortedGroupedUserStories);
            return SetReliabilityLevelIndex(predictorResult, reliabilityLevel);
        }

        #region Private methods

        private static IEnumerable<IGrouping<int, UserStory>> GroupSortedUserStories(IEnumerable<UserStory> data)
            => data.OrderBy(u => u.Duration)
                   .GroupBy(u => u.Duration);

        private static PredictorResultEnumerable CalculateHistogramData(IGrouping<int, UserStory>[] sortedGroupedUserStories)
        {
            var percentageIncrement = 1f / sortedGroupedUserStories.Sum(u => u.Count());
            var position = 0;

            var resultEnumerable = new PredictorResultEnumerable();
            resultEnumerable.AddRange(sortedGroupedUserStories.Select(u =>
            {
                position += u.Count();
                return new PredictorResult(u.Key, position * percentageIncrement, u.Count());
            }));

            return resultEnumerable;
        }

        private static IPredictorResultEnumerable SetReliabilityLevelIndex(PredictorResultEnumerable enumerable, float reliabilityLevel)
        {
            for (var i = 0; enumerable.Count > i && enumerable[i].AccumulatedPercentile < reliabilityLevel; i++)
                enumerable.SetReliabilityLevelIndex(i);

            return enumerable;
        }

        #endregion
    }
}