using System;

namespace sc.contracts
{
    public interface IAdminCredentialService
    {
        void CreateUser(string emailAddress, string rolename, Action onSuccess, Action<string> onError);
    }
}