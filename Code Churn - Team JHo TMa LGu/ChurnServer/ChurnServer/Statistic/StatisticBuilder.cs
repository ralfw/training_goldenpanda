using System;
using ChurnServer.Infrastructure;

namespace ChurnServer.Statistic
{
    public class StatisticBuilder
    {
        public static ReportStatistic BuildStatistic(DateTime startTime, string[] filesPaths)
        {
            var statisticDuration = AdapterProvider.TimeProvider.GetCurrentDateAndTime().Subtract(startTime);
            return new ReportStatistic(startTime, filesPaths.Length, statisticDuration);
        }
    }
}