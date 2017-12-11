using System.Collections.Generic;
using System.IO;
using System.Linq;
using CodeChurnReport.Structs;

namespace CodeChurnReport
{
    public class ReportPersistence
    {
        public static void StoreReport(Config config, IEnumerable<ReportItem> content)
        {
            var filepath =
                $"{Path.GetDirectoryName(config.ProtocolFilePath)}\\churnreport_{config.StartDate:yyyyMMdd}_{config.EndDate:yyyyMMdd}.csv";
            var fileContent = content.Select(item => $"{item.LinesOfCode};{item.ChurnRate};{item.UncFilePath}");
            File.WriteAllLines(filepath, fileContent);
        }
    }
}