using FluentAssertions;
using NUnit.Framework;

namespace BankOcr.UnitTest._Converter
{
    [TestFixture]
    public class Convert
    {
        [Test, Ignore("WIP")]
        public void ShouldConvertToAccountNumbers()
        {
            var lines = new[]
            {
                "    _  _     _  _  _  _  _ ",
                "  | _| _||_||_ |_   ||_||_|",
                "  ||_  _|  | _||_|  ||_| _|",
                "",
                "    _  _  _  _  _  _     _ ",
                "|_||_|| || ||_   |  |  ||_ ",
                "  | _||_||_||_|  |  |  | _|",
                ""
            };

            var result = Converter.Convert(lines);

            result.Should().ContainInOrder("123456789", "490067715");
        }
    }
}