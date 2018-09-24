using System;
using sc.contracts;
using servicehost;

namespace CredentialService
{
    public class Server
    {
        public Server(IUserService userService)
        {
            UserService = userService;
        }

        public void Run(string url)
        {
            ServiceHost.Run(new Uri(url));
        }

        #region Fields

        public static IUserService UserService;

        #endregion
    }
}