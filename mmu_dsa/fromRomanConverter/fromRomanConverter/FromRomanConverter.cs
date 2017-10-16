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
            {
                if (numbers[i] < numbers[i + 1])
                    result.Add(numbers[i] * -1);
                else
                    result.Add(numbers[i]);
            }
            result.Add(numbers[numbers.Length-1]);

            return result.ToArray();
        }

        public static char[] Split(string romanNumber)
        {
            return romanNumber.ToCharArray();
        }

        #region Private methods

        /// <summary>
        /// Implementation as suggested by 
        /// https://groups.google.com/forum/#!topic/de.comp.datenbanken.ms-access/tXICrqZ2EWc
        /// </summary>
        /// <param name="romanNumberStr"></param>
        /// <returns></returns>
        private static int ConvertRomanNumberToArabic(string romanNumberStr)
        {
            var arabicNumber = 0;

            for (var i = 0; i < romanNumberStr.Length; i++)
            {
                var substraction = 0;

                if (i < romanNumberStr.Length - 1)
                    substraction = ConvertRomanSubstractionSymbols(romanNumberStr[i], romanNumberStr[i + 1]);

                if (substraction != 0)
                {
                    arabicNumber += substraction;
                    i++;
                }
                else
                {
                    arabicNumber += ConvertRomanSymbol(romanNumberStr[i]);
                }
            }

            return arabicNumber;
        }

        private static int ConvertRomanSymbol(char romanNumber)
        {
            return SymbolValueMap[romanNumber];
        }

        private static int ConvertRomanSubstractionSymbols(char romanSymbol1, char romanSymbol2)
        {
            if (SymbolValueMap[romanSymbol1] < SymbolValueMap[romanSymbol2])
                return SymbolValueMap[romanSymbol2] - SymbolValueMap[romanSymbol1];

            return 0;
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

        public static int Sum(int[] numbers)
        {
            return numbers.Sum();
        }
    }
}