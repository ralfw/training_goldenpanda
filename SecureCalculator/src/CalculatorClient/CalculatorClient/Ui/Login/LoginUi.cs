using System;
using CalculatorClient.Interfaces;

namespace CalculatorClient
{
    public class LoginUi : ILoginUi
    {
        public event Action<string, string> OnLoginRequested;
        public void InvokeLoginRequested(string emailAddress, string password)
        {
            OnLoginRequested?.Invoke(emailAddress, password);
        }

        private LoginView _loginView;
        private LoginViewModel _loginViewModel;

        public void Open()
        {
            if (_loginView == null)
            {
                _loginViewModel = new LoginViewModel();
                _loginView = new LoginView
                {
                    DataContext = _loginViewModel
                };
            }

            App.Current.MainWindow = _loginView;
            App.Current.MainWindow.Show();
        }

        public void Display(string errorMessage)
        {
            _loginViewModel.Display(errorMessage);
        }
    }
}