using System;

namespace ChurnServer.Statistic
{
    public class ReportStatistic  
    {
        public DateTime StartTime { get; }
        public TimeSpan Duration { get;}
        public int NumberOfFiles { get;}

        public ReportStatistic(DateTime startTime, int numberOfFiles, TimeSpan duration)
        {
            StartTime = startTime;
            NumberOfFiles = numberOfFiles;
            Duration = duration;
        }
    }
}