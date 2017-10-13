using System;
using FluentAssertions;
using NUnit.Framework;

namespace fromRomanConverter.UnitTest
{
    [TestFixture]
    public class Sign
    {
        [TestCase(new int[] {1}, new int[] {1})]
        [TestCase(new int[] {2}, new int[] {2})]
        public void ShouldReturnSingleSignedNumber(int[] input, int[] expected)
        {
            var signedNumbers = FromRomanConverter.Sign(input);
            signedNumbers.Should().ContainInOrder(expected);
        }

        [TestCase(new int[] { 1,5 }, new int[] { (-1),5})]
       // [TestCase(new int[] { 1,5,2 }, new int[] { (-1),5, 2})]
       // [TestCase(new int[] { 1,5,10 }, new int[] { (-1),(-5), 10})]
       // [TestCase(new int[] { 5, 1 }, new int[] { 5, 1 })]
        public void ShouldReturnMultipleSignedNumbers(int[] input, int[] expected)
        {
            var signedNumbers = FromRomanConverter.Sign(input);
            signedNumbers[0].Should().Be(expected[0]);
            signedNumbers[1].Should().Be(expected[1]);
        }





    }
}