using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOCR
{
    public class SevenSegmentAccount
    {
        public string[] SevenSegmentAccountNumber { get; }

        public SevenSegmentAccount(string[] sevenSegmentAccountNumber)
        {
            SevenSegmentAccountNumber = sevenSegmentAccountNumber;
        }

        public List<SevenSegmentDigit> GenerateSevenSegmentDigits()
        {
            var sevenSegmentDigits = new List<SevenSegmentDigit>();
            for (int i = 0; i < SevenSegmentAccountNumber[0].Length; i = i + 3)
            {
                sevenSegmentDigits.Add(new SevenSegmentDigit(SevenSegmentAccountNumber[0].Substring(i,3) 
                    + SevenSegmentAccountNumber[1].Substring(i, 3)
                    + SevenSegmentAccountNumber[2].Substring(i, 3)));
            }
            return sevenSegmentDigits;
        }
    }
}
