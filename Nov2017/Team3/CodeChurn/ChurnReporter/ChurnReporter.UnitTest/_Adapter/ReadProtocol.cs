using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace ChurnReporter.UnitTest._Adapter
{
    [TestFixture]
    public class ReadProtocol
    {
        private string _protocolFilePath;
        private string _testDirectory;

        [SetUp]
        public void SetUp()
        {
            _testDirectory = TestContext.CurrentContext.TestDirectory;
        }

        [Test]
        public void ShouldReadCsvFile()
        {
            var sut = new Adapter();
            _protocolFilePath = Path.Combine(_testDirectory, @"./testFiles/protocol.csv");

            var lines = Adapter.ReadProtocol(_protocolFilePath);

            var firstLine = lines[0].Split(new[] {';'});
            firstLine[0].Should().Be(@"D:\dev\training_goldenpanda\Nov2017\Team3\BankOCR\BankOCR\BankOCR.cs");
            firstLine[1].Should().Be("131567725374506232");
            firstLine[2].Should().Be("21");
        }
    }
}