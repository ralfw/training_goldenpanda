using FluentAssertions;
using NUnit.Framework;

namespace BankOCR.UnitTest._SevenSegmentDigit
{
    [TestFixture]
    public class Map
    {                                  
        [TestCase("     I  I", '1')]
        public void ShouldMap(string inputString, char expectedChar)
        {
            var sut = new SevenSegmentDigit(inputString);

            var c = sut.Map(inputString);
            c.Should().Be(expectedChar); 
        }
    }
}