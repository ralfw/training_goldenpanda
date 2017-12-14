using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using sc.contracts;
using servicehost.contract;

namespace CredentialService
{
    [Service]
    public class CredentialService
    {
        [EntryPoint(HttpMethods.Get, "/api/v1/users")] 
        public string AddUser(string emailAddress, string roleName)
        {
            var result = string.Empty;
            Server.UserService.AddUser(emailAddress,roleName,() => {},s => result=s);
            return result;
        }

        [EntryPoint(HttpMethods.Get, "/api/v1/users/role")] 
        public string Login(string emailAddress, string passwordHash)
        {
            var result = string.Empty;
            PermissionSet permissionSet = null;
            Server.UserService.LogIn(emailAddress, passwordHash, p => permissionSet = p, s => result = s);
            //TODO Change result type 
            return result;
        }
    }
}
