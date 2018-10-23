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
            _eventStoreFolder = TestContext.CurrentContext.TestDirectory;
        }

        [Test, Category("Manual")]
        public void ShouldRecordEvents()
        {
            var path = Path.Combine(_eventStoreFolder, @"\TestFiles\eventstore");

            var provider = new EventSourceProvider(path);

            IEnumerable<Event> events = new Event[]
            {
                new Event {Nummer = 1, Id = "2", Name = "EventA", Daten = "NutzdatenEventA"}
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
            var provider = new EventSourceProvider(_eventStoreFolder)
            {
                ClientId = Guid.NewGuid().ToString()
            };
            return provider;
        }
    }
}
