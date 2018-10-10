using System;
using System.Collections.Generic;
using System.Linq;
using BlackBoxPredicter.Dto;

namespace BlackBoxPredicter
{
    public class BlackBox
    {
        internal static IList<int> CalculateCycleTimes(IEnumerable<UserStory> userStories)
        {
            return userStories.Select(o => CalcCycleTime(o.Start, o.End))
                .OrderBy(o => o)
                .ToList();

            int CalcCycleTime(DateTime start, DateTime end) => (end - start).Days + 1;
        }
        
        internal static IEnumerable<Tuple<int, double>> CalculatePercentiles(IEnumerable<int> cycleTimes)
        {
            var cycleTimesArray = cycleTimes.ToArray();
            return cycleTimesArray.Select((t, i) => new Tuple<int, double>(t, CalculatePercentile(i, cycleTimesArray.Length)));

            double CalculatePercentile(int currentIndex, int noOfCycleTimes) => (currentIndex + 1.0) / noOfCycleTimes;
        }

        internal static Histogram CalculateHistogram(IEnumerable<Tuple<int, double>> cycleTimesPercentiles, float markerValue)
        {
            var cycleTimeFrequencies = CalculateFrequencies(cycleTimesPercentiles.Select(tuple => tuple.Item1));
            var highestPercentilesPerCycle = FindHighestPercentilesPerCycleTime(cycleTimesPercentiles);
            var histogramEntries = CalculateHistogramEntries(cycleTimeFrequencies, highestPercentilesPerCycle);
            
            return new Histogram {
                Entries = histogramEntries, 
                MarkerValue = markerValue,
                MarkerIndex = DetermineMarkerIndex(histogramEntries, markerValue)
            };
        }
        
        private static IEnumerable<Tuple<int, int>> CalculateFrequencies(IEnumerable<int> cycleTimes)
        {
            return cycleTimes.GroupBy(c => c)
                             .Select(groupedCycle => new Tuple<int, int>(groupedCycle.Key, groupedCycle.Count()));
        }
        
        internal static IEnumerable<Tuple<int, double>> FindHighestPercentilesPerCycleTime(IEnumerable<Tuple<int, double>> percentiles)
        {
            var highestPercentiles = new List<Tuple<int, double>>();

            foreach (var percentile in percentiles)
            {
                if (highestPercentiles.All(o => o.Item1 != percentile.Item1))
                {
                    highestPercentiles.Add(new Tuple<int, double>(percentile.Item1, percentile.Item2));
                }

                var itm = highestPercentiles.First(o => o.Item1 == percentile.Item1);
                if (itm.Item2 < percentile.Item2)
                {
                    highestPercentiles.Remove(itm);
                    highestPercentiles.Add(new Tuple<int, double>(percentile.Item1, percentile.Item2));
                }
            }

            return highestPercentiles;
        }
        
        private static IList<HistogramEntry> CalculateHistogramEntries(IEnumerable<Tuple<int, int>> cycleTimeFrequencies,
                                                                       IEnumerable<Tuple<int, double>> highestPercentilesPerCycle)
        {
            var entries = new List<HistogramEntry>();

            foreach (var frequency in cycleTimeFrequencies)
            {
                var highestPercentile = highestPercentilesPerCycle.First(c => c.Item1 == frequency.Item1).Item2;
                entries.Add(new HistogramEntry(frequency.Item1, frequency.Item2, highestPercentile));
            }

            return entries;
        }

        internal static int DetermineMarkerIndex(IList<HistogramEntry> entries, float markerValue)
        {
            var markerIndex = 0;
            for (var i = 0; i < entries.Count; i++) {
                if (entries[i].Percentile * 100 <= markerValue)
                    markerIndex = i;
            }
            return markerIndex;
        }
    }
}