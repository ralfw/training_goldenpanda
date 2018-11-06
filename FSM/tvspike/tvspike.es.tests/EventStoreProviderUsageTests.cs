using System;
using System.Collections.Generic;
using NUnit.Framework;
using tvspike.contracts;
// ReSharper disable InconsistentNaming

namespace tvspike.es.tests
{
    [TestFixture, Category("Manual")]
    public class EventStoreProviderUsageTests
    {
        [Test]
        public void Usage_ReplayAll()
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

            var replayedEvents = eventSource.ReplayAll();
            foreach (var replayedEvent in replayedEvents)
            {
                EventStoreTestHelper.DumpEvent(replayedEvent);
            }
        }

        [Test]
        public void Usage_ReplayFromGivenAggregateId()
        {
            var eventSource = new EventSourceProvider(@"D:\temp\eventSource2");

            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            IEnumerable<Event> eventsToRecord = new[]
            {
                new Event {Nummer = 0, Id = id1.ToString(), Name = "EventA", Daten = "Nutzdaten EventA-1#1"},
                new Event {Nummer = 0, Id = id2.ToString(), Name = "EventA", Daten = "Nutzdaten EventA-1#2"},

                new Event {Nummer = 0, Id = id1.ToString(), Name = "EventB", Daten = "Nutzdaten EventB-0#1"},
                new Event {Nummer = 0, Id = id2.ToString(), Name = "EventB", Daten = "Nutzdaten EventB-0#2"},

                new Event {Nummer = 0, Id = id1.ToString(), Name = "EventC", Daten = "Nutzdaten EventC-0#1"},
                new Event {Nummer = 0, Id = id2.ToString(), Name = "EventC", Daten = "Nutzdaten EventC-0#2"},

                new Event {Nummer = 0, Id = id1.ToString(), Name = "EventA", Daten = "Nutzdaten EventA-2#1"},
            };

            eventSource.Record(eventsToRecord);

            DumpReplayedEventsFor(id1, eventSource);
            DumpReplayedEventsFor(id2, eventSource);
        }

        private static void DumpReplayedEventsFor(Guid currentId, EventSourceProvider eventSource)
        {
            Console.WriteLine($"Replay for AggregateId: {currentId}");
            Console.WriteLine("");

            var replayedEvents = eventSource.ReplayFor(currentId);

            foreach (var replayedEvent in replayedEvents)
            {
                EventStoreTestHelper.DumpEvent(replayedEvent);
            }

            Console.WriteLine("");
        }
    }
}