using System.Collections.Generic;

namespace bankocrapp.data
{
    struct OcrLine
    {
        private readonly string[] _lines;
        
        public OcrLine(string line0, string line1, string line2) {
            _lines = new[] {line0, line1, line2};
        }

        public OcrChar[] Chars
        {
            get
            {
                var digits = new List<OcrChar>();
                for (var c = 0; c < _lines[0].Length; c += 3) {
                    var d = new OcrChar(
                            _lines[0][c+0],_lines[0][c+1],_lines[0][c+2],
                            _lines[1][c+0],_lines[1][c+1],_lines[1][c+2],
                            _lines[2][c+0],_lines[2][c+1],_lines[2][c+2]
                        );
                    digits.Add(d);
                }
                return digits.ToArray();
            }
        }
    }
}