using System.Windows;
using AppointmentExporter.ViewModel;

namespace AppointmentExporter.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var vm = new AppointmentExporterVM();
            var  fakeRequestHandler = new FakeRequestHandler();

            vm.onExport += fakeRequestHandler.Export;
            fakeRequestHandler.onExported += vm.DisplayStatus;

            DataContext = vm;
        }
    }
}
