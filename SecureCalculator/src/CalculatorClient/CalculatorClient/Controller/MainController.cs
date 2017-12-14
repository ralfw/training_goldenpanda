using System;
using System.Windows;
using CalculatorClient.Interfaces;

namespace CalculatorClient.Controller
{
    public static class MainController
    {
        public static ILoginUi  LoginUi { get; private set; }

        public static void ShowLogin()
        {
            if(LoginUi == null)
                LoginUi = new LoginUi();

            LoginUi.OnLoginRequested += Login;
            LoginUi.Open();
        }

        private static void Login(string emailAddress, string password)
        {
            var message = $"User called Login with{Environment.NewLine}{Environment.NewLine}" +
                          $"email: {emailAddress}{Environment.NewLine}" +
                          $"password:{password}";
            MessageBox.Show(message);
        }
    }
}