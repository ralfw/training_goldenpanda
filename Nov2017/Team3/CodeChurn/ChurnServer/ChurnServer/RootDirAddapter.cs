using System;
using System.IO;

namespace ChurnServer
{
    public class RootDirAddapter
    {
        internal static string[] GetFilePaths(string rootDir)
        {
            string[] allfiles = Directory.GetFiles(rootDir, "*.cs", SearchOption.AllDirectories);
            return allfiles;
        }

        internal static ProtocolEntry AnalyzeFile(String filePath)
        {
            return new ProtocolEntry
            {
                LoC = File.ReadAllLines(filePath).Length,
                TimeStamp = new FileInfo(filePath).LastWriteTime,
                FilePath = filePath
            };
        }
    }
}