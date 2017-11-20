using System;

namespace Common
{
    public class ProtocolEntry
    {
        public DateTime Timestamp { get; private set; }
        public int Loc { get; private set; }
        public string FilePath { get; private set; }

        public void Parse(string line)
        {
            var parts = line.Split(';');
            Timestamp = DateTime.Parse(parts[0]);
            Loc = int.Parse(parts[1]);
            FilePath = parts[2].Trim();
        }
    }
}