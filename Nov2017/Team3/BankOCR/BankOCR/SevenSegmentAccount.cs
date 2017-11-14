using System.Collections.Generic;

namespace BankOCR
{
    public class SevenSegmentAccount
    {
        public SevenSegmentDigit[] GetDigits()
        {
            List<SevenSegmentDigit> arrResult = new List<SevenSegmentDigit>();

            for (int i = 0; i < line1.Length; i = i + 3)
            {
                string val = line1[i].ToString() + line1[i + 1] + line1[i + 2];
                val = val + line2[i] + line2[i + 1] + line2[i + 2];
                val = val + line3[i] + line3[i + 1] + line3[i + 2];

                SevenSegmentDigit digit = new SevenSegmentDigit(val);

                arrResult.Add(digit);
            }

            return arrResult.ToArray();
        }

        #region Fields

        public string line1;
        public string line2;
        public string line3;

        #endregion
    }
}