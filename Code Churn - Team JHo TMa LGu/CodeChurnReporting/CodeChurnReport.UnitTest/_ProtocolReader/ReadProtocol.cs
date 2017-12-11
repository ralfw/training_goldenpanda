using System;
using System.IO;
using System.Linq;
using CodeChurnReport.Structs;
using FluentAssertions;
using NUnit.Framework;

namespace CodeChurnReport.UnitTest._ProtocolReader
{
    [TestFixture]
    public class ReadProtocol
    {
        private void CreateTestFile(string filePath)
        {
            if (File.Exists(filePath))
                File.Delete(filePath);
            var content = new[] {"2000-01-01;1;a", "2000-02-01;2;b"};
            File.WriteAllLines(filePath, content);
        }

        [Test]
        public void ShouldReadProtocolFile()
        {
            var startDate = new DateTime(2000, 1, 15);
            var endDate = new DateTime(2000, 3, 1);
            var filePath = $"{AppDomain.CurrentDomain.BaseDirectory}\\test.csv";
            var config = new Config {StartDate = startDate, EndDate = endDate, ProtocolFilePath = filePath};
            CreateTestFile(filePath);

            var protocol = ProtocolReader.ReadProtocol(config).ToArray();

            protocol.Length.Should().Be(1);
            protocol[0].UncFilePath.Should().Be("b");
            protocol[0].LineOfCode.Should().Be(2);
            protocol[0].TimeStamp.Should().Be(new DateTime(2000, 2, 1));
        }
    }
}