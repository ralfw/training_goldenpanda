using System;
using System.Collections.Generic;
using NUnit.Framework;
using tvspike.contracts;

namespace tvspike.es.tests
{
    [TestFixture]
    public class EventStoreProviderUsageTests
    {

        [Test, Category("Manual")]
        public void Usage()
        {
            var eventSource = new EventSourceProvider(@"D:\temp\eventSource1");

            IEnumerable<Event> eventsToRecord = new[]
            {
                new Event {Nummer = 0, Id = Guid.NewGuid().ToString(), Name = "EventA", Daten = "Nutzdaten EventA-1"},
                new Event {Nummer = 0, Id = Guid.NewGuid().ToString(), Name = "EventB", Daten = "Nutzdaten EventB-0"},
                new Event {Nummer = 0, Id = Guid.NewGuid().ToString(), Name = "EventC", Daten = "Nutzdaten EventC-0"},
                new Event {Nummer = 0, Id = Guid.NewGuid().ToString(), Name = "EventA", Daten = "Nutzdaten EventA-2"},
            };

            eventSource.Record(eventsToRecord);

            var replayedEvents = eventSource.Replay();
            foreach (var replayedEvent in replayedEvents)
            {
                EventStoreTestHelper.DumpEvent(replayedEvent);
            }
        }
    }
}