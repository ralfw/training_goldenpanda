using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace BankOCR.UnitTest
{
    [TestFixture]
    public class SevenSegmentAccountTest
    {
        [Test]
        public void ShouldGenerateSingleAccountForDigit4()
        {
            SevenSegmentAccountNumber account = new SevenSegmentAccountNumber();

            account.Line1 = "   ";
            account.Line2 = "|_|";
            account.Line3 = "  |";

            account.GetDigits().Count().Should().Be(1);
            string result = account.GetDigits()[0].SevenSegments;
            result.Should().Be("   |_|  |");


        }
    }
}