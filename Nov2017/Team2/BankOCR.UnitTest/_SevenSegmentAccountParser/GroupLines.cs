using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace BankOCR.UnitTest._SevenSegmentAccountParser
{
    [TestFixture]
    public class GroupLines
    {
        [Test]
        public void ShouldGroupLines()
        {
            var lines = new List<string>
            {
                "    _  _     _  _  _  _  _ ",
                "  | _| _||_||_ |_   ||_||_|",
                "  ||_  _|  | _||_|  ||_| _|",
                "                           ",
                "    _  _  _  _  _  _     _ ",
                "|_||_|| || ||_   |  |  ||_ ",
                "  | _||_||_||_|  |  |  | _|"
            };
            var sevenSegmentAccounts = SevenSegmentAccountParser.GroupLines(lines.ToArray());

            sevenSegmentAccounts.Should().HaveCount(2);

            sevenSegmentAccounts[0].SevenSegmentAccountNumber[2].Should().Be("  ||_  _|  | _||_|  ||_| _|");
            sevenSegmentAccounts[1].SevenSegmentAccountNumber[2].Should().Be("  | _||_||_||_|  |  |  | _|");
        }
    }
}