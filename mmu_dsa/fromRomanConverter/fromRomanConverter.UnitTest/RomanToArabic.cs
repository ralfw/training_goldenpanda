using FluentAssertions;
using NUnit.Framework;

namespace fromRomanConverter.UnitTest
{
    [TestFixture]
    public class RomanToArabic
    {
        [TestCase(new[] {"I", "II", "III"}, new[] {1, 2, 3})]
        public void ShouldConvertFromStringArray(string[] romanNumberStr, int[] expected)
        {
            FromRomanConverter.ConvertRomanNumberStringsToArabicIntegers(romanNumberStr).Should().Contain(expected);
        }

        [TestCase("I", 1)]
        [TestCase("V", 5)]
        [TestCase("X", 10)]
        [TestCase("L", 50)]
        [TestCase("C", 100)]
        [TestCase("D", 500)]
        [TestCase("M", 1000)]
        public void ShouldConvertSingleSymbol(string romanNumberStr, int expected)
        {
            FromRomanConverter.ConvertRomanNumberStringsToArabicIntegers(new[] {romanNumberStr}).Should().Contain(expected);
        }

        [TestCase("MXLIX", 1049)]
        public void ShouldConvertSymbolsWithNegativeSigns(string romanNumberStr, int expected)
        {
            FromRomanConverter.ConvertRomanNumberStringsToArabicIntegers(new[] {romanNumberStr}).Should().Contain(expected);
        }

        [TestCase("LXVI", 66)]
        public void ShouldConvertSymbolsWithoutNegativeSigns(string romanNumberStr, int expected)
        {
            FromRomanConverter.ConvertRomanNumberStringsToArabicIntegers(new[] {romanNumberStr}).Should().Contain(expected);
        }
    }
}