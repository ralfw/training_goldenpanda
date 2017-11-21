using System.Collections.Generic;

namespace Client
{
    public static class Statistics
    {
        public static StatisticInfo Calculate(List<ReportEntry> entries)
        {
            var statisticInfo = new StatisticInfo();
            foreach (var entry in entries)
            {
                if (entry.LastLoc > statisticInfo.MaxLoc)
                    statisticInfo.MaxLoc = entry.LastLoc;
                if (entry.Churns > statisticInfo.MaxChurns)
                    statisticInfo.MaxChurns = entry.Churns;
            }
            statisticInfo.FileCount = entries.Count;
            return statisticInfo;
        }
    }
}