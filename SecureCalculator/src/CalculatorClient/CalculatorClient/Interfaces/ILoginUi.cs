using System;

namespace CalculatorClient.Interfaces
{
    public interface ILoginUi
    {
        void Open();
        void Display(string errorMessage);
        event Action<string, string> OnLoginRequested;
        void InvokeLoginRequested(string emailAddress, string password);
    }
}