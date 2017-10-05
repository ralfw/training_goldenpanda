using System.IO;
using FluentAssertions;
using NUnit.Framework;

namespace RomanNumberImportAdapter.UnitTest
{
    [TestFixture]
    public class Import
    {
        [SetUp]
        public void SetUp()
        {
            Directory.CreateDirectory("Import");

        }

        [Test]
        public void ShouldReadFileNamesInFolder()
        {
            var importAdapter = new ImportAdapter("Import");

            var result = importAdapter.ReadFileNamesInFolder();

            result.Length.Should().Be(4);
        }
    }
}