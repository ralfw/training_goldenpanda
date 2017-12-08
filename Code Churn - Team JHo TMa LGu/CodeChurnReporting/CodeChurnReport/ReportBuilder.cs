using System.Collections.Generic;
using System.Linq;
using CodeChurnReport.Structs;

namespace CodeChurnReport
{
    public static class ReportBuilder
    {
        public static IEnumerable<ReportItem> BuildReportItems(IEnumerable<ProtocolItem> protocollItems)
        {
            var result = new List<ReportItem>();
            var items = protocollItems.ToArray();
            var dictinctFilePaths = items.GetDistinctFilePaths().ToArray();
            foreach (var file in dictinctFilePaths)
            {
                var fileItems = items.FilterByUncFilePath(file).ToArray();
                var churnRate = fileItems.GetChurnRate();
                var lastLinesOfCode = fileItems.Last().LineOfCode;
                result.Add(new ReportItem {UncFilePath = file, ChurnRate = churnRate, LinesOfCode = lastLinesOfCode});
            }
            return result;
        }


    }
}