using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace ChurnServer.Test
{
    [TestFixture]
    public class ChurnServerTest
    {
        [Test]
        public void ShoudAnalzyeFiles()
        {
            string rootDir = " ";

            //string[] allfiles = System.IO.Directory.GetFiles(rootDir, "*.cs", System.IO.SearchOption.AllDirectories);
            string[] allfiles = {"TestData//Converter.cs ", ".//TestData//FileIO.cs "};

            var result = ChurnServer.AnalyzeFiles(allfiles);

            result.Should().NotBeNull();
            result.Length.Should().Be(allfiles.Length);

            result[0].LoC.Should().Be(27);
            result[1].LoC.Should().Be(12);
        }

        [Test, Ignore]
        public void ShouldEvaluateDirectory()
        {
            string rootDir = "TestData";
            string protocolFilePath = "ChurnProtocol.csv";
            ChurnServer.Quellcodestand_protokollieren(rootDir, protocolFilePath);
        }

        [Test]
        public void ShouldPrepareOutput()
        {
            List<ProtocolEntry> infos = new List<ProtocolEntry>();

            infos.Add(new ProtocolEntry {FilePath = "Path", LoC = 1, TimeStamp = DateTime.Now});
            infos.Add(new ProtocolEntry {FilePath = "Path1", LoC = 2, TimeStamp = DateTime.Now});

            string result = $"Lines written: {infos.ToArray().Length}";

            result.Should().Be("Lines written: 2");
        }
    }
}