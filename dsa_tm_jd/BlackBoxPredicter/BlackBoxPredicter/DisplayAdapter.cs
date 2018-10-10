using System;
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
            Console.WriteLine("Histogram");
            Console.WriteLine("---------");
        }

        private static void PrintMarkerValue(Histogram histogram)
        {
            Console.WriteLine();
            Console.WriteLine($"Verlässlichkeitsniveau = {histogram.MarkerValue:##.##} %");
            Console.WriteLine();
        }

        private static void PrintHistogramData(Histogram histogram)
        {
            for (var i = 0; i < histogram.Entries.Count; i++) {
                var line = FormatHistoryLine(histogram.Entries[i]);// avoid nesting! //TMa, why?
                Console.WriteLine(line);
                if (i != histogram.MarkerIndex)
                    continue;

                Console.WriteLine("--------------");
            }
            Console.WriteLine("");
            
            
            string FormatHistoryLine(HistogramEntry histogramEntry)
                => $"{histogramEntry.CycleTime};{histogramEntry.Frequency}x, {histogramEntry.Percentile}";
        }

    }
}