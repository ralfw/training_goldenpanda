using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace BankOCR.UnitTest._Converter
{
    [TestFixture]
    internal class Convert
    {
        [Test]
        public void ShouldConvertAccount()
        {
            SevenSegmentAccountNumber number1 = new SevenSegmentAccountNumber();
            SevenSegmentAccountNumber number2 = new SevenSegmentAccountNumber();

            number1.Line1 = "    _  _     _ ";
            number1.Line2 = "  I _I _II_II_ ";
            number1.Line3 = "  II_  _I  I _I";

            number2.Line1 = "    _  _ ";
            number2.Line2 = "  I _I _I";
            number2.Line3 = "  II_  _I";

            var result = Converter.Convert(new[] {number1, number2});

            result.Count().Should().Be(2);

            result[0].Should().Be("12345");
            result[1].Should().Be("123");
        }
    }
}