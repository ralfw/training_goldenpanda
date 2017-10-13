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
            char[] symbols = {'I', 'V', 'M'};

            var result = FromRomanConverter.Convert(symbols);

            result.Should().ContainInOrder(1, 5, 1000);

        }

    }
}