using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common;

namespace Server
{
    public static class FileSystemAdapter
    {
        public static List<string> GetFilePaths(string path)
        {
            return Directory.GetFiles(path, "*", SearchOption.AllDirectories).Where(s => s.EndsWith(".cs") || s.EndsWith(".txt")).ToList();
        }

        public static IEnumerable<ProtocolEntry> GetProtocolEntries(List<string> filePaths)
        {
            return filePaths.Select(filePath => new ProtocolEntry(DateTime.Now, File.ReadAllLines(filePath).Length, filePath));
        }

        public static void PersistProtocolEntries(IEnumerable<ProtocolEntry> protocolEntries, string protocolFilePath)
        {
            File.AppendAllLines(protocolFilePath, protocolEntries.Select(entry => entry.ToString()));
        }
    }
}