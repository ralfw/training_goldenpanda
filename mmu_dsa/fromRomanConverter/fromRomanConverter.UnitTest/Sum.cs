using System;
using FluentAssertions;
using NUnit.Framework;

namespace fromRomanConverter.UnitTest
{
    [TestFixture]
    public class Sum
    {
        [Test]
        public void ShouldSum()
        {
            int[] testData = new[] {1, 5, 10};

            var result = FromRomanConverter.Sum(testData);

            result.Should().Be(16);
        }
    }
}