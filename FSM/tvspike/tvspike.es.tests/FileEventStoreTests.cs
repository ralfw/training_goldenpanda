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
        public void ShouldCreateWorkingDirectoryIfNotExists()
        {
            var eventStoreRootFolder = EventStoreTestHelper.EnsureEmptyRootFolder("storeRoot_1");

            // ReSharper disable once ObjectCreationAsStatement
            new FileEventStore(eventStoreRootFolder, "1234");

            Directory.Exists(Path.Combine(eventStoreRootFolder, "events")).Should().BeTrue();
        }

        // Question: "This test tests an .net API call and should be removed /TMa"
        // HINT: this test also tests if events can be read from an former created directory
        [Test]
        public void ShouldGetAllFileNamesFromExistingStoreFolder()
        {
            // arrange
            var eventStoreRootFolder = EventStoreTestHelper.EnsureEmptyRootFolder("storeRoot_2");
            var eventsFolder = Path.Combine(eventStoreRootFolder, "events");
            Directory.CreateDirectory(eventsFolder);
            const string fileName2 = "test.txt";
            const string fileName = "test2.txt";
            EventStoreTestHelper.CreateTestFile(eventsFolder, fileName, "");
            EventStoreTestHelper.CreateTestFile(eventsFolder, fileName2, "");
            var fileEventStore = new FileEventStore(eventStoreRootFolder, "");

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
            var eventStoreRootFolder = EventStoreTestHelper.EnsureEmptyRootFolder("storeRoot_3");
            var eventsFolder = Path.Combine(eventStoreRootFolder, "events");
            Directory.CreateDirectory(eventsFolder);
            const string clientId = "572e2387-00f9-4f8c-af7a-952f1a06b8d2";
            const string fileName1 = "00000000000000000500_" + clientId + "_a2a45ecd-3060-415d-ab5c-ff1f33b8c9a4_EventA.txt";
            const string fileContent1 = "Content 500";
            const string fileName2 = "00000000000000000501_" + clientId + "_2a990294-8f3c-467d-ae0b-0b84685a4c4a_EventA.txt";
            const string fileContent2 = "Content 501";
            EventStoreTestHelper.CreateTestFile(eventsFolder, fileName1, new[] { fileName1, fileContent1 });
            EventStoreTestHelper.CreateTestFile(eventsFolder, fileName2, new[] { fileName2, fileContent2 });
            var fileEventStore = new FileEventStore(eventStoreRootFolder, clientId);

            var fileNames = new []
            {
                Path.Combine(eventsFolder, fileName1),
                Path.Combine(eventsFolder, fileName2),
            };

            var eventFileInfos = fileEventStore.CreateEventFileInfos(fileNames);

            eventFileInfos.Length.Should().Be(2);

            eventFileInfos.Any(x => x.EventNumber == "500").Should().BeTrue();
            var fileInfo1 = eventFileInfos.Single(x => x.EventNumber == "500");
            fileInfo1.EventId.Should().Be("a2a45ecd-3060-415d-ab5c-ff1f33b8c9a4");
            fileInfo1.EventName.Should().Be("EventA");
            fileInfo1.EventData.Should().Be(fileContent1);

            eventFileInfos.Any(x => x.EventNumber == "501").Should().BeTrue();
            var fileInfo2 = eventFileInfos.Single(x => x.EventNumber == "501");
            fileInfo2.EventId.Should().Be("2a990294-8f3c-467d-ae0b-0b84685a4c4a");
            fileInfo2.EventName.Should().Be("EventA");
            fileInfo2.EventData.Should().Be(fileContent2);
        }

        [Test]
        public void ShouldFilterFileNamesByEventId()
        {
            const string fileName1 = "00000000000000000500_572e2387-00f9-4f8c-af7a-952f1a06b8d2_a2a45ecd-3060-415d-ab5c-ff1f33b8c9a4_EventA.txt";
            const string fileName2 = "00000000000000000501_572e2387-00f9-4f8c-af7a-952f1a06b8d2_2a990294-8f3c-467d-ae0b-0b84685a4c4a_EventA.txt";
            var fileNames = new[]
            {
                // build absoulute file names
                Path.Combine(TestContext.CurrentContext.TestDirectory, fileName1),
                Path.Combine(TestContext.CurrentContext.TestDirectory, fileName2),
            };

            var filteredNames = FileEventStore.FilterFileNames("a2a45ecd-3060-415d-ab5c-ff1f33b8c9a4", fileNames);

            filteredNames.Length.Should().Be(1);
            filteredNames[0].Should().EndWith(fileName1);
        }

        [Test]
        public void ShouldStoreEventFileInfo()
        {
            // arrange
            var eventStoreRootFolder = EventStoreTestHelper.EnsureEmptyRootFolder("storeRoot_4");
            var clientId = Guid.NewGuid().ToString();
            EventStoreTestHelper.CreateTestFile(eventStoreRootFolder, "eventnumbers.txt", clientId);
            var eventsFolder = Path.Combine(eventStoreRootFolder, "events");
            var eventFileInfo = new EventFileInfo
            {
                EventNumber = "1000",
                EventId = Guid.NewGuid().ToString(),
                EventName = "EventA",
                EventData = "TestDaten_EventA"
            };
            var eventFileName = EventFilename.From(eventFileInfo, clientId);
            var fileEventStore = new FileEventStore(eventStoreRootFolder, clientId);

            fileEventStore.Store(eventFileInfo);

            var createdFile = Directory.GetFiles(eventsFolder).Single();
            var fileName = Path.GetFileName(createdFile);
            fileName.Should().Be(eventFileName.Name);

            EventStoreTestHelper.AssertFileContent(createdFile, content =>
            {
                var lines = content.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
                return lines[0] == fileName && lines[1] == eventFileInfo.EventData;
            });
        }
    }
}