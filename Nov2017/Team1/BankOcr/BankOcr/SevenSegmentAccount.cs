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

        public SevenSegmentDigit[] GenerateDigits()
        {
            var digits = new List<SevenSegmentDigit>();
            for (var i = 0; i < Line1.Length; i+=3)
            {
                var value = "";

                value += Line1.Substring(i, 3);
                value += Line2.Substring(i, 3);
                value += Line3.Substring(i, 3);

                digits.Add(new SevenSegmentDigit(value));
            }

            return digits.ToArray();
        }
    }


}
