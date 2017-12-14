using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sc.administration.Data;
using sc.contracts;

namespace sc.administration.Providers
{
    public class Controller
    {
        public static void Run(dynamic config, IAdminCredentialService credentialService)
        {
            var route = config._RoutePath;
            if (route == "cu")
            {
                string email = config.email;
                string role = config.role;
                credentialService.CreateUser(email, role, OnSuccess, OnError );
            }
            else
            {
                Console.WriteLine("Unknown command argument.");
            }
        }

        private static void OnError(string error)
        {
            Console.WriteLine($"User creation failed: {error}");
        }

        private static void OnSuccess()
        {
            Console.WriteLine("User successfully created");
        }
    }
}
