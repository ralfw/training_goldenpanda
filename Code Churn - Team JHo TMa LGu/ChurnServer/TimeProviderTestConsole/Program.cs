using System;
using ChurnServer;

namespace TimeProviderTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintTestHeader();

            Test_GetStartTime();
        }

        private static void PrintTestHeader()
        {
            Console.Out.WriteLine("TimeProviderTestConsole");
            Console.Out.WriteLine("");
        }

        private static void Test_GetStartTime()
        {

            Console.Out.Write("Any key to get the starttime from the provider...");
            Console.ReadKey();
            Console.Out.WriteLine("");

            var startTime = TimeProvider.GetStartTime();

            Console.Out.WriteLine("StartTime is {0}", startTime);
            Console.Out.WriteLine("");
        }
    }
}
