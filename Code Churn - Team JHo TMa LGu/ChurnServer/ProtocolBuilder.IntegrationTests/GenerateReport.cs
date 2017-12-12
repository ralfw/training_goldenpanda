using System;
using System.IO;
using ChurnServer.Adapter;
using ChurnServer.Infrastructure;
using FluentAssertions;
using NUnit.Framework;

namespace ProtocolBuilder.IntegrationTests
{
    [TestFixture]
    public class GenerateReport
    {
        [SetUp]
        public void SetUp()
        {
            AdapterProvider.TimeProvider = new TimeProvider();
            AdapterProvider.FileIo = new FileIo();
            AdapterProvider.Ui = new Ui();
        }

        [Test, Category("Manual")]
        public void ShouldGenerateNewBuildProtocol()
        {
            var testRootPath = TestContext.CurrentContext.TestDirectory.Replace(@"\bin\Debug", "");
            var protocolFilePath = Path.Combine(testRootPath, @"DirectoryToObserve\churnlog.csv");
            if(File.Exists(protocolFilePath))
                File.Delete(protocolFilePath);


            var observableDir = Path.Combine(testRootPath, "DirectoryToObserve");
            string[] fileExtensions = { "txt", "cs" };

            ChurnServer.ProtocolBuilder.GenerateReport(observableDir, protocolFilePath, fileExtensions);

            File.Exists(protocolFilePath).Should().BeTrue();
            var fileContent = File.ReadAllLines(protocolFilePath);

            fileContent.Length.Should().Be(6);
        }

        [Test, Category("Manual")]
        public void ShouldGenerateAdditionalLinesInExsitingProtocol()
        {
            var testRootPath = TestContext.CurrentContext.TestDirectory.Replace(@"\bin\Debug", "");
            var protocolFilePath = Path.Combine(testRootPath, @"DirectoryToObserve\churnlog.csv");
            if (File.Exists(protocolFilePath))
                File.Delete(protocolFilePath);
            
            // create fake protocol
            File.WriteAllLines(protocolFilePath, new []{"Timestamp;LinesOfCode;FilePath"});

            var observableDir = Path.Combine(testRootPath, "DirectoryToObserve");
            string[] fileExtensions = { "txt", "cs" };

            ChurnServer.ProtocolBuilder.GenerateReport(observableDir, protocolFilePath, fileExtensions);

            File.Exists(protocolFilePath).Should().BeTrue();
            var fileContent = File.ReadAllLines(protocolFilePath);

            fileContent.Length.Should().Be(7);
        }
    }
}