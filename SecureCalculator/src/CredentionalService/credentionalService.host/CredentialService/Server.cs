using System;
using sc.contracts;
using servicehost;

namespace CredentialService
{
    public class Server
    {
        public static IUserService UserService;

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