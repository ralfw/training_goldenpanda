using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOCR
{
    public class SevenSegmentDigit
    {
        public SevenSegmentDigit(string segments)
        {
            SevenSegments = segments;
        }
        public string SevenSegments { get; set; }

        public char Map(string segmentString)
        {
            return '1';
        }
    }
}
