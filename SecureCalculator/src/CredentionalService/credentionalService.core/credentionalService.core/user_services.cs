using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sc.contracts;

namespace credentionalService.core
{
    public class user_services : IUserService
    {
        public void AddUser(String emailAddress, String rolename, Action onSuccess, Action<String> onError)
        {
            
        }

        public void LogIn(String emailAddress, String passwordHash, Action<PermissionSet> onSuccess, Action<String> onError)
        {
            
        }
    }
}
