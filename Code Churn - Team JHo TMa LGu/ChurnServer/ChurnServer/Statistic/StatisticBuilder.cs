using System;
using ChurnServer.AdapterInterfaces;

namespace ChurnServer.Statistic
{
    public class StatisticBuilder
    {
        public static ReportStatistic BuildStatistic(ITimeProvider timeProvider, DateTime startTime, string[] filesPaths)
        {
            var statisticDuration = timeProvider.GetCurrentDateAndTime().Subtract(startTime);
            return new ReportStatistic(startTime, filesPaths.Length, statisticDuration);
        }
    }
}