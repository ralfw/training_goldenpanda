using FluentAssertions;
using NUnit.Framework;

namespace fromRomanConverter.UnitTest
{
    [TestFixture]
    public class Convert
    {
        [Test]
        public void ShouldConvertRomanSymbolsToArabicNumbers()
        {
            char[] symbols = {'I', 'V', 'X', 'L', 'C', 'D', 'M'};

            var arabicNumbers = FromRomanConverter.Convert(symbols);

            arabicNumbers.Should().ContainInOrder(1, 5, 10, 50, 100, 500, 1000);
        }
    }
}