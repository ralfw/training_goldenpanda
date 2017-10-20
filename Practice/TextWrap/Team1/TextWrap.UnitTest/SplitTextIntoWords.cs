using System;
using FluentAssertions;
using NUnit.Framework;

namespace TextWrap.UnitTest
{
    [TestFixture]
    public class SplitTextIntoWords
    {
        [Test]
        public void ShouldSplitTextIntoWords()
        {
            var row = "One Two Three";

            var words = TextWrapper.SplitTextIntoWords(row);

            words.Should().ContainInOrder("One", "Two", "Three")
                .And.HaveCount(3);
        }

        [Test]
        public void ShouldSplitMultilineTextIntoWords()
        {
            var row = $"One Two {Environment.NewLine}Three Four";

            var words = TextWrapper.SplitTextIntoWords(row);

            words.Should().ContainInOrder("One", "Two", "Three", "Four").And.HaveCount(4);
        }

        [Test]
        public void ShouldPreservePunctuationMarksAsPartOfWord()
        {
            var row = "One Two, Three.";

            var words = TextWrapper.SplitTextIntoWords(row);

            words.Should().ContainInOrder("One", "Two,", "Three.")
                 .And.HaveCount(3);
        }
    }
}