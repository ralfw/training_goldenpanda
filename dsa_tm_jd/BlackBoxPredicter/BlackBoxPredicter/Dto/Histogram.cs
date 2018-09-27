using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackBoxPredicter.Dto
{
    internal class Histogram
    {
        public IList<HistogramEntry> Entries { get; set; } = new List<HistogramEntry>();
        public int MarkerIndex { get; set; }
        public float MarkerValue { get; set; }

    }
}
