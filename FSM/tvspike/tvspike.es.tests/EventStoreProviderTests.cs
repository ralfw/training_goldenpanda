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
        private string _eventStoreFolder;

        [SetUp]
        public void SetUp()
        {
            _eventStoreFolder = Path.Combine(TestContext.CurrentContext.TestDirectory, "testfiles\\eventstore\\");
        }

        [Test, Category("Manual")]
        public void ShouldRecordEvents()
        {
            var provider = new EventSourceProvider(_eventStoreFolder);

            IEnumerable<Event> events = new Event[]
            {
                new Event {Nummer = 0, Id = "1", Name = "EventA", Daten = "NutzdatenEventA"},
                new Event {Nummer = 0, Id = "2", Name = "EventB", Daten = "NutzdatenEventB"},
                new Event {Nummer = 0, Id = "3", Name = "EventA", Daten = "NutzdatenEventA"},
            };

            provider.Record(events);
        }

        [Test, Category("Manual")]
        public void ShouldPersistDataToGivenFolder()
        {
            var guidBasedId = Guid.NewGuid().ToString();
            var @event = new Event {Nummer = 0, Id = guidBasedId, Name = "EventA", Daten = "Nutzdaten-EventA"};
            var provider = new EventSourceProvider(_eventStoreFolder);

            string filename = "persistedEvent.txt";
            provider.PersistEvent(filename, @event);
        }

        [Test, Category("Manual")]
        public void ShouldCreateClientIdIfNotYetSet()
        {
            var clientIdFilePath = Path.Combine(_eventStoreFolder, "clientId.txt");
            if(File.Exists(clientIdFilePath))
                File.Delete(clientIdFilePath);

            var provider = GetProvider();

            Guid.Parse(provider.ClientId).Should().NotBe(Guid.Empty);

            var clientId = File.ReadAllText(clientIdFilePath);

            Guid.Parse(clientId).Should().Be(Guid.Parse(provider.ClientId));
        }

        [Test, Category("Manual")]
        public void ShouldReplayEvents()
        {
            var eventSourceProvider = GetProvider();

            var events = eventSourceProvider.Replay().ToList();

            // expected data see 'Record' test
//            events[0].Id.Should().Be("1");
//            events[0].Name.Should().Be("EventA");
//            events[0].Name.Should().Be("NutzdatenEventA");
//
//            events[1].Id.Should().Be("2");
//            events[1].Name.Should().Be("EventB");
//            events[1].Name.Should().Be("NutzdatenEventB");
//
//            events[2].Id.Should().Be("3");
//            events[2].Name.Should().Be("EventA");
//            events[2].Name.Should().Be("NutzdatenEventA");

            foreach (var @event in events)
            {
                var dump = $"{@event.Nummer}, {@event.Id}, {@event.Name}, {@event.Daten}";
                Console.WriteLine(dump);
            }
        }

        [Test]
        public void ShouldBuildEventFromFilename()
        {
            var fileName = "00000000000000000500_e89449a1-37bf-41bd-bb92-2f906ff3386b_000000000000000000000000000000000001_EventA.txt";
            var eventFromFilename = EventSourceProvider.CreateEventFromFile(fileName);

            eventFromFilename.Nummer.Should().Be(500L);
            eventFromFilename.Id.Should().Be("000000000000000000000000000000000001");
            eventFromFilename.Name.Should().Be("EventA");
            // eventFromFilename.Daten.Should().Be("");

            // TODO: add reading of data /TMa
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

        [Test]
        public void ShouldThrowExceptionIfEventNameIsLongerThan20Characters()
        {
            var provider = GetProvider();

            var @event = new Event { Nummer = 100, Id = "1", Name = "123456789012345678901" };

            Action call = () => provider.BuildFileNameFromEvent(@event);

            call.ShouldThrow<InvalidOperationException>().WithMessage("Event name exceeds maximum of 20 characters.");
        }

        private EventSourceProvider GetProvider()
        {
            return new EventSourceProvider(_eventStoreFolder);
        }
    }
}
