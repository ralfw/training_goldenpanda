using System.Collections.Generic;

namespace bbp.dto
{
    internal class PredictorResultEnumerable : List<PredictorResult>, IPredictorResultEnumerable
    {
        public int ReliabilityLevelIndex { get; private set; }

        public void SetReliabilityLevelIndex(int index)
        {
            ReliabilityLevelIndex = index;
        }
    }
}