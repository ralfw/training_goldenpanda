using System;
using System.Collections.Generic;
using System.Linq;

namespace TextSplitter
{
    public class TextSplitter
    {
        public static string WrapWords(int limit, string input)
        {
            var words = Split_into_words(input);
            words = Split_long_words(words, limit);
            words = GenerateLines(limit, words);
            return Concatenate(words);
        }
        
        
        public static IEnumerable<string> Split_into_words(string text)
        {
            return text.Split('\n', '\r', '\t', ' ');
        }
        
        
        private static IEnumerable<string> Split_long_words(IEnumerable<string> words, int limit) {
            return words.SelectMany(w => Split_long_word(w,limit));
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
        
        
        private static string Concatenate(IEnumerable<string> lines)
        {
            return string.Join("\n", lines);
        }
        

        public static IEnumerable<string> Split_long_word(string word, int limit)
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