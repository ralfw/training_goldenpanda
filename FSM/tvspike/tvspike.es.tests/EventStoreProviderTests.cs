﻿using System;
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
        public void ShouldCreateWorkingDirectoryStructureIfNotExists()
        {
            var rootFolder = EventStoreTestHelper.EnsureDeletedRootFolder("eventstore_1");

            EventSourceProvider.EnsureWorkingDirectoryStructure(rootFolder);

            Directory.Exists(rootFolder).Should().BeTrue();
            Directory.Exists(Path.Combine(rootFolder, "events")).Should().BeTrue();
        }

        [Test]
        public void ShouldLeaveExistingWorkingDirectoryStructureUntouched()
        {
            var rootFolder = EventStoreTestHelper.EnsureEmptyRootFolder("eventstore_1_2");
            var eventsSubFolderPath = Path.Combine(rootFolder, "events");
            Directory.CreateDirectory(eventsSubFolderPath);

            var leaveMeHereInRootPath = Path.Combine(rootFolder, "leaveMeHere1.txt");
            var leaveMeHereInEventsSubFolderPath = Path.Combine(rootFolder, "leaveMeHere2.txt");
            EventStoreTestHelper.CreateTestFile(leaveMeHereInRootPath, "LeaveMeHereInRoot");
            EventStoreTestHelper.CreateTestFile(leaveMeHereInEventsSubFolderPath, "LeaveMeHereInEventsSubFolder");

            EventSourceProvider.EnsureWorkingDirectoryStructure(rootFolder);

            Directory.Exists(rootFolder).Should().BeTrue();
            File.Exists(leaveMeHereInRootPath).Should().BeTrue();
            Directory.Exists(eventsSubFolderPath).Should().BeTrue();
            File.Exists(leaveMeHereInEventsSubFolderPath).Should().BeTrue();
        }

        [Test]
        public void ShouldGeneratedAndStoreGuidBasedClientId()
        {
            var rootFolder = EventStoreTestHelper.EnsureEmptyRootFolder("eventstore_2");

            var clientId = EventSourceProvider.GetClientId(rootFolder);

            EventStoreTestHelper.AssertFileContent(Path.Combine(rootFolder, "clientId.txt"), clientId);
        }

        [Test]
        public void ShouldLoadClientId()
        {
            var rootFolder = EventStoreTestHelper.EnsureEmptyRootFolder("eventstore_2_2");
            const string existingClientId = "d876b013-22a9-4b4d-9f32-c6646ac351bd";
            EventStoreTestHelper.CreateTestFile(rootFolder, "clientId.txt", existingClientId);

            var loadedClientId = EventSourceProvider.GetClientId(rootFolder);

            loadedClientId.Should().Be(existingClientId);
        }
    }
}
