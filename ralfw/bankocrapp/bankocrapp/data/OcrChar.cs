﻿namespace bankocrapp.data
{
    struct OcrChar
    {
        private readonly string _segments;
        
        public OcrChar(char s0, char s1, char s2, char s3, char s4, char s5, char s6, char s7, char s8) {
            _segments = new string(new[]{s0,s1,s2,s3,s4,s5,s6,s7,s8});
            _segments = _segments.Replace(" ", ".").Replace("_", "x").Replace("I", "x").Replace("|", "x");
        }

        public char ToDecimalDigit()
        {
            switch (_segments) {
                case ".....x..x": return '1';
                case ".x..xxxx.": return '2';
                case ".x..xx.xx": return '3';
                case "...xxx..x": return '4';
                case ".x.xx..xx": return '5';
                case ".x.xx.xxx": return '6';
                case ".x...x..x": return '7';
                case ".x.xxxxxx": return '8';
                case ".x.xxx.xx": return '9';
                case ".x.x.xxxx": return '0';
                default: return '?';
            }
        }
    }
}