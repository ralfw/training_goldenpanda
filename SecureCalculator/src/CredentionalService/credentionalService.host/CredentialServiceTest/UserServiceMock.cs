using System;
using sc.contracts;

namespace CredentialServiceTest
{
    public class UserServiceMock : IUserService
    {
        #region IUserService members

        public void AddUser(string emailAddress, string rolename, Action onSuccess, Action<string> onError)
        {
            if (emailAddress.Contains("error"))
                onError?.Invoke($"ServiceMock: AddUser-Command: emailAddress={emailAddress}; rolename={rolename}");
            else
                onSuccess?.Invoke();
        }

        public void LogIn(string emailAddress, string passwordHash, Action<PermissionSet> onSuccess, Action<string> onError)
        {
            if (emailAddress.Contains("error"))
                onError?.Invoke($"ServiceMock: LogIn-Command: emailAddress={emailAddress}; passwordHash={passwordHash}");
            else
                onSuccess?.Invoke(new PermissionSet(new[] {Permissions.Add, Permissions.Subtract}));
        }

        #endregion
    }
}