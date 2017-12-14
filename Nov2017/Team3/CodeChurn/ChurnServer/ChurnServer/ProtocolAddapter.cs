using System;
using System.Collections.Generic;
using System.IO;

namespace ChurnServer
{
    public class ProtocolAddapter
    {
        internal static void WriteProtocol(ProtocolEntry[] infos, string protocolFilePath)
        {
            List<string> lines = new List<String>();

            foreach (var info in infos)
                lines.Add($"{info.TimeStamp};{info.LoC};{info.FilePath}");

            File.AppendAllLines(protocolFilePath, lines);
        }
    }
}