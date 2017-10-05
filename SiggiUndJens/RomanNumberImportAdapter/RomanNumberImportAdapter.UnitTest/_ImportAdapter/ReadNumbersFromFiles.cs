using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var importAdapter = new ImportAdapter("ReadFileNamesInFolder");
            var filePaths = importAdapter.ReadFileNamesInFolder();

            var result = importAdapter.ReadNumbersFromFiles(filePaths);

            result.Length.Should().Be(4);
        }
    }
}
