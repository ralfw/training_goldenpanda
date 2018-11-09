using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using tvspike.contracts;

namespace tvspike.es.tests
{
    [TestFixture]
    public class EventStoreProviderTests
    {
        [Test]
        public void ShouldReplayAllRecordedEvents()
        {
            // clean up / prepare
            var rootFolder = Path.Combine(TestContext.CurrentContext.TestDirectory, "eventstore_0");

            if (Directory.Exists(rootFolder))
                Directory.Delete(rootFolder, true);
            Directory.CreateDirectory(rootFolder);
            
            // arrange
            var eventSourceProvider = new EventSourceProvider(rootFolder);
            var eventId1 = Guid.NewGuid().ToString();
            var eventId2 = Guid.NewGuid().ToString();
            var eventId3 = Guid.NewGuid().ToString();

            IEnumerable<Event> eventsToRecord = new[]
            {
                new Event {Nummer = 0, Id = eventId1, Name = "EventA", Daten = "NutzdatenEventA"},
                new Event {Nummer = 0, Id = eventId2, Name = "EventB", Daten = "NutzdatenEventB"},
                new Event {Nummer = 0, Id = eventId3, Name = "EventA", Daten = "NutzdatenEventA"},
            };

            // act
            
            // - record
            eventSourceProvider.Record(eventsToRecord);
            
            // - replay
            var events = eventSourceProvider.ReplayAll().ToList();

            //assert
            events.Count.Should().Be(3);

            events[0].Nummer.Should().Be(500L);
            events[0].Id.Should().Be(eventId1);
            events[0].Name.Should().Be("EventA");
            events[0].Daten.Should().Be("NutzdatenEventA");

            events[1].Nummer.Should().Be(501L);
            events[1].Id.Should().Be(eventId2);
            events[1].Name.Should().Be("EventB");
            events[1].Daten.Should().Be("NutzdatenEventB");

            events[2].Nummer.Should().Be(502L);
            events[2].Id.Should().Be(eventId3);
            events[2].Name.Should().Be("EventA");
            events[2].Daten.Should().Be("NutzdatenEventA");
        }

        [Test]
        public void ShouldReplayRecordedEventsForAGivenAggregateId()
        {
            // clean up / prepare
            var rootFolder = Path.Combine(TestContext.CurrentContext.TestDirectory, "eventstore_0_2");

            if (Directory.Exists(rootFolder))
                Directory.Delete(rootFolder, true);
            Directory.CreateDirectory(rootFolder);

            // arrange
            var eventSourceProvider = new EventSourceProvider(rootFolder);
            var eventId1 = Guid.NewGuid().ToString();
            var eventId2 = Guid.NewGuid().ToString();

            IEnumerable<Event> eventsToRecord = new[]
            {
                new Event {Nummer = 0, Id = eventId1, Name = "EventA", Daten = "NutzdatenEventA_1"},
                new Event {Nummer = 0, Id = eventId1, Name = "EventB", Daten = "NutzdatenEventB_1"},
                new Event {Nummer = 0, Id = eventId2, Name = "EventB", Daten = "NutzdatenEventB_2"},
            };

            // act

            // - record
            eventSourceProvider.Record(eventsToRecord);

            // - replay
            var events = eventSourceProvider.ReplayFor(eventId1).ToList();

            //assert
            events.Count.Should().Be(2);

            events[0].Nummer.Should().Be(500L);
            events[0].Id.Should().Be(eventId1);
            events[0].Name.Should().Be("EventA");
            events[0].Daten.Should().Be("NutzdatenEventA_1");

            events[1].Nummer.Should().Be(501L);
            events[1].Id.Should().Be(eventId1);
            events[1].Name.Should().Be("EventB");
            events[1].Daten.Should().Be("NutzdatenEventB_1");
        }

        [Test]
        public void ShouldCreateWorkingDirectoryStructureIfNotExists()
        {
            var rootFolder = Path.Combine(TestContext.CurrentContext.TestDirectory, "eventstore_1");
            if (Directory.Exists(rootFolder))
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
    }
}
