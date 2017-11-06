using System;
using System.Collections.Generic;
using System.Linq;

namespace TextSplitter
{
    public class TextSplitter
    {
        public static string WrapWords(int limit, string input)
        {
            var lines = Split(input);
            lines = LimitWords(lines, limit);
            lines = GenerateLines(limit, lines);
            return Concatenate(lines);
        }
        public static IEnumerable<string> Split(string text)
        {
            return text.Split('\n', '\r', '\t', ' ');
        }
        public static IEnumerable<string> LimitWords(IEnumerable<string> words, int limit)
        {
            var result = new List<string>();
            foreach (var word in words)
            {
                result.AddRange(LimitWord(word, limit));
            }
            return result;
        }
        public static IEnumerable<string> GenerateLines(int limit, IEnumerable<string> words)
        {
            var result = new List<string>();
            var lineNumber = 0;
            foreach (var word in words)
            {
                if (result.Count == 0)
                {
                    result.Add(word);
                    continue;
                }
                if (result[lineNumber].Length + word.Length < limit)
                {
                    result[lineNumber] += $" {word}";
                }
                else
                {
                    result.Add(word);
                    lineNumber++;
                }
            }

            return result;
        }
        public static string Concatenate(IEnumerable<string> lines)
        {
            return string.Join("\n", lines);
        }

        #region Private methods
        private static IEnumerable<string> LimitWord(string word, int limit)
        {
            var result = new List<string>();
            var currentWord = word;
            while (currentWord.Length > 0)
            {
                var charCount = Math.Min(limit, currentWord.Length);
                var limitedWord = currentWord.Substring(0, charCount);
                result.Add(limitedWord);
                currentWord = currentWord.Substring(charCount, currentWord.Length - charCount);
            }

            return result;
        }
        #endregion
    }
}