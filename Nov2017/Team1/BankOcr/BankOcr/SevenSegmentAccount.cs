using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOcr
{
    public class SevenSegmentAccount
    {
        public string Line1 { get; private set; }
        public string Line2 { get; private set; }
        public string Line3 { get; private set; }

        public SevenSegmentAccount(string[] lines)
        {
            Line1 = lines[0];
            Line2 = lines[1];
            Line3 = lines[2];
        }
    }
}
