using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace BankOcr.UnitTest._Converter
{
    [TestFixture]
    public class ConvertSevenSegmentAccounts
    {
        [Test]
        public void ShouldConvertSevenSegmentAccounts()
        {
            var lines = new[]
            {
                "    _  _     _  _  _  _  _ ",
                "  | _| _||_||_ |_   ||_||_|",
                "  ||_  _|  | _||_|  ||_| _|",
            };

            var account = new SevenSegmentAccount(lines);
            var accounts = BankOcr.Converter.ConvertSevenSegmentAccounts(new []{ account});

            accounts.Single().Should().Be("123456789");
        }
    }
}