using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace BankOCR.UnitTest._SevenSegmentAccountParser
{
    [TestFixture]
    public class GroupLines
    {
        protected List<string> lines;
        [SetUp]
        public void SetUp()
        {
            lines = new List<string>
            {
                "    _  _     _  _  _  _  _ ",
                "  | _| _||_||_ |_   ||_||_|",
                "  ||_  _|  | _||_|  ||_| _|",
                "                           ",
                "    _  _  _  _  _  _     _ ",
                "|_||_|| || ||_   |  |  ||_ ",
                "  | _||_||_||_|  |  |  | _|"
            };
        }

        [Test]
        public void ShouldGroupLines()
        {
            var SevenSegmentAccounts = SevenSegmentAccountParser.GroupLines(lines);

            SevenSegmentAccounts.Should().HaveCount(2);
        }

    }
}