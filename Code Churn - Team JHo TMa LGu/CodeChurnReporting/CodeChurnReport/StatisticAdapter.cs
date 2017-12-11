using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CodeChurnReport.Structs;

namespace CodeChurnReport
{
    public static class StatisticAdapter
    {
        public static Statistic GenerateStatistic(IEnumerable<ReportItem> reportItems)
        {
            var items = reportItems.ToArray();
            if (items.Length == 0)
                return new Statistic {FileCount = 0, MaxChurnRate = 0, MaxLinesOfCode = 0};
            return new Statistic
            {
                FileCount = items.Length,
                MaxChurnRate = items.Max(i => i.ChurnRate),
                MaxLinesOfCode = items.Max(i => i.LinesOfCode)
            };
        }

        public static void DisplayStatistic(Statistic statistic)
        {
            Console.WriteLine($"{statistic.FileCount} files");
            Console.WriteLine($"max churn: {statistic.MaxChurnRate}");
            Console.WriteLine($"max lines of code: {statistic.MaxLinesOfCode}");
        }
    }
}