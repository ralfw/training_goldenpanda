using System.Collections.Generic;

namespace BankOCR
{
    public class SevenSegmentAccountNumber
    {
        public SevenSegmentDigit[] GetDigits()
        {
            List<SevenSegmentDigit> arrResult = new List<SevenSegmentDigit>();

            for (int i = 0; i < Line1.Length; i = i + 3)
            {
                string val = Line1[i].ToString() + Line1[i + 1] + Line1[i + 2];
                val = val + Line2[i] + Line2[i + 1] + Line2[i + 2];
                val = val + Line3[i] + Line3[i + 1] + Line3[i + 2];

                SevenSegmentDigit digit = new SevenSegmentDigit(val);

                arrResult.Add(digit);
            }

            return arrResult.ToArray();
        }

        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
    }
}