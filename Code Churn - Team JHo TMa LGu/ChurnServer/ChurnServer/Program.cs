using System;
using System.Diagnostics;
using System.Threading;
using ChurnServer.Adapter;
using ChurnServer.Infrastructure;

namespace ChurnServer
{
    class Program
    {
        private static ChurnServerConfiguration Configuration { get; set; }

        static void Main(string[] args)
        {
            Configuration = ChurnServerConfigurationProvider.CreateConfiguration(args);

            AdapterProvider.TimeProvider = new TimeProvider();
            AdapterProvider.FileIo = new FileIo();
            AdapterProvider.Ui = new Ui();
            
            using (AdapterProvider.TimeProvider.StartTimer(Configuration.SamplingRateInSeconds, GenerateReport))
            {
                Console.Out.WriteLine("");
                Console.Out.WriteLine("Churnserver.exe is observing '{0}' every {1} sec.", Configuration.ObservableDirectoryPath, Configuration.SamplingRateInSeconds);
                Console.Out.WriteLine("Writing to '{0}'", Configuration.ProtocolFilePath);
                Console.Out.WriteLine("");
                Console.Out.WriteLine("Any key to stop the server and exit");
                Console.Out.WriteLine("");
                Console.ReadKey();
            }
        }

        private static void GenerateReport(object o)
        {
            ProtocolBuilder.GenerateReport(
                Configuration.ObservableDirectoryPath,
                Configuration.ProtocolFilePath,
                Configuration.FileExtensions);
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
