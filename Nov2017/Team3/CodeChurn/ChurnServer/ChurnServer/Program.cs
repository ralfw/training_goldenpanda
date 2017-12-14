using System;
using System.Timers;

namespace ChurnServer
{
    internal class Program
    {
        #region Private methods

        private static void Main(string[] args)
        {
            var config = Konfigurationsparameter_lesen(args);
            PeriodischAnstoßen(config.Interval_In_Milli_Second_s,
                () => ChurnServer.Quellcodestand_protokollieren(config.RootDir, config.ProtocolFilePath));

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }

        private static Config Konfigurationsparameter_lesen(string[] args)
        {
            Config result;
            result.Interval_In_Milli_Second_s = 1000*Int16.Parse(args[1]);
            result.ProtocolFilePath = args[2];
            result.RootDir = args[0];

            return result;
        }

        private static void PeriodischAnstoßen(int interval, Action On_Tick)
        {
            var timer = new Timer();
            timer.Interval = interval;
            timer.Elapsed += (sender, args) => On_Tick();
            timer.Start();
        }

        #endregion

        #region Nested types

        private struct Config
        {
            public string ProtocolFilePath;
            public string RootDir;
            public int Interval_In_Milli_Second_s;
        }

        #endregion
    }
}