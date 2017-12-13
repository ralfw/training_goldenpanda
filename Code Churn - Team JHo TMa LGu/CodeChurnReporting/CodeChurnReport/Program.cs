using System.Collections.Generic;
using System.Linq;
using CodeChurnReport.Behavior.Core;
using CodeChurnReport.Behavior.Portals;
using CodeChurnReport.Behavior.Providers;
using CodeChurnReport.Data;

namespace CodeChurnReport
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var config = ConfigProvider.GetConfig(args);
            var report = Generate_Report_File(config);
            Show_Statistic(report);
        }

        private static IEnumerable<ReportItem> Generate_Report_File(Config config)
        {
            var protocol = ProtocolReader.ReadProtocol(config.ProtocolFilePath);
            var report = Generate_Report(config, protocol);
            ReportPersistence.StoreReport(config, report);
            return report;
        }

        private static ReportItem[] Generate_Report(Config config, IEnumerable<ProtocolItem> protocol)
        {
            protocol = protocol.FilterByTimeSpan(config.StartDate, config.EndDate);
            return ReportBuilder.BuildReportItems(protocol.ToArray()).ToArray();
        }

        private static void Show_Statistic(IEnumerable<ReportItem> report)
        {
            var statistic = StatisticAdapter.GenerateStatistic(report);
            StatisticAdapter.DisplayStatistic(statistic);
        }


    }
}