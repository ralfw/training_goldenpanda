using System;

namespace Common
{
    public class ProtocolEntry
    {
        public DateTime Timestamp { get; private set; }
        public int Loc { get; private set; }
        public string FilePath { get; private set; }

        public ProtocolEntry(DateTime timestamp, int loc, string filePath)
        {
            Timestamp = timestamp;
            Loc = loc;
            FilePath = filePath;
        }

        public static ProtocolEntry Parse(string line)
        {
            var parts = line.Split(';');
            return new ProtocolEntry(DateTime.Parse(parts[0]), int.Parse(parts[1]), parts[2].Trim());
        }
    }
}