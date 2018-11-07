using System;
using System.IO;
using System.Linq;
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

        [Test]
        public void ShouldCreateEventFileInfosFromFileNames()
        {
            // arrange
            var eventStoreRootFolder = Path.Combine(TestContext.CurrentContext.TestDirectory, "events");
            if (Directory.Exists(eventStoreRootFolder))
                Directory.Delete(eventStoreRootFolder, true);
            Directory.CreateDirectory(eventStoreRootFolder);

            const string fileName1 = "00000000000000000500_572e2387-00f9-4f8c-af7a-952f1a06b8d2_a2a45ecd-3060-415d-ab5c-ff1f33b8c9a4_EventA.txt";
            const string fileContent1 = "";
            const string fileName2 = "00000000000000000501_572e2387-00f9-4f8c-af7a-952f1a06b8d2_2a990294-8f3c-467d-ae0b-0b84685a4c4a_EventA.txt";
            const string fileContent2 = "";

            File.WriteAllLines(Path.Combine(eventStoreRootFolder, fileName1), new[] {fileName1, fileContent1});
            File.WriteAllLines(Path.Combine(eventStoreRootFolder, fileName2), new[] {fileName2, fileContent2});
            var fileEventStore = new FileEventStore(eventStoreRootFolder);

            var fileNames = new []
            {
                Path.Combine(eventStoreRootFolder, fileName1),
                Path.Combine(eventStoreRootFolder, fileName2),
            };

            var eventFileInfos = fileEventStore.CreateEventFileInfos(fileNames);

            eventFileInfos.Length.Should().Be(2);
            // TODO: get specific data /TMa
//            eventFileInfos.Any(x => x.EventNumber == "500").Should().BeTrue();
//            var file1Info = eventFileInfos.Single(x => x.EventNumber == "500");
//            file1Info.EventId.Should().Be("a2a45ecd-3060-415d-ab5c-ff1f33b8c9a4");
//            file1Info.EventName.Should().Be("EventA");
//            file1Info.EventData.Should().Be(fileContent1);


        }

    }
}