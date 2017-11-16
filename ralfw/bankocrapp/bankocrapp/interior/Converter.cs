using System.Collections.Generic;
using System.Linq;
using bankocrapp.data;

namespace bankocrapp.interior
{
    internal static class Converter
    {
        public static string[] convert(string[] lines)
        {
            var accNos = OcrParser.Parse(lines);
            return convert(accNos);
        }

        static string[] convert(IEnumerable<OcrLine> accNos) {
            return accNos.Select(convert).ToArray();
        }

        static string convert(OcrLine accNo)
        {
            var decDigits = accNo.Chars.Select(d => d.ToDecimalDigit());
            var accNoText = new string(decDigits.ToArray());
            if (accNoText.Contains("?")) accNoText = "Fehlerhafte Kontonummer!";
            return accNoText;
        }
    }
}