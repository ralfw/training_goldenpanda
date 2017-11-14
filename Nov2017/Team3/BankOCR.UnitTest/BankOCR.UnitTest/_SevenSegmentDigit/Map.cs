using System;
using FluentAssertions;
using NUnit.Framework;

namespace BankOCR.UnitTest._SevenSegmentDigit
{
    [TestFixture]
    public class Map
    {
        [Test]
        public void ShouldMap()
        {
            var sut = new SevenSegmentDigit();
            string input = "    I";

            sut.Map(input).Should().Be("1");

        }
    }
}