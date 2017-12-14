using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using CredentialService;

namespace CredentialServiceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            new Server("localhost:1234", new UserServiceMock());
        }
    }
}
