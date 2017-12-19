using System;
using CalculatorClient.Interfaces;

namespace CalculatorClient.UnitTests
{
    public class TestLoginUi : ILoginUi, ITestLoginUi
    {
        private bool _isOpen;

        public void Open()
        {
            _isOpen = true;
        }

        public void Display(string errorMessage)
        {
            throw new NotImplementedException();
        }

        public event Action<string, string> OnLoginRequested;
        public void InvokeLoginRequested(string emailAddress, string password)
        {
            OnLoginRequested?.Invoke(emailAddress, password);
        }

        public bool IsOpen()
        {
            return _isOpen;
        }
    }
}