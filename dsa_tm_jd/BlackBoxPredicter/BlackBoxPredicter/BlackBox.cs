using System;
using System.Collections.Generic;
using System.Linq;
using BlackBoxPredicter.Dto;

namespace BlackBoxPredicter
{
    /*
     * Habe die Funktionen hier in die Reihenfolge ihrer Aufrufe in Main() gebracht.
     * Dadurch ist der Code besser lesbar. Wenn ich in Main() eine Funktion an einer Position sehe,
     * dann hab ich eine Vorstellung, wo sie hier in der Datei steht.
     * Und hier in der Datei stehen sie dann auch in der Reihenfolge der Nutzung in Integrationen.
     *
     * Außerdem habe ich zwei Funktionen zur Herstellung des Histograms zusammengefasst. So ist es
     * kompakter und immer noch übersichtlich.
     */
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
            IList<Tuple<int, double>> result = new List<Tuple<int, double>>();

            var cycleTimesArray = cycleTimes.ToArray();

            for (var i = 0; i < cycleTimesArray.Length; i++)
            {
                result.Add(new Tuple<int, double>(cycleTimesArray[i], (i + 1.0) / cycleTimesArray.Length));
            }

            return result;
        }
        
        
        internal static Histogram CalculateHistogram(IEnumerable<Tuple<int, double>> cycleTimesPercentiles, float markerValue)
        {
            var cycleTimeFrequency = CalculateFrequencies(cycleTimesPercentiles.Select(_ => _.Item1));
            var highestPercentileForCycle = FindHighestPercentiles(cycleTimesPercentiles);

            var entries = CalculateHistogramEntries(cycleTimeFrequency, highestPercentileForCycle);
            
            // So ein Aufbau einer Datenstruktur muss nicht in einer eigenen Funktion geschehen.
            return new Histogram {
                Entries = entries, 
                MarkerValue = markerValue,
                MarkerIndex = DetectMarkerIndex(entries, markerValue)
            };
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
        
        
        // Dieser Name für die Funktion passt besser.
        // Vorher gab es Verwirrung: wo war der Unterschied zwischen GenerateHistogram() und CreateHistorgram()?
        private static IList<HistogramEntry> CalculateHistogramEntries(IEnumerable<Tuple<int, int>> cycleTimeFrequency,
                                                                       IEnumerable<Tuple<int, double>> highestPercentileForCycle)
        {
            var result = new List<HistogramEntry>();
            foreach (var cf in cycleTimeFrequency) {
                var percentile = highestPercentileForCycle.First(c => c.Item1 == cf.Item1)
                                                          .Item2;
                result.Add(new HistogramEntry(cf.Item1, cf.Item2, percentile));
            }
            return result;
        }


        internal static int DetectMarkerIndex(IList<HistogramEntry> entries, float markerValue)
        {
            var result = 0;
            for (var i = 0; i < entries.Count; i++) {
                if (entries[i].Percentile * 100 <= markerValue)
                    result = i;
            }
            return result;
        }
    }
}