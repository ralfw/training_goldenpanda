using System;
using System.Globalization;
using CodeChurnReport.Structs;

namespace CodeChurnReport
{
    public static class ConfigProvider
    {
        public static Config GetConfig(string[] args)
        {
            if (args.Length != 3)
                throw new ArgumentException();
            var startDate = DateTime.Parse(args[0], CultureInfo.InvariantCulture);
            var endDate = DateTime.Parse(args[1], CultureInfo.InvariantCulture);
            if (endDate < startDate)
                throw new ArgumentException("StartDate should be less or equal EndDate");
            return new Config
            {
                ProtocolFilePath = args[2],
                StartDate = startDate,
                EndDate = endDate
            };
        }
    }
}