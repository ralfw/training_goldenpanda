using System;

namespace Server
{
    public static class LogMsgAdapter
    {
        public static void OutputLogMsg(ScanData scanData)
        {
            Console.WriteLine($"{scanData.Start:s};{Math.Ceiling((scanData.End - scanData.Start).TotalSeconds):#########};{scanData.FileCount} file");
        }
    }
}