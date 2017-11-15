using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace BankOcr.UnitTest._SevenSegmentDigit
{
    [TestFixture]
    public class Map
    {
        private const string ValueOf0 =
            " _ " +
            "| |" +
            "|_|";

        private const string ValueOf1 =
            "   " +
            "  |" +
            "  |";

        private const string ValueOf2 =
            " _ " +
            " _|" +
            "|_ ";
        private const string ValueOf3 =
            " _ " +
            " _|" +
            " _|";
        private const string ValueOf4 =
            "   " +
            "|_|" +
            "  |";

        private const string ValueOf5 =
            " _ " +
            "|_ " +
            " _|";

        private const string ValueOf6 =
            " _ " +
            "|_ " +
            "|_|";

        private const string ValueOf7 =
            " _ " +
            "  |" +
            "  |";

        private const string ValueOf8 =
            " _ " +
            "|_|" +
            "|_|";

        private const string ValueOf9 =
            " _ " +
            "|_|" +
            " _|";



        [TestCase(ValueOf0, 0)]
        [TestCase(ValueOf1, 1)]
        [TestCase(ValueOf2, 2)]
        [TestCase(ValueOf3, 3)]
        [TestCase(ValueOf4, 4)]
        [TestCase(ValueOf5, 5)]
        [TestCase(ValueOf6, 6)]
        [TestCase(ValueOf7, 7)]
        [TestCase(ValueOf8, 8)]
        [TestCase(ValueOf9, 9)]
        public void ShouldMapGivenNumber(string digitValue, int expectedNumber)
        {
            var sevenSegmentDigit = new SevenSegmentDigit(digitValue);

            var value = sevenSegmentDigit.Map();

            value.Should().Be(expectedNumber);
        }
    }
}