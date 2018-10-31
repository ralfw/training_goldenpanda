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
        private string _eventStoreTestFolder;

        [SetUp]
        public void SetUp()
        {
            _eventStoreTestFolder = Path.Combine(TestContext.CurrentContext.TestDirectory, "testfiles\\eventstore\\");
        }

        [Test, Category("Manual")]
        public void ShouldCreateClientIdIfNotYetSet()
        {
            var clientIdFilePath = Path.Combine(_eventStoreTestFolder, "clientId.txt");
            if (File.Exists(clientIdFilePath))
                File.Delete(clientIdFilePath);

            var provider = GetProvider();

            Guid.Parse(provider.ClientId).Should().NotBe(Guid.Empty);

            var clientId = File.ReadAllText(clientIdFilePath);

            Guid.Parse(clientId).Should().Be(Guid.Parse(provider.ClientId));
        }

        [Test, Category("Manual")]
        public void ShouldRecordAndReplayEvents()
        {
            // arrange
            var eventSourceProvider = GetProvider();

            IEnumerable<Event> eventsToRecord = new[]
            {
                new Event {Nummer = 0, Id = "1", Name = "EventA", Daten = "NutzdatenEventA"},
                new Event {Nummer = 0, Id = "2", Name = "EventB", Daten = "NutzdatenEventB"},
                new Event {Nummer = 0, Id = "3", Name = "EventA", Daten = "NutzdatenEventA"},
            };

            // act
            
            // - record
            eventSourceProvider.Record(eventsToRecord);
            
            // - replay
            var events = eventSourceProvider.Replay().ToList();
            
            //assert
            events[0].Nummer.Should().Be(500L);
            events[0].Id.Should().Be("000000000000000000000000000000000001");
            events[0].Name.Should().Be("EventA");
            events[0].Daten.Should().Be("NutzdatenEventA");

            events[1].Nummer.Should().Be(501L);
            events[1].Id.Should().Be("000000000000000000000000000000000002");
            events[1].Name.Should().Be("EventB");
            events[1].Daten.Should().Be("NutzdatenEventB");

            events[2].Nummer.Should().Be(502L);
            events[2].Id.Should().Be("000000000000000000000000000000000003");
            events[2].Name.Should().Be("EventA");
            events[2].Daten.Should().Be("NutzdatenEventA");

            foreach (var @event in events)
                EventStoreTestHelper.DumpEvent(@event);


        }

        [Test, Category("Manual")]
        public void ShouldBuildEventFromFilename()
        {
            var eventStoreFolder = Path.Combine(TestContext.CurrentContext.TestDirectory, "testfiles\\eventstore2\\");

            var provider = new EventSourceProvider(eventStoreFolder);

            var eFolderName = Path.Combine(eventStoreFolder, "events\\");
            var fileName = "00000000000000000500_1b5b501f-680f-4e53-b09b-8c39689e2f6e_000000000000000000000000000000000001_EventA.txt";
            var fullPathToFile = Path.Combine(eFolderName, fileName);

            var eventFromFilename = provider.CreateEventFromFile(fullPathToFile);

            eventFromFilename.Nummer.Should().Be(500L);
            eventFromFilename.Id.Should().Be("000000000000000000000000000000000001");
            eventFromFilename.Name.Should().Be("EventA");
            eventFromFilename.Daten.Should().Be("NutzdatenEventA");
        }

        [Test]
        public void ShouldBuildNameFromEvent()
        {
            var provider = GetProvider();

            var @event = new Event {Nummer = 100, Id = "1", Name = "EventA"};

            var fileName = provider.BuildFileNameFromEvent(@event);

            Console.Out.WriteLine(fileName);

            fileName.Should().Be($"{@event.Nummer.ToString().PadLeft(20, '0')}_" +
                                 $"{provider.ClientId}_" +
                                 $"{@event.Id.PadLeft(36, '0')}_" +
                                 $"{@event.Name}.txt");
        }

        [Test, Category("Manual")]
        public void ShouldThrowExceptionIfEventNameIsLongerThan20Characters()
        {
            var provider = GetProvider();
            var @event = new Event { Nummer = 100, Id = "1", Name = "123456789012345678901" };

            Action call = () => provider.Record(@event);

            call.ShouldThrow<InvalidOperationException>().WithMessage("Event name exceeds maximum of 20 characters.");
        }

        private EventSourceProvider GetProvider()
        {
            return new EventSourceProvider(_eventStoreTestFolder);
        }
    }
}
