using System;
using ChurnServer.Adapter;
using ChurnServer.Infrastructure;

namespace ChurnServer
{
    class Program
    {
        static void Main(string[] args)
        {
            AdapterProvider.TimeProvider = new TimeProvider();
            AdapterProvider.FileIo = new FileIo();
            AdapterProvider.Ui = new Ui();

            var configuration = ChurnServerConfigurationProvider.CreateConfiguration(args);

            DumpConfiguration(configuration);

            // todo add timing

            ProtocolBuilder.GenerateReport(configuration.ObservableDirectoryPath, configuration.ProtocolFilePath, configuration.FileExtensions);
        }

        private static void DumpConfiguration(ChurnServerConfiguration configuration)
        {
            Console.Out.WriteLine("");
            Console.Out.WriteLine("ChurnServerConfiguration");
            Console.Out.WriteLine("");
            Console.Out.WriteLine("------------------------");
            Console.Out.WriteLine($"ObservableDirectoryPath: {configuration.ObservableDirectoryPath}");
            Console.Out.WriteLine($"ProtocolFilePath:        {configuration.ProtocolFilePath}");
            Console.Out.WriteLine($"SamplingRateInSeconds:   {configuration.SamplingRateInSeconds}");
            Console.Out.WriteLine("FileExtensions");
            foreach (var fileExtension in configuration.FileExtensions)
            {
                Console.Out.WriteLine($" - {fileExtension}");
            }
            Console.Out.WriteLine("");
        }
    }
}
