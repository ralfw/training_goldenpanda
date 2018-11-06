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

            var eventId1 = Guid.NewGuid().ToString();
            var eventId2 = Guid.NewGuid().ToString();
            IEnumerable<Event> eventsToRecord = new[]
            {
                new Event {Nummer = 0, Id = eventId1, Name = "EventA", Daten = "Nutzdaten EventA-1#1"},
                new Event {Nummer = 0, Id = eventId2, Name = "EventA", Daten = "Nutzdaten EventA-1#2"},

                new Event {Nummer = 0, Id = eventId1, Name = "EventB", Daten = "Nutzdaten EventB-0#1"},
                new Event {Nummer = 0, Id = eventId2, Name = "EventB", Daten = "Nutzdaten EventB-0#2"},

                new Event {Nummer = 0, Id = eventId1, Name = "EventC", Daten = "Nutzdaten EventC-0#1"},
                new Event {Nummer = 0, Id = eventId2, Name = "EventC", Daten = "Nutzdaten EventC-0#2"},

                new Event {Nummer = 0, Id = eventId1, Name = "EventA", Daten = "Nutzdaten EventA-2#1"},
            };

            eventSource.Record(eventsToRecord);

            DumpReplayedEventsFor(eventId1, eventSource);
            DumpReplayedEventsFor(eventId2, eventSource);
        }

        private static void DumpReplayedEventsFor(string currentId, EventSourceProvider eventSource)
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