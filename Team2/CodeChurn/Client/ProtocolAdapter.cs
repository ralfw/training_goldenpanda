using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common;

namespace Client
{
    public static class ProtocolAdapter
    {
        public static List<ProtocolEntry> CollectProtocolEntries(string filePath)
        {
            var lines = ReadAllLines(filePath);
            return GetProtocolEntries(lines);
        }

        #region Private methods

        private static List<string> ReadAllLines(string filePath)
        {
            return File.ReadAllLines(filePath).ToList();
        }

        private static List<ProtocolEntry> GetProtocolEntries(IEnumerable<string> lines)
        {
            return lines.Select(ProtocolEntry.Parse).ToList();
        }

        #endregion
    }
}