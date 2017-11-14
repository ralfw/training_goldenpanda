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

        }
    }
}