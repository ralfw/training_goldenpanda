using CalculatorClient.Interfaces;
using log4net;
using sc.contracts;

namespace CalculatorClient.Controller
{
    public static class MainController
    {
        public static ILoginUi LoginUi { get; set; }
        public static ICalculatorUi CalculatorUi { get; set; }

        public static void Run()
        {
            AssertLoginUi();
            LoginUi.OnLoginRequested += LoginUiRequestsLogin;
            LoginUi.Open();
        }

        public static void LoginUiRequestsLogin(string emailAddress, string password)
        {
            LogRequest(emailAddress, password);

            // TODO: use service to get permissions
            var fakePermissions = GetFakePermissions();

            AssertCalculatorUi();
            CalculatorUi.Open(fakePermissions);
        }

        #region Private methods

        private static void LogRequest(string emailAddress, string password)
        {
            Log.Debug($"Login request - Email:{emailAddress}, Password:{password}");
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

        private static PermissionSet GetFakePermissions()
        {
            Permissions[] permissions =
            {
                Permissions.Add,
                Permissions.Subtract,
                Permissions.Multiply,
                Permissions.Divide
            };

            var fakePermissions = new PermissionSet(permissions);
            return fakePermissions;
        }

        #endregion

        #region Fields

        private static readonly ILog Log = LogManager.GetLogger(typeof(MainController));

        #endregion
    }
}