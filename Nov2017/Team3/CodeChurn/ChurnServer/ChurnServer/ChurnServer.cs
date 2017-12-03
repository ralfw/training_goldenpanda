using System;
using System.Collections.Generic;
using System.IO;

namespace ChurnServer
{
    public class ChurnServer
    {
        public static ChurnFileInfo[] AnalyzeFiles(string[] filePaths)
        {
            List<ChurnFileInfo> result = new List<ChurnFileInfo>();
            foreach (var filePath in filePaths)
            {
                var info = new FileInfo(filePath);

                var churnFileInfo = new ChurnFileInfo();
                churnFileInfo.LoC = File.ReadAllLines(filePath).Length;
                churnFileInfo.TimeStamp = info.LastWriteTime;
                churnFileInfo.FilePath = filePath;

                result.Add(churnFileInfo);
            }
            return result.ToArray();
        }

        public static string BuildConsoleOutput(ChurnFileInfo[] infos)
        {
            return $"Lines written: {infos.Length}";
        }

        public static void DoEvaluateDirectory(string rootDir, string protocolFilePath)
        {
            var files = GetFilePaths(rootDir);
            var fileInfos = AnalyzeFiles(files);
            WriteOutput(fileInfos, protocolFilePath);
        }

        public static string[] GetFilePaths(string rootDir)
        {
            string[] allfiles = Directory.GetFiles(rootDir, "*.cs", SearchOption.AllDirectories);
            return allfiles;
        }

        public static void WriteOutput(ChurnFileInfo[] infos, string protocolFilePath)
        {
            WriteProtocol(infos, protocolFilePath);
            WriteToConsole(infos);
        }

        public static void WriteProtocol(ChurnFileInfo[] infos, string protocolFilePath)
        {
            List<string> lines = new List<String>();

            foreach (var info in infos)
                lines.Add($"{info.TimeStamp};{info.LoC};{info.FilePath}");

            File.AppendAllLines(protocolFilePath, lines);
        }

        public static void WriteToConsole(ChurnFileInfo[] infos)
        {
            string output = BuildConsoleOutput(infos);
            Console.WriteLine(output);
        }
    }
}