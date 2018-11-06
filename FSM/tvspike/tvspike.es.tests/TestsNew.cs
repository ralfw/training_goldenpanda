using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;

namespace tvspike.es.tests
{
    [TestFixture]
    public class TestsNew
    {
        [Test]
        public void ShouldCreateWorkingDirectoryStructureIfNotExists()
        {
            var rootFolder = Path.Combine(TestContext.CurrentContext.TestDirectory, "eventstore_1");
            if(Directory.Exists(rootFolder))
                Directory.Delete(rootFolder, true);

            EventSourceProvider.EnsureWorkingDirectoryStructure(rootFolder);

            Directory.Exists(rootFolder).Should().BeTrue();
            Directory.Exists(Path.Combine(rootFolder, "events")).Should().BeTrue();
        }

        [Test]
        public void ShouldLeaveExistingWorkingDirectoryStructureUntouched()
        {
            var rootFolder = Path.Combine(TestContext.CurrentContext.TestDirectory, "eventstore_1_2");
            if (Directory.Exists(rootFolder))
                Directory.Delete(rootFolder, true);

            Directory.CreateDirectory(rootFolder);
            var eventsSubFolderPath = Path.Combine(rootFolder, "events");
            Directory.CreateDirectory(eventsSubFolderPath);

            var leaveMeHereInRootPath = Path.Combine(rootFolder, "leaveMeHere1.txt");
            File.WriteAllText(leaveMeHereInRootPath, "LeaveMeHereInRoot");
            var leaveMeHereInEventsSubFolderPath = Path.Combine(eventsSubFolderPath, "leaveMeHere2.txt");
            File.WriteAllText(leaveMeHereInEventsSubFolderPath, "LeaveMeHereInEventsSubFolder");

            EventSourceProvider.EnsureWorkingDirectoryStructure(rootFolder);

            Directory.Exists(rootFolder).Should().BeTrue();
            File.Exists(leaveMeHereInRootPath).Should().BeTrue();
            Directory.Exists(eventsSubFolderPath).Should().BeTrue();
            File.Exists(leaveMeHereInEventsSubFolderPath).Should().BeTrue();
        }

        [Test]
        public void ShouldGeneratedAndStoreGuidBasedClientId()
        {
            var rootFolder = Path.Combine(TestContext.CurrentContext.TestDirectory, "eventstore_2");
            if (Directory.Exists(rootFolder))
                Directory.Delete(rootFolder, true);
            Directory.CreateDirectory(rootFolder);

            var ensureClientId = EventSourceProvider.GetClientId(rootFolder);

            Guid.TryParse(ensureClientId, out var generatedClientId).Should().BeTrue();

            var clientIdFileContent = File.ReadAllText(Path.Combine(rootFolder, "clientId.txt")).Trim();
            Guid.Parse(clientIdFileContent).Should().Be(generatedClientId);
        }

        [Test]
        public void ShouldLoadClientId()
        {
            var rootFolder = Path.Combine(TestContext.CurrentContext.TestDirectory, "eventstore_2_2");
            if (Directory.Exists(rootFolder))
                Directory.Delete(rootFolder, true);
            Directory.CreateDirectory(rootFolder);

            var clientId = Guid.Parse("D876B013-22A9-4B4D-9F32-C6646AC351BD").ToString();
            File.WriteAllText(Path.Combine(rootFolder, "clientId.txt"), clientId);

            var loadedClientId = EventSourceProvider.GetClientId(rootFolder);

            loadedClientId.Should().Be(clientId);
        }

        [Test]
        public void ShouldCreateAndStoreNextUniqueEventNumber()
        {
            var rootFolder = Path.Combine(TestContext.CurrentContext.TestDirectory, "eventstore_3");
            if (Directory.Exists(rootFolder))
                Directory.Delete(rootFolder, true);
            Directory.CreateDirectory(rootFolder);

            var lastEventId = EventSourceProvider.GetNextUniqueEventNumber(rootFolder);

            lastEventId.Should().Be(500); // check for initial value of 500

            var eventNumbersFileContent = File.ReadAllText(Path.Combine(rootFolder, "eventnumbers.txt")).Trim();
            long.Parse(eventNumbersFileContent).Should().Be(lastEventId);
        }

        [Test]
        public void ShouldLoadNextUniqueEventNumber()
        {
            var rootFolder = Path.Combine(TestContext.CurrentContext.TestDirectory, "eventstore_3_2");
            if (Directory.Exists(rootFolder))
                Directory.Delete(rootFolder, true);
            Directory.CreateDirectory(rootFolder);
            File.WriteAllText(Path.Combine(rootFolder, "eventnumbers.txt"), 501L.ToString());

            var lastEventId = EventSourceProvider.GetNextUniqueEventNumber(rootFolder);

            lastEventId.Should().Be(501L);
        }

        [Test]
        public void ShouldBuildEventFromFilenameNew()
        {
            var eventStoreFolder = Path.Combine(TestContext.CurrentContext.TestDirectory, "eventstore_4");
            if (Directory.Exists(eventStoreFolder))
                Directory.Delete(eventStoreFolder, true);
            Directory.CreateDirectory(eventStoreFolder);
            var eventFolderName = Path.Combine(eventStoreFolder, "events");
            Directory.CreateDirectory(eventFolderName);

            // create 
            var eventFileName = "00000000000000000500_1b5b501f-680f-4e53-b09b-8c39689e2f6e_000000000000000000000000000000000001_EventA.txt";
            var fileContent = new[]
            {
                eventFileName,
                "NutzdatenEventA" // event data
            };
            File.WriteAllLines(Path.Combine(eventFolderName, eventFileName), fileContent);

            var @event = EventSourceProvider.CreateEventFromFile(eventFolderName, eventFileName);

            @event.Nummer.Should().Be(500L);
            @event.Id.Should().Be("000000000000000000000000000000000001");
            @event.Name.Should().Be("EventA");
            @event.Daten.Should().Be("NutzdatenEventA");
        }
    }
}