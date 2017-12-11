using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CodeChurnReport.Structs;

namespace CodeChurnReport
{
    public static class ProtocolReader
    {
        public static IEnumerable<ProtocolItem> GenerateProtocolItems(IEnumerable<string> lines)
        {
            return (from line in lines
                select line.Split(';')
                into fields
                where fields.Length == 3
                select new ProtocolItem
                {
                    TimeStamp = DateTime.ParseExact(fields[0], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    UncFilePath = fields[2],
                    LineOfCode = int.Parse(fields[1])
                }).ToList();
        }

        public static IEnumerable<ProtocolItem> ReadProtocol(Config config)
        {
            var protocolLines = File.ReadAllLines(config.ProtocolFilePath);
            var protocollItems = GenerateProtocolItems(protocolLines);
            return protocollItems.FilterByTimeSpan(config.StartDate, config.EndDate);
        }


    }
}