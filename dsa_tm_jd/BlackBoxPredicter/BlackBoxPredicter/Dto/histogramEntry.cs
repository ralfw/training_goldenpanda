using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackBoxPredicter.Dto
{
    public class HistogramEntry
    {
        public int CycleTime { get; set; }

        public int Frequence { get; set; }

        public double Percentil { get; set; }

        public HistogramEntry(int cycleTime,int frequence, double percentil)
        {
            CycleTime = cycleTime;
            Frequence = frequence;
            Percentil = percentil;
        }

    }
}
