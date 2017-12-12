using System.IO;
using FluentAssertions;
using NUnit.Framework;

namespace FileIo.IntegrationTests
{
    [TestFixture]
    public class StoreProtocol : FileIoTestsBase
    {
        [SetUp]
        public void SetUp()
        {
            var rootRwTestDir = GetPathToTestDirectoryForReadWrite();
            if (Directory.Exists(rootRwTestDir))
                Directory.Delete(rootRwTestDir, true);
            Directory.CreateDirectory(rootRwTestDir);
        }

        private void CreateTestFileWithContent(string filePath, string[] fileContent)
        {
            File.WriteAllLines(filePath, fileContent);
        }

        [Test]
        public void ShouldStoreIfFileDoesNotExists()
        {
            var fileIo = new ChurnServer.Adapter.FileIo();

            var pathToNewFile = BuildTestFilePath("newFile.txt");
            string[] protocolLines = {"Line1", "Line2"};

            fileIo.StoreFileContent(pathToNewFile, protocolLines);

            File.Exists(pathToNewFile).Should().BeTrue();

            var fileContent = File.ReadAllLines(pathToNewFile);
            fileContent.Length.Should().Be(2);
            fileContent[0].Should().Be(protocolLines[0]);
            fileContent[1].Should().Be(protocolLines[1]);
        }

        [Test]
        public void ShouldStoreByAppendingToExistingFile()
        {
            var fileIo = new ChurnServer.Adapter.FileIo();

            var pathToExistingFile = BuildTestFilePath("existingFile.txt");
            var existingContent = new[] { "ProtocolLine1", "ProtocolLine2" };
            CreateTestFileWithContent(pathToExistingFile, existingContent);
            
            string[] contentToAppend = {"NewLine1","NewLine2"};
            fileIo.StoreFileContent(pathToExistingFile, contentToAppend);

            File.Exists(pathToExistingFile).Should().BeTrue();

            var fileContent = File.ReadAllLines(pathToExistingFile);
            fileContent.Length.Should().Be(4);
            fileContent[0].Should().Be(existingContent[0]);
            fileContent[1].Should().Be(existingContent[1]);
            fileContent[2].Should().Be(contentToAppend[0]);
            fileContent[3].Should().Be(contentToAppend[1]);
        }


        private string BuildTestFilePath(string filename)
        {
            return Path.Combine(GetPathToTestDirectoryForReadWrite(), filename);
        }
    }
}