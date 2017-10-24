using System;
using FluentAssertions;
using NUnit.Framework;

namespace TextWrap.UnitTest
{
    [TestFixture]
    public class BuildText
    {
        [Test]
        public void ShouldBuildTextFromOneRow()
        {
            string[] rows = { "One" };

            var text = TextWrapper.BuildText(rows);

            text.Should().Be("One");
        }

        [Test]
        public void ShouldBuildTextFromMultipleRows()
        {
            string[] rows = { "One","Two","Three" };

            var text = TextWrapper.BuildText(rows);

            text.Should().Be($"One{Environment.NewLine}Two{Environment.NewLine}Three");
        }
    }
}