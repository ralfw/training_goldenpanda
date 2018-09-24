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

        public string UserMessage
        {
            get { return _userMessage; }
            set { _userMessage = value; OnPropertyChanged();}
        }

        public ICommand Login { get; }

        public LoginViewModel()
        {
            UserMessage = "";
            Login = new RelayCommands(OnLogin);
        }

        private void OnLogin(object obj)
        {
            if (string.IsNullOrEmpty(EmailAddress))
                Controller.MainController.LoginUi.Display("E-Mail address required");
            else if(string.IsNullOrEmpty(Password))
                Controller.MainController.LoginUi.Display("Password required");
            else
            {
                UserMessage = string.Empty;
                Controller.MainController.LoginUi.InvokeLoginRequested(EmailAddress, Password);
            }
        }

        #region Fields

        private string _emailAddress;
        private string _password;
        private string _userMessage;

        #endregion

        public void Display(string errorMessage)
        {
            UserMessage = errorMessage;
        }
    }
}