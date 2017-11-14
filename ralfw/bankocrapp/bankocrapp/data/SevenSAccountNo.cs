using System;
using System.Collections.Generic;

namespace bankocrapp
{
    struct SevenSAccountNo
    {
        public SevenSAccountNo(string line0, string line1, string line2) {
            Lines = new[] {line0, line1, line2};
        }
        
        private string[] Lines { get; }

        public SevenSDigit[] Digits
        {
            get
            {
                var digits = new List<SevenSDigit>();
                for (var c = 0; c < Lines[0].Length; c += 3) {
                    var d = new SevenSDigit(
                            Lines[0][c+0],Lines[0][c+1],Lines[0][c+2],
                            Lines[1][c+0],Lines[1][c+1],Lines[1][c+2],
                            Lines[2][c+0],Lines[2][c+1],Lines[2][c+2]
                        );
                    digits.Add(d);
                }
                return digits.ToArray();
            }
        }
    }
}