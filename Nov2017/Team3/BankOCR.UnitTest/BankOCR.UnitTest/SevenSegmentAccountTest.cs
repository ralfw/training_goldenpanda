using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace BankOCR.UnitTest
{
    [TestFixture]
    public class SevenSegmentAccountTest
    {
        [Test]
        public void ShouldGenerateSingleAccountForDigit1()
        {
            SevenSegmentAccountNumber account = new SevenSegmentAccountNumber
            {
                Line1 = line1[0],
                Line2 = line1[1],
                Line3 = line1[2]
            };


            account.GetDigits().Count().Should().Be(1);
            string result = account.GetDigits()[0].SevenSegments;
            result.Should().Be(segments1);
        }

        [Test]
        public void ShouldGenerateSingleAccountForDigit2()
        {
            SevenSegmentAccountNumber account = new SevenSegmentAccountNumber();

            account.Line1 = line2[0];
            account.Line2 = line2[1];
            account.Line3 = line2[2];

            account.GetDigits().Count().Should().Be(1);
            string result = account.GetDigits()[0].SevenSegments;
            result.Should().Be(segments2);
        }

        [Test]
        public void ShouldGenerateTwoAccountaForDigit12()
        {
            SevenSegmentAccountNumber account = new SevenSegmentAccountNumber();

            account.Line1 = line1[0] + line2[0];
            account.Line2 = line1[1] + line2[1];
            account.Line3 = line1[2] + line2[2];

            account.GetDigits().Count().Should().Be(2);
            string result1 = account.GetDigits()[0].SevenSegments;
            string result2 = account.GetDigits()[1].SevenSegments;
            result1.Should().Be(segments1);
            result2.Should().Be(segments2);
        }

        private readonly string[] line1 =
        {
            "   ",
            "  I",
            "  I"
        };

        private readonly string segments1 = "   " +
                                            "  I" +
                                            "  I";

        private readonly string[] line2 =
        {
            " _ ",
            " _I",
            " I_"
        };

        private readonly string segments2 = " _ " +
                                            " _I" +
                                            " I_";

        private string[] line4 = {
            "   ",
            "I_I",
            "  I"
        };

        private string segments4 = "   " +
                                   "I_I" +
                                   "  I";
    }
}