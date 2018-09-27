using System;
using System.Collections.Generic;
using System.Linq;
using BlackBoxPredicter.Dto;

namespace BlackBoxPredicter
{
    public class BlackBox
    {
        #region Private methods

        private static IEnumerable<HistogramEntry> CreateHistogram(IEnumerable<Tuple<int, int>> cycleTimeFrequency,
                                                                   IEnumerable<Tuple<int, double>> highestPercentileForCycle)
        {
            var result = new List<HistogramEntry>();
            foreach (var cf in cycleTimeFrequency)
            {
                var percentile = highestPercentileForCycle.First(c => c.Item1 == cf.Item1)
                                                        .Item2;
                result.Add(new HistogramEntry(cf.Item1, cf.Item2, percentile));
            }

            return result;
        }

        private static IEnumerable<Tuple<int, int>> CalculateFrequencies(IEnumerable<int> cycleTimes)
        {
            var result = new List<Tuple<int, int>>();

            var groupedCycles = cycleTimes.GroupBy(c => c);
            foreach (var groupedCycle in groupedCycles)
            {
                result.Add(new Tuple<int, int>(groupedCycle.Key, groupedCycle.Count()));
            }

            return result;
        }

        #endregion

        internal static IList<int> CalculateCycleTimes(IEnumerable<UserStory> userStories)
        {
            return userStories.Select(o => CalcCycleTime(o.Start, o.End))
                              .OrderBy(o => o)
                              .ToList();

            int CalcCycleTime(DateTime start, DateTime end) => (end - start).Days + 1;
        }

        internal static IEnumerable<Tuple<int, double>> FindHighestPercentiles(IEnumerable<Tuple<int, double>> percentiles)
        {
            IList<Tuple<int, double>> result = new List<Tuple<int, double>>();

            foreach (var percentile in percentiles)
            {
                if (result.All(o => o.Item1 != percentile.Item1))
                {
                    result.Add(new Tuple<int, double>(percentile.Item1, percentile.Item2));
                }

                var itm = result.First(o => o.Item1 == percentile.Item1);
                if (itm.Item2 < percentile.Item2)
                {
                    result.Remove(itm);
                    result.Add(new Tuple<int, double>(percentile.Item1, percentile.Item2));
                }
            }

            return result;
        }

        internal static IEnumerable<Tuple<int, double>> CalculatePercentiles(IEnumerable<int> cycleTimes)
        {
            IList<Tuple<int, double>> result = new List<Tuple<int, double>>();

            var cycleTimesArray = cycleTimes.ToArray();

            for (var i = 0; i < cycleTimesArray.Length; i++)
            {
                result.Add(new Tuple<int, double>(cycleTimesArray[i], (i + 1.0) / cycleTimesArray.Length));
            }

            return result;
        }

        internal static Histogram GenerateHistogram(IEnumerable<HistogramEntry> entries, float markerValue)
        {
            var result = new Histogram { Entries = entries.ToList(), MarkerValue = markerValue };
            result.MarkerIndex = DetectMarkerIndex(result.Entries, markerValue);

            return result;
        }

        internal static IEnumerable<HistogramEntry> GenerateHistogramEntries(IEnumerable<Tuple<int, double>> cycleTimesPercentiles)
        {
            var cycleTimeFrequency = CalculateFrequencies(cycleTimesPercentiles.Select(_ => _.Item1));
            var highestPercentileForCycle = FindHighestPercentiles(cycleTimesPercentiles);

            return CreateHistogram(cycleTimeFrequency, highestPercentileForCycle);
        }

        private static int DetectMarkerIndex(IList<HistogramEntry> entries, float markerValue)
        {
            var result = -1;

            for (var i = 0; i < entries.Count; i++)
            {
                if (entries[i]
                        .Percentile * 100 <= markerValue)
                {
                    result = i;
                }
            }

            if (result < 0) result = entries.Count - 1;

            return result;
        }
    }
}