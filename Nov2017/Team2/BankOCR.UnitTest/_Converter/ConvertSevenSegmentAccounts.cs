using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace BankOCR.UnitTest._Converter
{
    [TestFixture]
    public class ConvertSevenSegmentAccounts
    {
        [Test]
        public void ShouldConvertSevenSegmentAccounts()
        {
            var sevenSegmentAccount1 = new SevenSegmentAccount(
                new[] { "    _  _     _  _  _  _  _ ",
                        "  | _| _||_||_ |_   ||_||_|",
                        "  ||_  _|  | _||_|  ||_| _|"});
            var sevenSegmentAccount2= new SevenSegmentAccount(
                new[] {"    _  _  _  _  _  _     _ ",
                       "|_||_|| || ||_   |  |  ||_ ",
                       "  | _||_||_||_|  |  |  | _|"});
            var accounts = new List<SevenSegmentAccount>();
            accounts.Add(sevenSegmentAccount1);
            accounts.Add(sevenSegmentAccount2);
            
            var accountNos = Converter.ConvertSevenSegmentAccounts(accounts.ToArray());

            accountNos.Length.Should().Be(2);
            accountNos[0].Should().Be("123456789");
            accountNos[1].Should().Be("490067715");
        }
    }
}