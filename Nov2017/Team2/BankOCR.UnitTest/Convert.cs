using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using NUnit.Framework;

namespace BankOCR.UnitTest
{
    [TestFixture]
    public class Convert
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
            var SevenSegmentAccounts = Converter.GroupLines(lines);

            SevenSegmentAccounts.Should().HaveCount(2);
        }

        [Test]
        public void ShouldConvertLines()
        {
            var accountNos = Converter.Convert(lines.ToArray());

            accountNos.Should().HaveCount(2);
        }
    }
}
