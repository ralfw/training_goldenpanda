using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace bankocrapp
{
    class Converter
    {
        public static string[] convert(string[] lines)
        {
            var accNos = SevenSAccountNoParser.Parse(lines);
            return convert(accNos);
        }

        static string[] convert(IEnumerable<SevenSAccountNo> accNos) {
            return accNos.Select(convert).ToArray();
        }

        static string convert(SevenSAccountNo accNo)
        {
            var decDigits = accNo.Digits.Select(d => d.ToDecimalDigit());
            var accNoText = new string(decDigits.ToArray());
            if (accNoText.Contains("?")) accNoText = "Fehlerhafte Kontonummer!";
            return accNoText;
        }
    }
}