using System.Linq;
using CalculatorClient.Interfaces;
using CalculatorClient.Resources;
using log4net;
using sc.contracts;

namespace CalculatorClient.Controller
{
    public static class MainController
    {
        public static ILoginUi LoginUi { get; set; }
        public static ICalculatorUi CalculatorUi { get; set; }
        public static ICalculatorCredentialService CalculatorCredentialService { get; set; }

        public static void Run()
        {
            AssertLoginUi();
            LoginUi.OnLoginRequested += LoginUiRequestsLogin;
            LoginUi.Open();
        }

        public static void LoginUiRequestsLogin(string emailAddress, string password)
        {
            LogLoginRequest(emailAddress, password);

            var hashedPassword = HashPassword(password);

            AssertService();
            CalculatorCredentialService.LogIn(emailAddress, hashedPassword,
                OnLoginSuccess,
                OnLoginFailure);
        }

        public static string HashPassword(string password)
        {
            // we currently use very simple hashing, which is the length of the given password
            var hashPassword = password.Length.ToString();

            Log.Debug($"Hashed password '{password}' -> '{hashPassword}'");

            return hashPassword;
        }

        public static void OnLoginSuccess(PermissionSet permissionSet)
        {
            LogLoginSuccess(permissionSet);

            // TODO: check for any given permissions and redirect to login if there are none

            AssertCalculatorUi();
            CalculatorUi.Open(permissionSet);
        }

        public static void OnLoginFailure(string errorMessage)
        {
            LogLoginFailure(errorMessage);

            LoginUi.Display(errorMessage);
        }

        #region Private methods

        private static void LogLoginRequest(string emailAddress, string password)
        {
            Log.Debug($"Login request - Email:{emailAddress}, Password:{password}");
        }

        private static void LogLoginSuccess(PermissionSet permissionSet)
        {
            Log.Debug($"Login succeeded! Permissions: A({permissionSet.Permissions.Contains(Permissions.Add)})," +
                      $"S({permissionSet.Permissions.Contains(Permissions.Subtract)})," +
                      $"M({permissionSet.Permissions.Contains(Permissions.Multiply)})," +
                      $"D({permissionSet.Permissions.Contains(Permissions.Divide)})");
        }

        private static void LogLoginFailure(string errorMessage)
        {
            Log.Debug($"Login failed! Reason '{errorMessage}'");
        }

        private static void AssertCalculatorUi()
        {
            if (CalculatorUi == null)
                CalculatorUi = new CalculatorUi();
        }

        private static void AssertLoginUi()
        {
            if (LoginUi == null)
                LoginUi = new LoginUi();
        }

        private static void AssertService()
        {
            if (CalculatorCredentialService == null)
                CalculatorCredentialService = new CalculatorCredentialService();
        }

        #endregion

        #region Fields

        private static readonly ILog Log = LogManager.GetLogger(typeof(MainController));

        #endregion
    }
}