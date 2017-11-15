using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOCR
{
    public class Integrator
    {
        public List<string> Convert(List<string> lines)
        {
            var sevenSegmentParser = new SevenSegmentParser();
            var sevenSegmentAccountNumbers = sevenSegmentParser.GroupLines(lines);

            var converter = new Converter();
            var accountNumbers = converter.Convert(sevenSegmentAccountNumbers);

            return accountNumbers;
        }
    }
}
