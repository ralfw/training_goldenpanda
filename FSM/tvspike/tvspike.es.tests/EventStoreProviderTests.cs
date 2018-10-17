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
                new Event{Nummer = 1, Id = "2", Name = "EventA", Daten = "NutzdatenEventA"}
            };

            provider.Record(events);

        }

        // TODO: Test padding of 19 chars for nummer

        [Test]
        public void ShouldBuildNameFromEvent()
        {
            var clientIdSource = DateTime.Parse("2018-10-16 12:30:00");
            var eventNumberSource = DateTime.Parse("2018-10-16 12:30:01");

            var fakeZeitProvider = new FakeZeitProvider();
            fakeZeitProvider.Add(clientIdSource);
            fakeZeitProvider.Add(eventNumberSource);

            var provider = new EventSourceProvider(_eventStoreFolder, fakeZeitProvider);
            var @event = new Event { Nummer = 100, Id = "1", Name = "EventA"};

            var fileName = provider.BuildFileName(@event);

            fileName.Should().Be($"{eventNumberSource.Ticks.ToString()}_" +
                                 $"{clientIdSource.Ticks.ToString()}_" +
                                 $"{@event.Id}_" +
                                 $"{@event.Name}.txt");
        }

        [Test, Category("Manual")]
        public void ShouldPersistDataToGivenFolder()
        {
            var guidBasedId = Guid.NewGuid().ToString();
            var @event = new Event { Nummer = 0, Id = guidBasedId, Name = "EventA", Daten = "Nutzdaten-EventA" };
            var provider = new EventSourceProvider(_eventStoreFolder);

            string filename = "persistedEvent.txt";
            provider.Persist(filename, @event);
        }
    }
}
