using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace fromRomanConverter.UnitTest
{
    [TestFixture]
    public class Sign
    {
        [TestCaseSource(nameof(GetTestCaseData))]
        public void ShouldReturnMultipleSignedNumbers(int[] input, int[] expected)
        {
            var signedNumbers = FromRomanConverter.Sign(input);
            signedNumbers.Should().ContainInOrder(expected);
        }

        public static IEnumerable<TestCaseData> GetTestCaseData()
        {
            var testCaseData = new TestCaseData(new[] { 5, 1 }, new[] { 5, 1 });
            testCaseData.SetName("ShouldNotSignNumbers");
            yield return testCaseData;

            var testCaseData1 = new TestCaseData(new[] { 1, 5 }, new[] { -1, 5 });
            testCaseData1.SetName("ShouldSignFirstNumber");
            yield return testCaseData1;

            var testCaseData2 = new TestCaseData(new[] { 1, 5, 10 }, new[] { -1, -5, 10 });
            testCaseData2.SetName("ShouldSignFirstTwoNumbers");
            yield return testCaseData2;
        }
    }
}