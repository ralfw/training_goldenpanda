using FluentAssertions;
using NUnit.Framework;

namespace BankOCR.UnitTest
{
    [TestFixture]
    public class Map
    {
        [TestCase("   " +
                  "  |" +
                  "  |", '1')]
        [TestCase(" _ " +
                  " _|" +
                  "|_ ", '2')]
        [TestCase(" _ " +
                  " _|" +
                  " _|", '3')]
        [TestCase("   " +
                  "|_|" +
                  "  |", '4')]
        
        public void ShouldMapSegmentsToNumber(string segments, char expected)
        {
            var sut = new SevenSegmentDigit(segments);

            var mappedSignatur = sut.Map();
            mappedSignatur.Should().Be(expected);
        }

    }
}