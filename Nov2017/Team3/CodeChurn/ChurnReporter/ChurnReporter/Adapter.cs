using System.IO;

namespace ChurnReporter
{
    public class Adapter
    {
        public static string[] ReadProtocol(string protocolFilePath)
        {
            return File.ReadAllLines(protocolFilePath);
        }

        public static void WriteReport(string[] reportEntries, string reportFilePath)
        {
            using (StreamWriter outputFile = new StreamWriter(reportFilePath))
            {
                for (int i = 0; i < reportEntries.Length; i++)
                {
                    outputFile.WriteLine(reportEntries[i]);
                }
            }
        }
    }
}