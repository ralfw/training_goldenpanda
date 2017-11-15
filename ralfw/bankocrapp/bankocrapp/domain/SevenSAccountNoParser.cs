using System.Collections.Generic;

namespace bankocrapp
{
    class SevenSAccountNoParser
    {
        public static IEnumerable<SevenSAccountNo> Parse(string[] lines) {
            for (var i = 0; i < lines.Length; i += 4) {
                yield return new SevenSAccountNo(lines[i], lines[i+1], lines[i+2]);
            }
        }
    }
}