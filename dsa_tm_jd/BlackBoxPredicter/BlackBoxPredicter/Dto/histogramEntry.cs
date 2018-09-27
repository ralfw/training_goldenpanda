namespace BlackBoxPredicter.Dto
{
    public class HistogramEntry
    {
        public int CycleTime { get; set; }

        public int Frequency { get; set; }

        public double Percentile { get; set; }

        public HistogramEntry(int cycleTime,int frequency, double percentile)
        {
            CycleTime = cycleTime;
            Frequency = frequency;
            Percentile = percentile;
        }
    }
}
