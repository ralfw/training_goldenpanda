using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using RomanNumberImportAdapter.UnitTest._ImportAdapter;

namespace RomanNumberImportAdapter.UnitTest
{
    [TestFixture]
    public class ReadFileNamesInFolder : TesterBase
    {
        [Test]
        public void ShouldReadFileNamesInFolder()
        {
            var importAdapter = new ImportAdapter("ReadFileNamesInFolder");
           
            var result = importAdapter.ReadFileNamesInFolder();

            result.Length.Should().Be(2);
            result.Should().Contain(new List<string> { "ReadFileNamesInFolder\\a.txt", "ReadFileNamesInFolder\\b.txt" });
        }
    }
}