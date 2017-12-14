using System.Windows.Input;
using CalculatorClient.Infrastructure;

namespace CalculatorClient
{
    public class LoginViewModel : ViewModelBase
    {
        public override string Title => "Login";

        public string EmailAddress
        {
            get { return _emailAddress; }
            set
            {
                _emailAddress = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public ICommand Login { get; }

        public LoginViewModel()
        {
            Login = new RelayCommands(OnLogin);
        }

        private void OnLogin(object obj)
        {
            Controller.MainController.LoginUi.InvokeLoginRequested(EmailAddress, Password);
        }

        #region Fields

        private string _emailAddress;
        private string _password;

        #endregion
    }
}