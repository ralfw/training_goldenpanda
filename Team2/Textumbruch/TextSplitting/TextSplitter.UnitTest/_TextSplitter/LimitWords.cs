using System;
using FluentAssertions;
using NUnit.Framework;

namespace TextSplitter.UnitTest._TextSplitter
{
    [TestFixture]
    public class LimitWords
    {
        [Test]
        public void ShouldLimitWords()
        {
            var sut = new TextSplitter();
            var input = new[] {"die", "Nacht"};
            var expected = new[] { "die", "Nac","ht" };
            const int linelength = 3;

            var result = sut.LimitWords(input, linelength);

            result.Should().ContainInOrder(expected);
        }
    }
}