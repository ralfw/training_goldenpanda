using System;
using System.Globalization;
using ChurnServer.Infrastructure;

namespace ChurnServer
{
    public class ProtocolLine
    {
        public static string BuildProtocolLine(DateTime startTime, string filePath)
        {
            var timeStamp = $"{startTime:s}";
            var linesOfCode = ProtocolBuilder.GetLinesOfCode(filePath).ToString();
            var uncFilePath = AdapterProvider.FileIo.ToUncPath(filePath);

            return string.Join(";", timeStamp, linesOfCode, uncFilePath);
        }
    }
}