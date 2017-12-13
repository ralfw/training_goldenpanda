using System;
using System.Globalization;
using System.IO;
using FluentAssertions;
using NUnit.Framework;

namespace ChurnReporter.UnitTest._Adapter
{
    [TestFixture]
    public class WriteProtocol
    {
        private string _testDirectory;
        private string _reportFilePath;

        [SetUp]
        public void SetUp()
        {
            _testDirectory = TestContext.CurrentContext.TestDirectory;
        }

        [Test]
        public void ShouldWriteCsvFile()
        {
            // Arrange
            var reportEntries = new[]
            {
                @"D:\dev\training_goldenpanda\Nov2017\Team3\BankOCR\BankOCR\BankOCR.cs;34;3",
                @"D:\dev\training_goldenpanda\Nov2017\Team3\BankOCR\BankOCR\Converter.cs;19;2"
            };
            _reportFilePath = Path.Combine(_testDirectory, "./testFiles/report.csv");

            // Act
            Adapter.WriteReport(reportEntries, _reportFilePath);

            // Assert
            var reportLines = File.ReadAllLines(_reportFilePath);
            reportLines[0].Should().Be(@"D:\dev\training_goldenpanda\Nov2017\Team3\BankOCR\BankOCR\BankOCR.cs;34;3");
            reportLines[1].Should().Be(@"D:\dev\training_goldenpanda\Nov2017\Team3\BankOCR\BankOCR\Converter.cs;19;2");              
        }
    }
}