namespace bbp.dto
{
    internal class PredictorResult
    {
        public PredictorResult(int duration, float accumulatedPercentile, int frequency)
        {
            Duration = duration;
            AccumulatedPercentile = accumulatedPercentile;
            Frequency = frequency;
        }

        public int Duration { get; }
        public float AccumulatedPercentile { get; }
        public int Frequency { get; }
    }
}