using System;
using sc.contracts;

namespace CredentialServiceTest
{
    public class UserServiceMock : IUserService
    {
        public UserServiceMock()
        {
        }

        public void AddUser(string emailAddress, string rolename, Action onSuccess, Action<string> onError)
        {
            onError?.Invoke($"ServiceMock: AddUser-Command: emailAddress={emailAddress}; rolename={rolename}");
        }

        public void LogIn(string emailAddress, string passwordHash, Action<PermissionSet> onSuccess, Action<string> onError)
        {
            onError?.Invoke($"ServiceMock: LogIn-Command: emailAddress={emailAddress}; passwordHash={passwordHash}");
            onSuccess?.Invoke(new PermissionSet(new Permissions[] {Permissions.Add, Permissions.Subtract }));
        }
    }
}