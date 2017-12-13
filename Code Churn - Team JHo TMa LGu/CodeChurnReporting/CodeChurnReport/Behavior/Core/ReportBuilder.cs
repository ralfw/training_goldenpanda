using System.Collections.Generic;
using System.Linq;
using CodeChurnReport.Data;

namespace CodeChurnReport.Behavior.Core
{
    public static class ReportBuilder
    {
        public static IEnumerable<ReportItem> BuildReportItems(ProtocolItem[] protocollItems)
        {
            var dictinctFilePaths = protocollItems.GetDistinctFilePaths();

            foreach (var file in dictinctFilePaths)
                yield return Build_Report_Item(protocollItems, file);
        }

        private static ReportItem Build_Report_Item(ProtocolItem[] protocollItems, string file)
        {
            var fileItems = protocollItems.FilterByUncFilePath(file).ToArray();
            var reportItem = new ReportItem
            {
                UncFilePath = file,
                LinesOfCode = fileItems.Last().LineOfCode,
                ChurnRate = fileItems.GetChurnRate()
            };
            return reportItem;
        }
    }
}