using FluentAssertions;
using NUnit.Framework;

namespace BankOcr.UnitTest._SevenSegmentAccountParser
{
    [TestFixture]
    public class GroupLines
    {
        [Test]
        public void ShouldGroupLinesToSevenSegmentAccount()
        {
            var lines = new[]
            {
                "    _  _     _  _  _  _  _ ",
                "  | _| _||_||_ |_   ||_||_|",
                "  ||_  _|  | _||_|  ||_| _|",
                "",
                "    _  _  _  _  _  _     _ ",
                "|_||_|| || ||_   |  |  ||_ ",
                "  | _||_||_||_|  |  |  | _|",
                ""
            };

            var groupLines = SevenSegmentAccountParser.GroupLines(lines);

            groupLines.Length.Should().Be(2);

            groupLines[0].Line1.Should().Be("    _  _     _  _  _  _  _ ");
            groupLines[0].Line2.Should().Be("  | _| _||_||_ |_   ||_||_|");
            groupLines[0].Line3.Should().Be("  ||_  _|  | _||_|  ||_| _|");

            groupLines[1].Line1.Should().Be("    _  _  _  _  _  _     _ ");
            groupLines[1].Line2.Should().Be("|_||_|| || ||_   |  |  ||_ ");
            groupLines[1].Line3.Should().Be("  | _||_||_||_|  |  |  | _|");
        }
    }
}