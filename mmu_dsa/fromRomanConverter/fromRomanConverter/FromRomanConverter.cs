using System.Collections.Generic;
using System.Linq;

namespace fromRomanConverter
{
    public class FromRomanConverter
    {
        public static int[] Convert(char[] symbols)
        {
            return symbols.Select(ConvertRomanSymbol).ToArray();
        }

        public static int[] ConvertRomanNumberStringsToArabicIntegers(string[] romanNumberStrings)
        {
            return romanNumberStrings.Select(ConvertRomanNumberToArabic).ToArray();
        }

        public static int[] Sign(int[] numbers)
        {
            var result = new List<int>();
            for (var i = 0; i < numbers.Length - 1; i++)
                if (numbers[i] < numbers[i + 1])
                    result.Add(numbers[i] * -1);
                else
                    result.Add(numbers[i]);
            result.Add(numbers[numbers.Length - 1]);

            return result.ToArray();
        }

        public static char[] Split(string romanNumber)
        {
            return romanNumber.ToCharArray();
        }

        public static int Sum(int[] numbers)
        {
            return numbers.Sum();
        }

        #region Private methods

        private static int ConvertRomanNumberToArabic(string romanNumberStr)
        {
            var symbols = Split(romanNumberStr);
            var numbers = Convert(symbols);
            var signedNumbers = Sign(numbers);
            return Sum(signedNumbers);
        }

        private static int ConvertRomanSymbol(char romanNumber)
        {
            return SymbolValueMap[romanNumber];
        }

        #endregion

        #region Fields

        private static readonly IReadOnlyDictionary<char, int> SymbolValueMap = new Dictionary<char, int>
        {
            ['I'] = 1,
            ['V'] = 5,
            ['X'] = 10,
            ['L'] = 50,
            ['C'] = 100,
            ['D'] = 500,
            ['M'] = 1000
        };

        #endregion
    }
}