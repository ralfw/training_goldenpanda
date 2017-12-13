﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CodeChurnReport.Behavior.Core;
using CodeChurnReport.Data;

namespace CodeChurnReport.Behavior.Providers
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
                    TimeStamp = DateTime.Parse(fields[0]),
                    UncFilePath = fields[2],
                    LineOfCode = int.Parse(fields[1])
                }).ToList();
        }

        public static IEnumerable<ProtocolItem> ReadProtocol(string filePath)
        {
            var protocolLines = File.ReadAllLines(filePath);
            return GenerateProtocolItems(protocolLines);
        }


    }
}