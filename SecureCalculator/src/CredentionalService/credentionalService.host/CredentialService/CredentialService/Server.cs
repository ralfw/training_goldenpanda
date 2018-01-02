using System;
using sc.contracts;
using servicehost;

namespace CredentialService
{
    public class Server
    {
        public static IUserService UserService;
        private ServiceHost _serviceHost;

        public Server(IUserService userService)
        {
            UserService = userService;
        }

        public void Run(string url)
        {
            servicehost.ServiceHost.Run(new Uri(url));
        }
    }
}