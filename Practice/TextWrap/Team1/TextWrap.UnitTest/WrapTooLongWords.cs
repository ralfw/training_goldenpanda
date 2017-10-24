using System;
using FluentAssertions;
using NUnit.Framework;

namespace TextWrap.UnitTest
{
    [TestFixture]
    public class WrapTooLongWords
    {
        [Test]
        public void ShouldWrapOneTooLongWordIntoTwoWords()
        {
            int maxRowLength = 7;
            string[] words = { "CCD-School" };

            var wrappedWords = TextWrapper.WrapTooLongWords(words, maxRowLength);

            wrappedWords.Should().ContainInOrder("CCD-Sch", "ool")
                .And.HaveCount(2);
        }

        [Test]
        public void ShouldWrapOneTooLongWordIntoMultipleWords()
        {
            int maxRowLength = 2;
            string[] words = { "CCD-School" };

            var wrappedWords = TextWrapper.WrapTooLongWords(words, maxRowLength);

            wrappedWords.Should().ContainInOrder("CC","D-","Sc","ho","ol")
                        .And.HaveCount(5);
        }

        [Test]
        public void ShouldNotWrapFittingWord()
        {
            int maxRowLength = 7;
            string[] words = { "Short" };

            var wrappedWords = TextWrapper.WrapTooLongWords(words, maxRowLength);

            wrappedWords.Should().ContainInOrder("Short")
                        .And.HaveCount(1);
        }
    }
}