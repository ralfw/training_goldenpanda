using System;

namespace bankocrapp
{
    struct SevenSDigit
    {
        public SevenSDigit(char s0, char s1, char s2, char s3, char s4, char s5, char s6, char s7, char s8) {
            Segments = new string(new[]{s0,s1,s2,s3,s4,s5,s6,s7,s8});
            Segments = Segments.Replace(" ", ".").Replace("_", "x").Replace("I", "x").Replace("|", "x");
        }
        
        private string Segments { get; }

        public char ToDecimalDigit()
        {
            switch (Segments) {
                case ".....x..x": return '1';
                case ".x..xxxx.": return '2';
                default: return '?';
            }
        }
    }
}