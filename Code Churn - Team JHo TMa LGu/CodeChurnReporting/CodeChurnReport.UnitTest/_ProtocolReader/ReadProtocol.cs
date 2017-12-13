using System;
using System.IO;
using System.Linq;
using CodeChurnReport.Behavior.Providers;
using CodeChurnReport.Data;
using FluentAssertions;
using NUnit.Framework;

namespace CodeChurnReport.UnitTest._ProtocolReader
{
    [TestFixture]
    public class ReadProtocol
    {
        private string _filePath;

        [SetUp]
        public void Setup()
        {
            _filePath = $"{AppDomain.CurrentDomain.BaseDirectory}\\test.csv";
            if (File.Exists(_filePath))
                File.Delete(_filePath);
            var content = new[] {"2000-01-01;1;a", "2000-02-01;2;b"};
            File.WriteAllLines(_filePath, content);
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);
        }

        [Test]
        public void ShouldReadProtocolFile()
        {
            var startDate = new DateTime(2000, 1, 15);
            var endDate = new DateTime(2000, 3, 1);
            var config = new Config {StartDate = startDate, EndDate = endDate, ProtocolFilePath = _filePath};

            var protocol = ProtocolReader.ReadProtocol(config.ProtocolFilePath).ToArray();

            protocol.Length.Should().Be(2);
            protocol[0].UncFilePath.Should().Be("a");
            protocol[0].LineOfCode.Should().Be(1);
            protocol[0].TimeStamp.Should().Be(new DateTime(2000, 1, 1));
            protocol[1].UncFilePath.Should().Be("b");
            protocol[1].LineOfCode.Should().Be(2);
            protocol[1].TimeStamp.Should().Be(new DateTime(2000, 2, 1));
        }
    }
}