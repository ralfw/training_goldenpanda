using System;

namespace sc.contracts
{
    public interface IUserService
    {
        void AddUser(string emailAddress, string rolename, Action onSuccess, Action<string> onError);
        void LogIn(string emailAddress, string passwordHash, Action<PermissionSet> onSuccess, Action<string> onError);
    }
}