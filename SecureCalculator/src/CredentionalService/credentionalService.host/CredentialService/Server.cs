using System;
using System.Threading;
using sc.contracts;
using servicehost;

namespace CredentialService
{
    public class Server
    {
        public static IUserService UserService;

        public Server(string url, IUserService userService)
        {
            UserService = userService;
            using (var host = new ServiceHost())
            {
                host.Start(new Uri(url));
                
            }
        }
    }
}