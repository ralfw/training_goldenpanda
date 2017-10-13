using System;
using FluentAssertions;
using NUnit.Framework;

namespace fromRomanConverter.UnitTest
{
    [TestFixture]
    public class Split
    {
        [Test]
        public void ShouldSplitRomanNumberIntoCharacters()
        {
            const string romanNumber = "MCIX";

            var characters = FromRomanConverter.Split(romanNumber);

            characters[0].Should().Be('M');
            characters[1].Should().Be('C');
            characters[2].Should().Be('I');
            characters[3].Should().Be('X');
        }
    }
}