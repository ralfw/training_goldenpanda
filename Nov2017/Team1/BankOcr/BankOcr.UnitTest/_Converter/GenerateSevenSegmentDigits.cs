using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace BankOcr.UnitTest._Converter
{
    [TestFixture]
    public class GenerateSevenSegmentDigits
    {
        [Test]
        public void ShouldReturnDigitsForGivenAccount()
        {
            var lines = new[]
            {
                "    _  _     _  _  _  _  _ ",
                "  | _| _||_||_ |_   ||_||_|",
                "  ||_  _|  | _||_|  ||_| _|",
            };
            var account = new SevenSegmentAccount(lines);

            var result = account.GenerateDigits();

            result.Length.Should().Be(9);
            result[5].Value.Should().Be(
                " _ " +
                "|_ " +
                "|_|");
        }
    }
}