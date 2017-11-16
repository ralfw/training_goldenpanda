using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace BankOCR.UnitTest._SevenSegmentAccount
{
    [TestFixture]
    public class GenerateSevenSegmentDigits
    {
        [TestCase(1,
            new[]{"   " ,
                  "  |" ,      
                  "  |"},       
            new[]{ "   " +
                   "  |" +
                   "  |"})]                           
        [TestCase(2,new[]{"    _ " ,
                        "  | _|" ,
                        "  ||_ "}, new[]{ "   " +
                                       "  |" +
                                       "  |", " _ " +
                                              " _|" +
                                              "|_ "
        })]
        public void ShouldGenerateSevenSegmentDigit(int dummy, string[] lines, string[] expectedDigits )
        {
            var sut = new SevenSegmentAccount(lines);
            var generatedDigits = sut.GenerateSevenSegmentDigits();

            generatedDigits.Should().HaveCount(expectedDigits.Length);
            var index = 0;
            foreach (var sevenSegmentDigit in generatedDigits)
            {
                sevenSegmentDigit.Segments.Should().Be(expectedDigits[index++]);
            }
        }
    }
}