using System.Collections.Generic;
using ChurnServer.AdapterInterfaces;
using ChurnServer.Infrastructure;
using NUnit.Framework;
using FluentAssertions;
using Moq;

namespace ChurnServer.UnitTests
{
    [TestFixture]
    public class ProtocolBuilderTests
    {
        [SetUp]
        public void SetUp()
        {
            AdapterProvider.FileIo = new Mock<IFileIo>().Object;
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(100)]
        public void ShouldGetLinesOfCode(int expectedLinesOfCode)
        {
            var fileContent = CreateFileContentArrayFromLinesOfCode(expectedLinesOfCode);
            Mock.Get(AdapterProvider.FileIo).Setup(m => m.GetFileContent("")).Returns(fileContent);

            var linesOfCode = ProtocolBuilder.GetLinesOfCode("");

            linesOfCode.Should().Be(expectedLinesOfCode);
        }

        private string[] CreateFileContentArrayFromLinesOfCode(int expectedLinesOfCode)
        {
            return new string[expectedLinesOfCode];
        }
    }
}