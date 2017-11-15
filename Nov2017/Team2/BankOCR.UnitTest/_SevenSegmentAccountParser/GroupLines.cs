using System;
using System.Collections.Generic;
using System.Linq;
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

            lines = new List<string>
            {
                "1",
                "2",
                "3",
                "                           ",
                "a",
                "b",
                "c"
            };

            var sevenSegmentAccounts = SevenSegmentAccountParser.GroupLines(lines.ToArray()).ToArray();


            sevenSegmentAccounts.Should().HaveCount(2);

            sevenSegmentAccounts[0].SevenSegmentAccountNumber[2].Should().Be("3");
            sevenSegmentAccounts[1].SevenSegmentAccountNumber[2].Should().Be("c");
        }
    }
}