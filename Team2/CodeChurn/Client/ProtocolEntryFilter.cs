using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace Client
{
    public static class ProtocolEntryFilter
    {
        public static List<ProtocolEntry> Filter(List<ProtocolEntry> entries, DateTime start, DateTime end)
        {
            return entries.Where(entry => entry.Timestamp >= start && entry.Timestamp <= end).ToList();
        }
    }
}