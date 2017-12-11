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
            var report = ReportBuilder.BuildReportItems(protocol).ToArray();
            ReportPersistence.StoreReport(config,report);
            var statistic = StatisticAdapter.GenerateStatistic(report);
            StatisticAdapter.DisplayStatistic(statistic);
        }
    }
}
