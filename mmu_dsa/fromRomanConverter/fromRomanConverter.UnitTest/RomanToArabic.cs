using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace fromRomanConverter.UnitTest
{
    [TestFixture]
    //[Ignore("Manual")]
    public class RomanToArabic
    {
        [TestCase("I", 1)]
        [TestCase("II", 2)]
        [TestCase("III", 3)]
        [TestCase("V", 5)]
        [TestCase("IV", 4)]
        [TestCase("IX", 9)]
        [TestCase("XL", 40)]
        [TestCase("XC", 90)]
        [TestCase("CD", 400)]
        [TestCase("CM", 900)]
        [TestCase("VIII", 8)]
        [TestCase("CXLVIII", 148)]
        [TestCase("CDXCV", 495)]
        [TestCase("CMXCIX", 999)]
        [TestCase("MDCCLXXXIII", 1783)]
        [TestCase("MDCCCLXXXIX", 1889)]
        [TestCase("MMMDCCCXLV", 3845)]
        [TestCase("MMM", 3000)]
        [TestCase("MM", 2000)]
        [TestCase("MMDCCLXI", 2761)]
        [TestCase("MCMXXXII", 1932)]
        [TestCase("MDCLXXXVIII", 1688)]
        [TestCase("MDXCV", 1595)]
        [TestCase("CMLXXXVIII", 988)]
        [TestCase("CD", 400)]
        [TestCase("CDXLIX", 449)]
        [TestCase("CCIV", 204)]
        [TestCase("CXCIV", 194)]
        [TestCase("CXLIX", 149)]
        [TestCase("CLXXXVIII", 188)]
        public void ShouldConvert(string romanNumberStr, int expected)
        {
            var sut = new ToArabicConverter();

            sut.Convert(romanNumberStr).Should().Be(expected);
        }
    }
    public class ToArabicConverter
    {
        public int Convert(string numberStr)
        {
            var result = 0;

            for (int pos = 0; pos < numberStr.Length; pos++)
            {
                if (pos + 1 < numberStr.Length)
                {
                    int subtractValue;
                    if (CheckForSubtractions(numberStr.Substring(pos, 2), out subtractValue))
                    {
                        result += subtractValue;
                        pos += 1;
                        continue;
                    }
                }

                result += _romanNumbers[numberStr[pos].ToString()];
            }

            return result;
        }

        private bool CheckForSubtractions(string subString, out int result)
        {
            result = 0;

            if (subString == "IV")
            {
                result = 4;
                return true;
            }
            if (subString == "IX")
            {
                result = 9;
                return true;
            }
            if (subString == "XL")
            {
                result = 40;
                return true;
            }
            if (subString == "XC")
            {
                result = 90;
                return true;
            }
            if (subString == "CD")
            {
                result = 400;
                return true;
            }
            if (subString == "CM")
            {
                result = 900;
                return true;
            }

            return false;
        }

        #region Fields

        private readonly Dictionary<string, int> _romanNumbers = new Dictionary<string, int> {
                                                                                                 { "I", 1 },
                                                                                                 { "V", 5 },
                                                                                                 { "X", 10 },
                                                                                                 { "L", 50 },
                                                                                                 { "C", 100 },
                                                                                                 { "D", 500 },
                                                                                                 { "M", 1000 }
                                                                                             };

        #endregion
    }

}