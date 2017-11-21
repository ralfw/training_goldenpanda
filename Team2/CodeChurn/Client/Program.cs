using System;
using appcfg;

namespace Client
{
    class Program
    {
        #region Private methods

        static void Main(string[] args)
        {
            var cfg = GetConfig(args);

            DateTime start = DateTime.Parse(cfg.start);
            DateTime end = DateTime.Parse(cfg.end);
            string reportPath = cfg.report;
            string protocolFilePath = cfg.protocol;


            var protocolEntries = ProtocolAdapter.CollectProtocolEntries(protocolFilePath);
            var filteredProtocolEntries = ProtocolEntryFilter.Filter(protocolEntries, start, end);
            var reportEntries = ReportGenerator.GenerateReportEntries(filteredProtocolEntries);
            var csvFilePath = ReportAdapter.PersistReport(reportEntries, start, end, reportPath);
            var statisticInfo = Statistics.Calculate(reportEntries);
            StatisticAdapter.DisplayStatistics(statisticInfo, csvFilePath);
        }

        private static dynamic GetConfig(string[] args)
        {
            var cfgSchema = new AppCfgSchema(null,
                new Route("params", "", isDefault: true)
                    .Param("start", "startDate", valueType: ValueTypes.String, isRequired: true)
                    .Param("end", "endDate", valueType: ValueTypes.String, isRequired: true)
                    .Param("report", "reportFile", valueType: ValueTypes.String, isRequired: true)
                    .Param("protocol", "protocolFile", valueType: ValueTypes.String, isRequired: true)
            );

            var cfgcomp = new AppCfgCompiler(cfgSchema);

            var cfg = cfgcomp.Compile(args);
            return cfg;
        }

        #endregion
    }
}