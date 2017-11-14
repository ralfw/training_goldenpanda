using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOcr
{
    public class SevenSegmentAccountParser
    {
        public static SevenSegmentAccount[] GroupLines(string[] lines)
        {
            return new SevenSegmentAccount[]
            {
                new SevenSegmentAccount(), 
                new SevenSegmentAccount(),
            };
        }
    }
}
