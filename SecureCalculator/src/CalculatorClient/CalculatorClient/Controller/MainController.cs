using System;
using System.Windows;

namespace CalculatorClient.Controller
{
    public static class MainController
    {
        public static void ShowLogin()
        {
            var loginView = new LoginView
            {
                DataContext = new LoginViewModel()
            };

            App.Current.MainWindow = loginView;
            App.Current.MainWindow.Show();
        }

        public static void Login(string emailAddress, string password)
        {
            var message = $"User called Login with{Environment.NewLine}{Environment.NewLine}" +
                          $"email: {emailAddress}{Environment.NewLine}" +
                          $"password:{password}";
            MessageBox.Show(message);
        }
    }
}