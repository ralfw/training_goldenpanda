using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOCR
{
    public class Converter
    {
        public static string[] Convert(SevenSegmentAccountNumber[] input)
        {
            IList<string> resultsList = new List<String>();

            foreach (var acc in input)
            {
                var chars = acc.GetDigits().Select(_ => _.Map()).ToList();

                string account = chars.Aggregate("", (current, ch) => current + ch);

                resultsList.Add(account);
            }


            return resultsList.ToArray();

        }

    }
}
