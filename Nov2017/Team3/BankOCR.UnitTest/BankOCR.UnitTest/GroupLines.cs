using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace BankOCR.UnitTest
{
    [TestFixture]
    public class GroupLines
    {
        [Test]
        public void ShouldCreateAccountNumber()
        {
            List<string> lines = new List<string>();
            lines.Add(" _ _     _ _  _ _  _ _");
            lines.Add("I _I _II_II_ I_   II_II_II I");
            lines.Add("I I_  _I I _II_I II_I _II_I");
            lines.Add("                           ");

            var sut = new SevenSegmentParser();
            var sevenSegmentNumbers = sut.GroupLines(lines);

            sevenSegmentNumbers.Should().HaveCount(1);
            var sevenSegmentAccountNumber = sevenSegmentNumbers[0];

            sevenSegmentAccountNumber.Line1.Should().Be(lines[0]);
            sevenSegmentAccountNumber.Line2.Should().Be(lines[1]);
            sevenSegmentAccountNumber.Line3.Should().Be(lines[2]);

        }
        [Test]
        public void ShouldCreateMultipleAccountNumbers()
        {
            List<string> lines = new List<string>();
            lines.Add(" _ _     _ _  _ _  _ _");
            lines.Add("I _I _II_II_ I_   II_II_II I");
            lines.Add("I I_  _I I _II_I II_I _II_I");
            lines.Add("                           ");
            lines.Add(" _ _     _ _  _ _  _ _");
            lines.Add("I _I _II_II_ I_   II_II_II I");
            lines.Add("I I_  _I I _II_I II_I _II_I");
            lines.Add("                           ");

            var sut = new SevenSegmentParser();
            var sevenSegmentNumbers = sut.GroupLines(lines);

            sevenSegmentNumbers.Should().HaveCount(2);
            var sevenSegmentAccountNumber = sevenSegmentNumbers[0];

            sevenSegmentAccountNumber.Line1.Should().Be(lines[0]);
            sevenSegmentAccountNumber.Line2.Should().Be(lines[1]);
            sevenSegmentAccountNumber.Line3.Should().Be(lines[2]);

            var sevenSegmentAccountNumber2 = sevenSegmentNumbers[1];

            sevenSegmentAccountNumber2.Line1.Should().Be(lines[4]);
            sevenSegmentAccountNumber2.Line2.Should().Be(lines[5]);
            sevenSegmentAccountNumber2.Line3.Should().Be(lines[6]);
        }
    }
}