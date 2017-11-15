using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace BankOcr.UnitTest._Converter
{
    public class ConvertSevenSegmentDigitsToAccount
    {
        private const string ValueOf1 =
            "   " +
            "  |" +
            "  |";

        private const string ValueOf2 =
            " _ " +
            " _|" +
            "|_ ";

        private const string ValueOf3 =
            " _ " +
            " _|" +
            " _|";

        [Test]
        public void ShouldConvertGivenDigitsToAccount()
        {
            var digits = new List<SevenSegmentDigit>
            {
                new SevenSegmentDigit(ValueOf1),
                new SevenSegmentDigit(ValueOf2),
                new SevenSegmentDigit(ValueOf3)
            };

            var account = Converter.ConvertSevenSegmentDigitsToAccount(digits.ToArray());

            account.Should().Be("123");
        }
    }
}