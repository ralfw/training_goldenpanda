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
            var sut = new FromRomanConverter();

            sut.ConvertRomanNumbersToArabInts(new[]{ romanNumberStr}).Should().Contain(expected);
        }
        [TestCase(new[] { "CLXXXVIII", "CXLIX", "MMMDCCCXLV" }, new[] { 188, 149, 3845 })]
        public void ShouldConvert(string[] romanNumberStr, int[] expected)
        {
            var sut = new FromRomanConverter();

            sut.ConvertRomanNumbersToArabInts( romanNumberStr).Should().Contain(expected);
        }

    }

}