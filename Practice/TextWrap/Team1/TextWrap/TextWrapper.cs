using System;
using System.Collections.Generic;

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
            List<string> rows = new List<string>();
            var row = "";

            foreach (var word in words)
            {
                var space = row.Length == 0 ? 0 : 1;

                if (row.Length + word.Length + space > maxRowLength)
                {
                    rows.Add(row);
                    row = string.Empty;
                }

                if (row.Length > 0)
                    row += " ";

                row += word;
            }

            rows.Add(row);
            return rows.ToArray();
        }

        public static string[] WrapTooLongWords(string[] words, int maxRowLength)
        {
            var newWords = new List<string>();
            foreach (var word in words)
            {
                if (word.Length <= maxRowLength)
                    newWords.Add(word);
                else
                {
                    var tempWord = word;
                    do
                    {
                        
                        if (tempWord.Length > maxRowLength)
                        {
                            newWords.Add(tempWord.Substring(0, maxRowLength));
                            tempWord = tempWord.Substring(maxRowLength);
                        }
                        else
                        {
                            newWords.Add(tempWord);
                            tempWord = String.Empty;
                        }
                    } while (tempWord.Length > 0);
                }
            }
            return newWords.ToArray();
        }

        public static string BuildText(string[] rows)
        {
            var text = string.Empty;
            foreach (var row in rows)
            {
                if (!string.IsNullOrEmpty(text))
                    text += Environment.NewLine;
                
                text += row;
            }
            return text;
        }

        public static string Wrap(string text, int maxRowLength)
        {
            var words = SplitTextIntoWords(text);
            var wrappedWords = WrapTooLongWords(words, maxRowLength);
            var rows = BuildRows(wrappedWords, maxRowLength);
            return BuildText(rows); ;
        }
    }
}
