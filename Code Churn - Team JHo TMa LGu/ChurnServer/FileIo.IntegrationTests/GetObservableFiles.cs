using System.IO;
using System.Linq;
using NUnit.Framework;
using FluentAssertions;

namespace FileIo.IntegrationTests
{
    [TestFixture]
    public class GetObservableFiles : FileIoTestsBase
    {
        [Test]
        public void ShouldReturnTextFilesOnly()
        {
            var fileIo = new ChurnServer.Adapter.FileIo();
            var testDirectoryPath = GetPathToTestDirectory();
            var fileExtensions = new[] {"txt"};

            var observableFiles = fileIo.GetObservableFiles(testDirectoryPath,fileExtensions).ToList();

            observableFiles.Count.Should().Be(3);
            
            observableFiles.Should().Contain(Path.Combine(testDirectoryPath, "TextFile1.txt"));
            observableFiles.Should().Contain(Path.Combine(testDirectoryPath, "TextFile2.txt"));
            observableFiles.Should().Contain(Path.Combine(testDirectoryPath, @"SubDir1\TextFile3.txt"));
        }

        [Test]
        public void ShouldReturnCSharpCodeFilesOnly()
        {
            var fileIo = new ChurnServer.Adapter.FileIo();
            var testDirectoryPath = GetPathToTestDirectory();
            var fileExtensions = new[] {"cs"};

            var observableFiles = fileIo.GetObservableFiles(testDirectoryPath,fileExtensions).ToList();

            observableFiles.Count.Should().Be(3);
            observableFiles.Should().Contain(Path.Combine(Path.GetFullPath(testDirectoryPath), "CSharpCodeFile1.cs"));
            observableFiles.Should().Contain(Path.Combine(Path.GetFullPath(testDirectoryPath), "CSharpCodeFile2.cs"));
            observableFiles.Should().Contain(Path.Combine(Path.GetFullPath(testDirectoryPath), @"SubDir2\CSharpCodeFile3.cs"));
        }

        [Test]
        public void ShouldReturnAllTextAndCSharpCodeFiles()
        {
            var fileIo = new ChurnServer.Adapter.FileIo();
            var testDirectoryPath = GetPathToTestDirectory();
            var fileExtensions = new[] { "txt", "cs" };

            var observableFiles = fileIo.GetObservableFiles(testDirectoryPath, fileExtensions).ToList();

            observableFiles.Count.Should().Be(6);
            observableFiles.Should().Contain(Path.Combine(testDirectoryPath, "TextFile1.txt"));
            observableFiles.Should().Contain(Path.Combine(testDirectoryPath, "TextFile2.txt"));
            observableFiles.Should().Contain(Path.Combine(testDirectoryPath, @"SubDir1\TextFile3.txt"));
            observableFiles.Should().Contain(Path.Combine(Path.GetFullPath(testDirectoryPath), "CSharpCodeFile1.cs"));
            observableFiles.Should().Contain(Path.Combine(Path.GetFullPath(testDirectoryPath), "CSharpCodeFile2.cs"));
            observableFiles.Should().Contain(Path.Combine(Path.GetFullPath(testDirectoryPath), @"SubDir2\CSharpCodeFile3.cs"));
        }

    }
}