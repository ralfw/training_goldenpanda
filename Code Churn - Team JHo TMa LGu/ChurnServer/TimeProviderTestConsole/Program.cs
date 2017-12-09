using System;
using ChurnServer.Adapter;

namespace TimeProviderTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintTestHeader();

            Test_GetCurrentDateAndTime();
        }

        private static void PrintTestHeader()
        {
            Console.Out.WriteLine("TimeProviderTestConsole");
            Console.Out.WriteLine("");
        }

        private static void Test_GetCurrentDateAndTime()
        {
            var timeProvider = new TimeProvider();

            Console.Out.Write("Any key to get the current time from the provider...");
            Console.ReadKey();
            Console.Out.WriteLine("");

            var startTime = timeProvider.GetCurrentDateAndTime();

            Console.Out.WriteLine("Current time is {0}", startTime);
            Console.Out.WriteLine("");
        }
    }
}
