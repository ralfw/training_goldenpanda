using System;
using System.Timers;

namespace ChurnServer
{
    internal class Program
    {
        #region Private methods

        private static void Main(string[] args)
        {
            _rootDir = args[0];
            _reportFilePath = args[2];

            var timer = new Timer();
            timer.Interval = Int16.Parse(args[1]);
            timer.Elapsed += TimerOnElapsed;
            timer.Start();

            Console.ReadLine();


        }

        private static void TimerOnElapsed(Object sender, ElapsedEventArgs elapsedEventArgs)
        {
            ChurnServer.DoEvaluateDirectory(_rootDir, _reportFilePath);
        }

        #endregion

        #region Fields

        private static string _rootDir;
        private static String _reportFilePath;

        #endregion
    }
}