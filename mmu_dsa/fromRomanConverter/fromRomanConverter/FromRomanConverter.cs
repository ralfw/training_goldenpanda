using System.Collections.Generic;
using System.Linq;

namespace fromRomanConverter
{
    public class FromRomanConverter
    {
        public int[] ConvertRomanNumbersToArabInts(string[] inputRomanNumbers)
        {
            var result = new List<int>();

            foreach (string number in inputRomanNumbers)
            {
                result.Add(ConvertSingleNumber(number));
            }

            return result.ToArray();

          //  return inputRomanNumbers.Select(ConvertSingleNumber).ToArray();
        }

        #region Private methods

        /// <summary>
        /// Implementation as suggested by 
        /// https://groups.google.com/forum/#!topic/de.comp.datenbanken.ms-access/tXICrqZ2EWc
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private int ConvertSingleNumber(string input)
        {
            var sum = 0;
            for (var index = 0; index < input.Length; index++)
            {
                if (index < input.Length - 1)
                    if (SymbolValueMap[input[index]] < SymbolValueMap[input[index + 1]])
                    {
                        //toggle the sign
                        sum += SymbolValueMap[input[index + 1]] - SymbolValueMap[input[index]];
                        index++;
                        continue;
                    }
                sum += SymbolValueMap[input[index]];
            }
            return sum;
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
