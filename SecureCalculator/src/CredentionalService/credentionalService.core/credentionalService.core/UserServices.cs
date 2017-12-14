using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sc.contracts;

namespace credentionalService.core
{
    public class UserServices : IUserService
    {
        public void AddUser(String emailAddress, String rolename, Action onSuccess, Action<String> onError)
        {
            throw new NotImplementedException();
        }

        public void LogIn(String emailAddress, String passwordHash, Action<PermissionSet> onSuccess, Action<String> onError)
        {
            throw new NotImplementedException();
        }
    }
}
