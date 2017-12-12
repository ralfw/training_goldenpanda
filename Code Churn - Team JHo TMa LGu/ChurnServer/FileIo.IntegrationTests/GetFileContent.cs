using System.IO;
using NUnit.Framework;
using FluentAssertions;

namespace FileIo.IntegrationTests
{
    [TestFixture]
    public class GetFileContent : FileIoTestsBase
    {
        [Test]
        public void ShouldGetFileContentFromTextFile1()
        {
            var fileIo = new ChurnServer.Adapter.FileIo();
            var testDirectoryPath = GetPathToTestDirectory();

            var fileContent = fileIo.GetFileContent(Path.Combine(testDirectoryPath, "TextFile1.txt"));

            fileContent.Length.Should().Be(2);
            fileContent[0].Should().Be("//Filename:TextFile1.txt");
            fileContent[1].Should().Be("//comment");
        }

        [Test]
        public void ShouldGetEmptyFileContentFromTextFile2()
        {
            var fileIo = new ChurnServer.Adapter.FileIo();
            var testDirectoryPath = GetPathToTestDirectory();

            var fileContent = fileIo.GetFileContent(Path.Combine(testDirectoryPath, "TextFile2.txt"));

            fileContent.Length.Should().Be(0);
        }

        [Test]
        public void ShouldGetFileContentFromCSharpFile1()
        {
            var fileIo = new ChurnServer.Adapter.FileIo();
            var testDirectoryPath = GetPathToTestDirectory();

            var fileContent = fileIo.GetFileContent(Path.Combine(testDirectoryPath, "CSharpCodeFile1.cs"));

            fileContent.Length.Should().Be(3);
            fileContent[0].Should().Be("//Filename:CSharpCodeFile1.cs");
            fileContent[1].Should().Be("//comment");
            fileContent[2].Should().Be("//comment");
        }
    }
}