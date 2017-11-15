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
        [TestCase(2,new[]{
                        "111aaa" ,
                        "222bbb" ,
                        "333ccc"},
            new[]{ "111" +
                   "222" +
                   "333",
                   "aaa" +
                   "bbb" +
                   "ccc"
        })]
        public void ShouldGenerateSevenSegmentDigit(int dummy, string[] lines, string[] expectedDigits )
        {
            var sut = new SevenSegmentAccount(lines);
            var generatedDigits = sut.Digits;

            generatedDigits.Should().HaveCount(expectedDigits.Length);
            var index = 0;
            foreach (var sevenSegmentDigit in generatedDigits)
            {
                sevenSegmentDigit.Segments.Should().Be(expectedDigits[index++]);
            }
        }
    }
}