using CalculatorClient.Interfaces;
using sc.contracts;

namespace CalculatorClient
{
    public class CalculatorUi : ICalculatorUi
    {
        private CalculatorView _view;
        private CalculatorViewModel _viewModel;

        public void Open(PermissionSet permissionSet)
        {
            if (_view == null)
            {
                _viewModel = new CalculatorViewModel(permissionSet);
                _view = new CalculatorView
                {
                    DataContext = _viewModel
                };
            }
            App.Current.MainWindow.Close();
            App.Current.MainWindow = _view;
            App.Current.MainWindow.Show();
           
        }
    }
}
