using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace BankOCR.UnitTest._Converter
{
    [TestFixture]
    public class ConvertSevenSegmentDigitsToAccountNo
    {
        [TestCase(1,new[]{"   " +
                        "  |" +
                        "  |"},"1")]
        [TestCase(2,new[]{"   " +
                        "  |" +
                        "  |",
                        "   " +
                        "  |" +
                        "  |",
        }, "11")]
        public void ShouldConvertSevenSegmentDigitsToAccountNo(int dummyInt,string[] digitStrings, string expectedAccountNo)
        {
            var digits = new List<SevenSegmentDigit>();
            foreach (var digitString in digitStrings)
            {
                digits.Add(new SevenSegmentDigit(digitString));
            }
            var accountNo = Converter.ConvertSevenSegmentDigitsToAccountNo(digits);

            accountNo.Should().Be(expectedAccountNo);
        }
    }
}