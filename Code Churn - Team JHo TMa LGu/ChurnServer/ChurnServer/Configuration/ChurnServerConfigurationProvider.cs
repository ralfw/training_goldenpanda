namespace ChurnServer
{
    public static class ChurnServerConfigurationProvider
    {
        public static ChurnServerConfiguration CreateConfiguration(string[] args)
        {
            var samplingRateInSeconds = int.Parse(args[1]);
            return new ChurnServerConfiguration(args[0], samplingRateInSeconds, args[2]);
        }
    }

    public class ChurnServerConfiguration
    {
        public ChurnServerConfiguration(string observableDirectoryPath, int samplingRateInSeconds, string protocolFilePath)
        {
            ObservableDirectoryPath = observableDirectoryPath;
            SamplingRateInSeconds = samplingRateInSeconds;
            ProtocolFilePath = protocolFilePath;

            FileExtensions = new[] {"txt", "cs"};
        }

        public string ObservableDirectoryPath { get; }
        public int SamplingRateInSeconds { get; }
        public string ProtocolFilePath { get; }
        public string[] FileExtensions { get; set; }
    }
}