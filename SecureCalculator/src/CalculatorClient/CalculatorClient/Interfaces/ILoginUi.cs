using System;

namespace CalculatorClient.Interfaces
{
    public interface ILoginUi
    {
        void Open();
        void Display(string errorMessage);
        Action<string, string> OnLoginRequested();
    }
}