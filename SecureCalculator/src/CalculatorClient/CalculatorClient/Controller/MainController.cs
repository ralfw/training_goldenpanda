using System;
using System.Windows;
using CalculatorClient.Interfaces;
using sc.contracts;

namespace CalculatorClient.Controller
{
    public static class MainController
    {
        public static ILoginUi  LoginUi { get; private set; }
        public static ICalculatorUi CalculatorUi { get; private set; }

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

            // TODO: user service to get permissions
            var fakePermissions = GetFakePermissions();

            // call Calculator view
            if (CalculatorUi == null)
                CalculatorUi = new CalculatorUi();

            CalculatorUi.Open(fakePermissions);
        }

        private static PermissionSet GetFakePermissions()
        {
            Permissions[] permissions = {
                Permissions.Add,
                Permissions.Subtract,
                Permissions.Multiply,
                Permissions.Divide
            };

            var fakePermissions = new PermissionSet(permissions);
            return fakePermissions;
        }
    }
}