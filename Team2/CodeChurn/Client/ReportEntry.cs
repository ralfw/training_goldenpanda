namespace Client
{
    public class ReportEntry
    {
        public string FilePath { get; }
        public int LastLoc { get; set; }
        public int Churns { get; set; }

        public ReportEntry(string filePath, int lastLoc)
        {
            FilePath = filePath;
            LastLoc = lastLoc;
        }

        public override string ToString()
        {
            return $"{LastLoc};{Churns};{FilePath}";
        }
    }
}