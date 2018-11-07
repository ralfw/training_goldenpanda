using System;
using System.IO;
using System.Net;
using FluentAssertions;
using NUnit.Framework;
using tvspike.contracts;

namespace tvspike.es.tests
{
    [TestFixture]
    public class FileEventStoreTests
    {
        [SetUp]
        public void SetUp()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
        }

        [Test]
        public void ShouldGetAllFileNamesFromTheStore()
        {
            // arrange
            var eventStoreRootFolder = Path.Combine(TestContext.CurrentContext.TestDirectory, "events");
            if (Directory.Exists(eventStoreRootFolder))
                Directory.Delete(eventStoreRootFolder, true);
            Directory.CreateDirectory(eventStoreRootFolder);

            const string fileName2 = "test.txt";
            const string fileName = "test2.txt";
            File.WriteAllText(Path.Combine(eventStoreRootFolder, fileName), "");
            File.WriteAllText(Path.Combine(eventStoreRootFolder, fileName2), "");
            var fileEventStore = new FileEventStore(eventStoreRootFolder);

            // act
            var allFileNames = fileEventStore.GetAllFileNames();

            // assert
            allFileNames.Length.Should().Be(2);
            allFileNames.Should().Contain(x => x.EndsWith(fileName));
            allFileNames.Should().Contain(x => x.EndsWith(fileName2));
        }
    }
}