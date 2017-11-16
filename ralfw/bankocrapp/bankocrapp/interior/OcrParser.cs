using System.Collections.Generic;
using bankocrapp.data;

namespace bankocrapp.interior
{
    class OcrParser
    {
        public static IEnumerable<OcrLine> Parse(string[] lines) {
            for (var i = 0; i < lines.Length; i += 4) {
                yield return new OcrLine(lines[i], lines[i+1], lines[i+2]);
            }
        }
    }
}