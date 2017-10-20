using System;
using System.Collections.Generic;

namespace TextSplitter
{
    public class TextSplitter
    {
        public string[] Split(string text)
        {
            return text.Split(new char[] {'\n', '\r', '\t', ' '});
        }

        public IEnumerable<string> LimitWords(string[] words, int limit)
        {
            var result = new List<string>();
            foreach (var word in words)
            {
                result.AddRange(LimitWord(word,limit));
            }
            return result;
        }

        public IEnumerable<string> LimitWord(string word, int limit)
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
    }
}