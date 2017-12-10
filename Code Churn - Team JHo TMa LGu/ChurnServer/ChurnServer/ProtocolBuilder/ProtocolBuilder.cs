using ChurnServer.Infrastructure;

namespace ChurnServer
{
    public static class ProtocolBuilder
    {
        public static void GenerateReport(string observableDirectoryPath, string protocolFilepath, string[] fileExtensions)
        {
            var startTime = AdapterProvider.TimeProvider.GetCurrentDateAndTime();

            throw new System.NotImplementedException();
        }

        public static int GetLinesOfCode(string filePath)
        {
            // empty lines are counted as well
            return AdapterProvider.FileIo.GetFileContent(filePath).Length;
        }
    }
}
