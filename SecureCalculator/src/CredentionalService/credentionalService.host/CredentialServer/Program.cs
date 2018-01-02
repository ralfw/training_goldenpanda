using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CredentialService;
using CredentialServiceTest;

namespace CredentialServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO use real Service 
            var server = new Server(new UserServiceMock());
            server.Run("http://localhost:8080");
        }
    }
}
