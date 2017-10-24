using System;
using FluentAssertions;
using NUnit.Framework;

namespace TextWrap.UnitTest
{
    [TestFixture]
    public class BuildRows
    {
        [Test]
        public void ShouldBuildOneRowForOneFittingWord()
        {
            int maxRowLength = 3;
            string[] words = {"One"};

            var rows = TextWrapper.BuildRows(words, maxRowLength);

            rows.Should().ContainInOrder("One")
                 .And.HaveCount(1);
        }

        [Test]
        public void ShouldBuildOneRowForTwoFittingWords()
        {
            int maxRowLength = 7;
            string[] words = { "One", "Two" };

            var rows = TextWrapper.BuildRows(words, maxRowLength);

            rows.Should().ContainInOrder("One Two")
                .And.HaveCount(1);
        }

        [Test]
        public void ShouldBuildTwoRowsWithNonFittingWords()
        {
            int maxRowLength = 7;
            string[] words = { "One", "Two", "Three" };

            var rows = TextWrapper.BuildRows(words, maxRowLength);

            rows.Should().ContainInOrder("One Two", "Three")
                .And.HaveCount(2);
        }
    }
}