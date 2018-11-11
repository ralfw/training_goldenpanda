using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using tvspike.contracts;

namespace tvspike.es.tests
{
    [TestFixture]
    public class EventStoreProviderTests
    {
        [SetUp]
        public void SetUp()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
        }

        [Test]
        public void ShouldReplayAllRecordedEvents()
        {
            // clean up / prepare
            var rootFolder = EventStoreTestHelper.EnsureEmptyRootFolder("eventstore_0");

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
            var rootFolder = EventStoreTestHelper.EnsureEmptyRootFolder("eventstore_0_2");

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
        public void ShouldCreateEventFileInfoFromEvent()
        {
            var @event = new Event();

            var eventFileInfo = EventSourceProvider.CreateEventFileInfo(@event);

            eventFileInfo.EventNumber.Should().Be(@event.Nummer.ToString());
            eventFileInfo.EventId.Should().Be(@event.Id);
            eventFileInfo.EventName.Should().Be(@event.Name);
            eventFileInfo.EventData.Should().Be(@event.Daten);
        }

        [Test]
        public void ShouldCreateWorkingDirectoryIfNotExists()
        {
            var rootFolder = EventStoreTestHelper.EnsureDeletedRootFolder("eventstore_1");

            // ReSharper disable once ObjectCreationAsStatement
            new EventSourceProvider(rootFolder);

            Directory.Exists(rootFolder).Should().BeTrue();
        }

        [Test]
        public void ShouldLeaveExistingWorkingDirectoryUntouchedIfExists()
        {
            var rootFolder = EventStoreTestHelper.EnsureEmptyRootFolder("eventstore_1_2");
            var eventsSubFolderPath = Path.Combine(rootFolder, "events");
            Directory.CreateDirectory(eventsSubFolderPath);

            var leaveMeHereInRootPath = Path.Combine(rootFolder, "leaveMeHere1.txt");
            EventStoreTestHelper.CreateTestFile(leaveMeHereInRootPath, "LeaveMeHereInRoot");

            // ReSharper disable once ObjectCreationAsStatement
            new EventSourceProvider(rootFolder);

            Directory.Exists(rootFolder).Should().BeTrue();
            File.Exists(leaveMeHereInRootPath).Should().BeTrue();
        }
    }
}
