using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurnServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = ChurnServerConfigurationProvider.CreateConfiguration(args);

            DumpConfiguration(configuration);
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
