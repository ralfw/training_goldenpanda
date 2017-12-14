using System;
using sc.contracts;

namespace CredentialServiceTest
{
    internal class UserServiceMock : IUserService
    {
        public UserServiceMock()
        {
        }

        public void AddUser(string emailAddress, string rolename, Action onSuccess, Action<string> onError)
        {
            onError?.Invoke("Error");
        }

        public void LogIn(string emailAddress, string passwordHash, Action<PermissionSet> onSuccess, Action<string> onError)
        {
            onError?.Invoke("Error");
        }
    }
}