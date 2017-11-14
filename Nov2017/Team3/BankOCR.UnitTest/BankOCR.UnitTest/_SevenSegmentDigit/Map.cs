using System;
using FluentAssertions;
using NUnit.Framework;

namespace BankOCR.UnitTest._SevenSegmentDigit
{
    [TestFixture]
    public class Map
    {                                  
        [TestCase("       I", '1')]
        public void ShouldMap(string inputString, char expectedChar)
        {
            var sut = new SevenSegmentDigit(inputString);

            sut.Map(inputString).Should().Be(expectedChar); 
        }
    }
}