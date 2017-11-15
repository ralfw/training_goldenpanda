using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace BankOCR.UnitTest._Converter
{
    [TestFixture]
    public class Convert
    {
        [Test]
        public void ShouldConvertLines()
        {
           var lines = new List<string>
            {
                "    _  _     _  _  _  _  _ ",
                "  | _| _||_||_ |_   ||_||_|",
                "  ||_  _|  | _||_|  ||_| _|",
                "                           ",
                "    _  _  _  _  _  _     _ ",
                "|_||_|| || ||_   |  |  ||_ ",
                "  | _||_||_||_|  |  |  | _|"
            };
            var accountNos = Converter.Convert(lines.ToArray());

            accountNos.Should().HaveCount(2);
            accountNos[0].Should().Be("123456789");
            accountNos[1].Should().Be("490067715");
        }
    }
}
