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
        public void ShouldRecordAndReplayEvents()
        {
            // clean up / prepare
            var rootFolder = Path.Combine(TestContext.CurrentContext.TestDirectory, "testfiles\\eventstore\\");

            if (Directory.Exists(rootFolder))
                Directory.Delete(rootFolder, true);
            Directory.CreateDirectory(rootFolder);
            
            // arrange
            var eventSourceProvider = new EventSourceProvider(rootFolder);
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
            var events = eventSourceProvider.ReplayAll().ToList();
            
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
    }
}
