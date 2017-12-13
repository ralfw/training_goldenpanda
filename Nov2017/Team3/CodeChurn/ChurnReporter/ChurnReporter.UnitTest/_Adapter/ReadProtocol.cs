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
            // Arrange
            _protocolFilePath = Path.Combine(_testDirectory, @"./testFiles/protocol.csv");

            // Act
            var lines = Adapter.ReadProtocol(_protocolFilePath);

            // Assert
            var firstLine = lines[0].Split(new[] {';'});
            firstLine[0].Should().Be(@"03.12.2017 17:58:09");
            firstLine[1].Should().Be("27");
            firstLine[2].Should().Be(@"D:\dev\training_goldenpanda\Nov2017\Team3\CodeChurn\ChurnReporter\ChurnReporter\Adapter.cs");
        }
    }
}