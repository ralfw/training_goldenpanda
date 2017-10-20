using System;
using FluentAssertions;
using NUnit.Framework;

namespace TextWrap.UnitTest
{
    [TestFixture]
    public class BuildRows
    {
        [Test]
        public void ShouldBuildRowsFromWords()
        {
            int maxRowLength = 14;
            string[] words = {"One"};


            var rows = TextWrapper.BuildRows(words, maxRowLength);

            rows.Should().ContainInOrder("One")
                 .And.HaveCount(1);
        }
    }
}