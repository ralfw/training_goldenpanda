using System.Collections.Generic;
using System.Linq;
using Common;

namespace Client
{
    public static class ReportGenerator
    {
        public static List<ReportEntry> GenerateReportEntries(List<ProtocolEntry> protocolEntries)
        {
            var reportEntryMap = new Dictionary<string, ReportEntry>();
            foreach (var protocolEntry in protocolEntries)
            {
                if (!reportEntryMap.ContainsKey(protocolEntry.FilePath))
                    reportEntryMap.Add(protocolEntry.FilePath, new ReportEntry(protocolEntry.FilePath, protocolEntry.Loc));

                if (protocolEntry.Loc != reportEntryMap[protocolEntry.FilePath].LastLoc)
                {
                    reportEntryMap[protocolEntry.FilePath].Churns++;
                    reportEntryMap[protocolEntry.FilePath].LastLoc = protocolEntry.Loc;
                }
            }

            return reportEntryMap.Values.ToList();
        }
    }
}