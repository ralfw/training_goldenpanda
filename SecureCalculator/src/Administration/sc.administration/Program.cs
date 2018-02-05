using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sc.administration.Providers;

namespace sc.administration
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ConfigProvider.GetConfig(args);
        
            var credentialService = new AdminCredentialService.AdminCredentialService();
            Controller.Run(config, credentialService);
        }
    }
}
