using bankocrapp.data;

namespace bankocrapp.interior
{
    class Ocr
    {
        public static char Recognize(OcrChar ocrChar)
        {
            switch (ocrChar.Segments) {
                case "...x..x": return '1';
                case "x.xxxx.": return '2';
                case "x.xx.xx": return '3';
                case ".xxx..x": return '4';
                case "xxx..xx": return '5';
                case "xxx.xxx": return '6';
                case "x..x..x": return '7';
                case "xxxxxxx": return '8';
                case "xxxx.xx": return '9';
                case "xx.xxxx": return '0';
                default: return '?';
            }
        }
    }
}