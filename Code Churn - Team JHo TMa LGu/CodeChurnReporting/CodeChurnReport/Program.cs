using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChurnReport
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ConfigProvider.GetConfig(args);
            var protocol = ProtocolReader.ReadProtocol(config);
            var report = ReportBuilder.BuildReportItems(protocol);
            var statistic = StatisticAdapter.GenerateStatistic(report);
            Console.WriteLine($"{statistic.FileCount} files");
            Console.WriteLine($"max churn: {statistic.MaxChurnRate}");
            Console.WriteLine($"max lines of code: {statistic.MaxLinesOfCode}");
        }
    }
}
