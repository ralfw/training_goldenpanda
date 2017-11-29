using System;
using System.Collections.Generic;
using System.Linq;

namespace BankOCR
{
    public class Converter
    {
        public static string[] Convert(SevenSegmentAccountNumber[] input)
        {
            IList<string> resultsList = input.Select(GetAccount).ToList();

            return resultsList.ToArray();
        }

        #region Private methods

        private static String GetAccount(SevenSegmentAccountNumber acc)
        {
            var chars = acc.GetDigits().Select(_ => _.Map()).ToList();
            var account = chars.Aggregate("", (current, ch) => current + ch);
            return account;
        }

        #endregion
    }
}