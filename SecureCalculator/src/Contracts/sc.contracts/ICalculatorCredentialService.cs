using System;

namespace sc.contracts
{
    public interface ICalculatorCredentialService
    {
        void LogIn(string emailAddress, string passwordHash, Action<PermissionSet> onSuccess, Action<string> onError);
    }
}