using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Client
{
    public static class ReportAdapter
    {
        public static string PersistReport(List<ReportEntry> entries, DateTime start, DateTime end, string filePath)
        {
            var csvFilePath = Path.Combine(filePath, $"churn_report_{start:yyyy-MM-dd}_{end:yyyy-MM-dd}.csv");
            File.WriteAllLines(csvFilePath, entries.Select(entry => entry.ToString()));
            return csvFilePath;
        }
    }
}