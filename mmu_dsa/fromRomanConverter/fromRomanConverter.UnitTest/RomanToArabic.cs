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
        [TestCase("V", 5)]
        [TestCase("X", 10)]
        [TestCase("L", 50)]
        [TestCase("C", 100)]
        [TestCase("D", 500)]
        [TestCase("M", 1000)]
        public void ShouldConvertSingleSymbol(string romanNumberStr, int expected)
        {
            FromRomanConverter.ConvertRomanNumberStringsToArabicIntegers(new[]{ romanNumberStr}).Should().Contain(expected);
        }

        [TestCase("II", 2)]
        [TestCase("III", 3)]
        [TestCase("VV", 10)]
        [TestCase("LL", 100)]
        [TestCase("XX", 20)]
        [TestCase("XXX", 30)]
        [TestCase("CC", 200)]
        [TestCase("CCC", 300)]
        [TestCase("MM", 2000)]
        [TestCase("MMM", 3000)]
        public void ShouldConvertMultipleIdenticalSymbols(string romanNumberStr, int expected)
        {
            FromRomanConverter.ConvertRomanNumberStringsToArabicIntegers(new[]{ romanNumberStr}).Should().Contain(expected);
        }

        [TestCase("VI", 6)]
        [TestCase("VII", 7)]
        [TestCase("VIII", 8)]
        [TestCase("XI", 11)]
        [TestCase("XIII", 13)]
        [TestCase("XV", 15)]
        [TestCase("XVII", 17)]
        [TestCase("LI", 51)]
        [TestCase("LII", 52)]
        [TestCase("LX", 60)]
        [TestCase("LXII", 62)]
        [TestCase("LXV", 65)]
        [TestCase("LXVI", 66)]
        [TestCase("LXXIII", 73)]
        [TestCase("LXXXVI", 86)]
        [TestCase("MI", 1001)]
        [TestCase("MV", 1005)]
        [TestCase("MVIII", 1008)]
        [TestCase("MX", 1010)]
        [TestCase("MXV", 1015)]
        [TestCase("MXVII", 1017)]
        [TestCase("MXXXVII", 1037)]
        [TestCase("ML", 1050)]
        [TestCase("MC", 1100)]
        [TestCase("MD", 1500)]
        [TestCase("MLX", 1060)]
        [TestCase("MCV", 1105)]
        [TestCase("MDC", 1600)]
        [TestCase("MLXI", 1061)]
        [TestCase("MCVII", 1107)]
        [TestCase("MDCII", 1602)]
        [TestCase("MM", 2000)]
        public void ShouldConvertMultipleMixedSymbols(string romanNumberStr, int expected)
        {
            FromRomanConverter.ConvertRomanNumberStringsToArabicIntegers(new[]{ romanNumberStr}).Should().Contain(expected);
        }

        [TestCase("IV", 4)]
        [TestCase("IX", 9)]
        [TestCase("XL", 40)]
        [TestCase("IC", 99)]
        [TestCase("VC", 95)]
        [TestCase("XC", 90)]
        [TestCase("LC", 50)]
        [TestCase("IM", 999)]
        [TestCase("VM", 995)]
        [TestCase("XM", 990)]
        [TestCase("LM", 950)]
        [TestCase("ILM", 1049)]
        public void ShouldConvertSymbolsWithDifference(string romanNumberStr, int expected)
        {
            FromRomanConverter.ConvertRomanNumberStringsToArabicIntegers(new[]{ romanNumberStr}).Should().Contain(expected);
        }

        [TestCase(new[] { "CLXXXVIII", "CXLIX", "MMMDCCCXLV" }, new[] { 188, 149, 3845 })]
        public void ShouldConvertFromStringArray(string[] romanNumberStr, int[] expected)
        {
            FromRomanConverter.ConvertRomanNumberStringsToArabicIntegers( romanNumberStr).Should().Contain(expected);
        }

    }

}