﻿using System;
using System.Threading;
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

        public void Start(string url)
        {
            _serviceHost = new ServiceHost();

            _serviceHost.Start(new Uri(url));
        }

        public void Stop()
        {
            _serviceHost.Stop();
        }
    }
}