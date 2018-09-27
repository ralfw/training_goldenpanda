using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackBoxPredicter.Dto
{
    class Histogram
    {
        public IList<HistogramEntry> Entries { get; set; }
        public int MarkerIndex { get; set; }
        public float MarkerValue { get; set; }
    }
}
