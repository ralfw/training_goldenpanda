using System.Collections.Generic;
using System.Linq;

namespace BankOCR
{
    public class Converter
    {
        public static string[] Convert(string[] lines)
        {
            var sevenSegmentAccounts = SevenSegmentAccountParser.GroupLines(lines);
            return ConvertSevenSegmentAccounts(sevenSegmentAccounts);
        }

        public static string[] ConvertSevenSegmentAccounts(IEnumerable<SevenSegmentAccount> sevenSegmentAccounts)
        {
            return sevenSegmentAccounts.Select(ConvertSevenSegmentAccountNo).ToArray();
        }

        private static string ConvertSevenSegmentAccountNo(SevenSegmentAccount sevenSegmentAccount)
        {
            return new string(sevenSegmentAccount.Digits.Select(d => d.Map()).ToArray());
        }       
    }
}