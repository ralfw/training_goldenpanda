using FluentAssertions;
using NUnit.Framework;

namespace BankOCR.UnitTest
{
    [TestFixture]
    public class Map
    {
        [Test]
        public void ShouldMapSegmentsToNumber()
        {
            var sut = new SevenSegmentDigit("   " +
                                            "  |" +
                                            "  |");

            var mappedSignatur = sut.Map();
            mappedSignatur.Should().Be('1');
        }

    }
}