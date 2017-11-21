using System;

namespace Client
{
    public static class StatisticAdapter
    {
        public static void DisplayStatistics(StatisticInfo statisticInfo, string filePath)
        {
            Console.WriteLine($"{statisticInfo.FileCount} files");
            Console.WriteLine($"max. churns: {statisticInfo.MaxChurns}");
            Console.WriteLine($"max. loc: {statisticInfo.MaxLoc}");
            Console.WriteLine($"reoprt file: {filePath}");
        }
    }
}