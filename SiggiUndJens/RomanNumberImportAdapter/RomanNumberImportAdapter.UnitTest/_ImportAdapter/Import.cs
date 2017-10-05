using FluentAssertions;
using NUnit.Framework;

namespace RomanNumberImportAdapter.UnitTest._ImportAdapter
{
    [TestFixture]
    class Import : TesterBase
    {
        [Test]
        public void ShouldImportNumbersAndReturnFileCount()
        {
            var result = _importAdapter.Import();

            result.Item1.Length.Should().Be(4);
            result.Item2.Should().Be(2);
        }
    }
}