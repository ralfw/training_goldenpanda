using System.Collections.Generic;

namespace BankOCR
{
    public class Converter
    {
        public static string[] Convert(string[] lines)
        {
            return new[] {""};
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
    }
}