using System;
using System.Collections.Generic;
using BlackBoxPredicter.Dto;

namespace BlackBoxPredicter
{
    public class DisplayAdapter
    {
        public static void Display(IEnumerable<HistogramEntry> historyEntries)
        {
            Console.Out.WriteLine("Histogram");
            Console.Out.WriteLine("-----------");
            foreach (var histogramEntry in historyEntries)
                Console.Out.WriteLine(FormatHistoryLine(histogramEntry));

            Console.Out.WriteLine("");
        }

        private static string FormatHistoryLine(HistogramEntry histogramEntry)
        {
            return $"{histogramEntry.CycleTime};{histogramEntry.Frequence}x, {histogramEntry.Percentil}";
        }
    }
}