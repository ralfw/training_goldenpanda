using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using log4net;

namespace CalculatorClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(App));


        protected override void OnStartup(StartupEventArgs e)
        {
            Log.InfoFormat("{0}{0}Starting up...{0}", Environment.NewLine);

            Controller.MainController.Run();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Log.Info($"Exiting with code {e.ApplicationExitCode}...");
            base.OnExit(e);
        }
    }
}
