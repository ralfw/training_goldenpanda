using System;

namespace Server
{
    public class ScanData
    {
        public int FileCount { get; }
        public DateTime Start { get; }
        public DateTime End { get; }

        public ScanData(int fileCount, DateTime start, DateTime end)
        {
            FileCount = fileCount;
            Start = start;
            End = end;
        }
    }
}