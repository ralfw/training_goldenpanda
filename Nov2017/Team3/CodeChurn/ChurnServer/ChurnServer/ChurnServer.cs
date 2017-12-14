using System.Linq;

namespace ChurnServer
{
    public class ChurnServer
    {
        public static void Quellcodestand_protokollieren(string rootDir, string protocolFilePath)
        {
            var files = RootDirAddapter.GetFilePaths(rootDir);
            var protocolEntries = AnalyzeFiles(files);
            WriteOutput(protocolEntries, protocolFilePath);
        }

        internal static ProtocolEntry[] AnalyzeFiles(string[] filePaths)
        {
            return filePaths.Select(RootDirAddapter.AnalyzeFile).ToArray();
        }

        internal static void WriteOutput(ProtocolEntry[] infos, string protocolFilePath)
        {
            ProtocolAddapter.WriteProtocol(infos, protocolFilePath);
            new ConsoleAddapter().WriteToConsole(infos);
        }
    }
}