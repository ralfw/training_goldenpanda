using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChurnReport
{
    public static class ConfigProvider
    {
        public static Config GetConfig(string[] args)
        {
            if (args.Length != 3)
                throw new ArgumentException();
            var startDate = DateTime.Parse(args[1], CultureInfo.InvariantCulture);
            var endDate = DateTime.Parse(args[2], CultureInfo.InvariantCulture);
            if (endDate < startDate)
                throw new ArgumentException("StartDate should be less or equal EndDate");
            return new Config
            {
                ProtocolFilePath = args[0],
                StartDate = startDate,
                EndDate = endDate
            };
        }
    }
}
