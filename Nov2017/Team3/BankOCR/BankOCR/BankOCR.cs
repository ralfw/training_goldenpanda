using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOCR
{
    public class BankOCR
    {
        public List<string> Decode(List<string> lines)
        {
            var sevenSegmentParser = new SevenSegmentParser();
            var sevenSegmentAccountNumbers = sevenSegmentParser.GroupLines(lines);

            var accountNumbers = Converter.Convert(sevenSegmentAccountNumbers.ToArray());

            return accountNumbers.ToList();
        }
    }
}
