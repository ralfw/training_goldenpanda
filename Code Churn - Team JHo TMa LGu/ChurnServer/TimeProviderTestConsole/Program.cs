using System;
using System.Threading;
using ChurnServer.Adapter;
using ChurnServer.AdapterInterfaces;

namespace TimeProviderTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var timeProvider = new TimeProvider();

            PrintTestHeader();

            Test_GetCurrentDateAndTime(timeProvider);

            Test_RunTimer(timeProvider);
        }

        private static void Test_RunTimer(ITimeProvider timeProvider)
        {
            const int sampleRate = 2;

            Console.Out.Write("Any key to start the timer (samplingRate={0})", sampleRate);
            Console.ReadKey();
            Console.Out.WriteLine("");

            using (timeProvider.StartTimer(sampleRate, TimerTickAction))
            {
                Console.Out.WriteLine("Any key to stop time and exit...");
                Console.ReadKey();
                Console.Out.WriteLine("");
                Console.Out.WriteLine("");
            }
        }
        
        private static void TimerTickAction(object state)
        {
            Console.Out.WriteLine("Time is now: " + DateTime.Now);
        }

        private static void PrintTestHeader()
        {
            Console.Out.WriteLine("TimeProviderTestConsole");
            Console.Out.WriteLine("");
        }

        private static void Test_GetCurrentDateAndTime(ITimeProvider timeProvider)
        {
            Console.Out.Write("Any key to get the current time from the provider...");
            Console.ReadKey();
            Console.Out.WriteLine("");

            var startTime = timeProvider.GetCurrentDateAndTime();

            Console.Out.WriteLine("Current time is {0}", startTime);
            Console.Out.WriteLine("");
        }
    }
}
