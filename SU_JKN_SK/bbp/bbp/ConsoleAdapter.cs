using System;
using bbp.dto;

namespace bbp
{
    internal static class ConsoleAdapter
    {
        public static void Output(IPredictorResultEnumerable predictorResultEnumerable, float reliabilityLevel)
        {
            // ReSharper disable once StringLiteralTypo
            Console.WriteLine("Verlässlichkeitsniveau: {0:P}", reliabilityLevel);
            Console.WriteLine();

            var currentIndex = 0;
            foreach (var result in predictorResultEnumerable)
            {
                Console.WriteLine($"{result.Frequency}x {result.Duration}, {result.AccumulatedPercentile:F3}");
                if (predictorResultEnumerable.ReliabilityLevelIndex == currentIndex++)
                    Console.WriteLine(string.Empty.PadLeft(16, '─'));
            }

            Console.WriteLine();
        }

        [Obsolete]
        public static void Output(string text)
        {
            Console.Write(text);
        }
    }
}