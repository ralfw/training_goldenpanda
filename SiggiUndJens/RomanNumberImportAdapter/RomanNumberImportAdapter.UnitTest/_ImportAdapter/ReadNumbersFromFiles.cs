using FluentAssertions;
using NUnit.Framework;

namespace RomanNumberImportAdapter.UnitTest._ImportAdapter
{
    [TestFixture]
    public class ReadNumbersFromFiles : TesterBase
    {
        [Test]
        public void ShouldReadAllNumbersFromFiles()
        {
            var filePaths = _importAdapter.ReadFileNamesInFolder();

            var result = _importAdapter.ReadNumbersFromFiles(filePaths);

            result.Length.Should().Be(4);
        }
    }
}