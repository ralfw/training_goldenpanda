using System;
using System.Collections.Generic;
using BlackBoxPredicter.Dto;

namespace BlackBoxPredicter
{
    internal class DisplayAdapter
    {
        public static void Display(Histogram histogram)
        {
            PrintHeader();
            PrintMarkerValue(histogram);
            PrintHistogramData(histogram);
        }

        private static void PrintHeader()
        {
            Console.Out.WriteLine("Histogramm");
            Console.Out.WriteLine("----------");
        }

        private static void PrintMarkerValue(Histogram histogram)
        {
            Console.Out.WriteLine("");
            Console.Out.WriteLine($"Verlässlichkeitsniveau = {histogram.MarkerValue:##.##} %");
            Console.Out.WriteLine("");
        }

        private static void PrintHistogramData(Histogram histogram)
        {
            for (var i = 0; i < histogram.Entries.Count; i++)
            {
                Console.Out.WriteLine(FormatHistoryLine(histogram.Entries[i]));
                if (i != histogram.MarkerIndex)
                    continue;

                Console.Out.WriteLine("______________________");
                Console.Out.WriteLine("");
            }

            Console.Out.WriteLine("");
        }

        private static string FormatHistoryLine(HistogramEntry histogramEntry)
        {
            return $"{histogramEntry.CycleTime};{histogramEntry.Frequency}x, {histogramEntry.Percentile}";
        }
    }
}