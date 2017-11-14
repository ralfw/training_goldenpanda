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
            return new string(decDigits.ToArray());
        }
    }


    class SevenSAccountNoParser
    {
        public static IEnumerable<SevenSAccountNo> Parse(string[] lines) {
            for (var i = 0; i < lines.Length; i += 4) {
                yield return new SevenSAccountNo(lines[i], lines[i+1], lines[i+2]);
            }
        }
    }
}