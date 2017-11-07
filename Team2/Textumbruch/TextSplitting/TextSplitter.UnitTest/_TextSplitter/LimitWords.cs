using System;
using FluentAssertions;
using NUnit.Framework;

namespace TextSplitter.UnitTest._TextSplitter
{
    [TestFixture]
    public class LimitWords
    {
        [TestCase("die", 3, new[]{"die"})]
        [TestCase("Nacht", 3, new[]{"Nac", "ht"})]
        public void ShouldLimitWord(string word, int limit, string[] expected) {
            var result = TextSplitter.Split_long_word(word, limit);
            result.Should().ContainInOrder(expected);
        }
    }
}