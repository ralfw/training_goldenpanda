using System.Collections.Generic;

namespace BankOCR
{
    public class Converter
    {
        public static string[] Convert(string[] lines)
        {
            var sevenSegmentAccounts = SevenSegmentAccountParser.GroupLines(lines);
            return ConvertSevenSegmentAccounts(sevenSegmentAccounts);
        }

    public static string ConvertSevenSegmentDigitsToAccountNo(List<SevenSegmentDigit> digits)
        {
            var accountNo = string.Empty;
            foreach (var digit in digits)
            {
                accountNo += digit.Map();
            }
            return accountNo;
        }

        public static string[] ConvertSevenSegmentAccounts(SevenSegmentAccount[] sevenSegmentAccounts)
        {
            var accountNos = new List<string>();
            foreach (var sevenSegmentAccount in sevenSegmentAccounts)
            {
                var sevenSegmentDigits = sevenSegmentAccount.GenerateSevenSegmentDigits();
                var accountNo = ConvertSevenSegmentDigitsToAccountNo(sevenSegmentDigits);
                accountNos.Add(accountNo);
            }
            return accountNos.ToArray();
        }
    }
}