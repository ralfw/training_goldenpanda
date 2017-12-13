using System;
using ChurnServer.AdapterInterfaces;
using ChurnServer.Infrastructure;
using NUnit.Framework;
using FluentAssertions;
using Moq;

namespace ChurnServer.UnitTests
{
    [TestFixture]
    public class ProtocolLineTests
    {
        [SetUp]
        public void SetUp()
        {
            AdapterProvider.FileIo = new Mock<IFileIo>().Object;
        }

        [Test]
        public void ShouldBuildProtocolLine()
        {
            var startTime = DateTime.Parse("10.12.2017 15:00:00");
            var linesOfCode = 33;
            var filePath = @"c:\pathToDir\pathToFile.txt";
            var fileIoMock = Mock.Get(AdapterProvider.FileIo);
            fileIoMock.Setup(m => m.GetFileContent(filePath)).Returns(new string[linesOfCode]);
            fileIoMock.Setup(m => m.ToUncPath(filePath)).Returns(filePath);

            var line = ProtocolLine.BuildProtocolLine(startTime, filePath);

            line.Should().Be("2017-12-10T15:00:00;33;c:\\pathToDir\\pathToFile.txt");
        }
    }
}