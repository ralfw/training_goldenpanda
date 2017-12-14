using System;
using sc.administration.Data;
using sc.contracts;

namespace sc.administration.Providers
{
    public class ConfigProvider
    {
        public static Config GetConfig(string[] args)
        {
            return new Config()
            {
                Command = args[0],
                Email = args[1],
                Role = args[2],
                ServerUri = args[3]
            };
        }
    }
}
