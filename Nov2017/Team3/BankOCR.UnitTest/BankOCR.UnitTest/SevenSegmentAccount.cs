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
            SevenSegmentAccount account = new SevenSegmentAccount();

            account.line1 = "   ";
            account.line2 = "|_|";
            account.line3 = "  |";

            account.GetDigits().Count().Should().Be(1);
            string result = account.GetDigits()[0].SevenSegments;
            result.Should().Be("   |_|  |");


        }
    }
}