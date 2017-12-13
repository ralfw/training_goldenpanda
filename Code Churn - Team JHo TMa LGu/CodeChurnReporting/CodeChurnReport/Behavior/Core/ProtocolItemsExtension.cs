using System;
using System.Collections.Generic;
using System.Linq;
using CodeChurnReport.Data;

namespace CodeChurnReport.Behavior.Core
{
    public static class ProtocolItemsExtension
    {
        public static IEnumerable<ProtocolItem> FilterByTimeSpan(this IEnumerable<ProtocolItem> source,
            DateTime startDate, DateTime endDate)
        {
            return source.Where(item => item.TimeStamp >= startDate && item.TimeStamp <= endDate).ToList();
        }

        public static IEnumerable<ProtocolItem> FilterByUncFilePath(this IEnumerable<ProtocolItem> source,
            string uncFilePath)
        {
            return source.Where(item => item.UncFilePath == uncFilePath).ToList();
        }

        public static IEnumerable<string> GetDistinctFilePaths(this IEnumerable<ProtocolItem> protocolItems)
        {
            return new HashSet<string>(protocolItems.Select(item => item.UncFilePath));
        }

        public static int GetChurnRate(this IEnumerable<ProtocolItem> protocolItems)
        {
            var churns = 0;
            var previousLinesOfCode = -1;
            var items = protocolItems.ToArray();
            if (items.Length == 0)
                return 0;
            foreach (var item in items)
            {
                if (previousLinesOfCode == -1)
                {
                    previousLinesOfCode = item.LineOfCode;
                    churns++;
                    continue;
                }
                if (previousLinesOfCode == item.LineOfCode)
                    continue;
                previousLinesOfCode = item.LineOfCode;
                churns++;
            }
            return churns;
        }

    }
}