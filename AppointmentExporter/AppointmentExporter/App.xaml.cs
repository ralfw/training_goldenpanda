using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppointmentExporter.View;
using AppointmentExporter.ViewModel;

namespace AppointmentExporter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var window = new AppointmentExporterWindow();

            var vm = new AppointmentExporterVM();
            var fakeRequestHandler = new FakeRequestHandler();

            vm.onExport += fakeRequestHandler.Export;
            fakeRequestHandler.onExported += vm.DisplayStatus;

            window.DataContext = vm;
            window.Show();
        }
    }
}
