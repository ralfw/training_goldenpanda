using System;

namespace TextWrap
{
    public static class TextWrapper
    {
        public static string[] SplitTextIntoWords(string row)
        {
            return row.Split(new[] {" ", Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string [] BuildRows(string [] words, int maxRowLength)
        {
            return words;
        }
    }
}
