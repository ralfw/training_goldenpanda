using System;
using tvspike.contracts;

namespace tvspike.es.tests
{
    public class EventStoreTestHelper
    {
        public static void DumpEvent(Event @event)
        {
            Console.WriteLine($"{@event.Nummer}, {@event.Id}, {@event.Name}, {@event.Daten}");
        }
    }
}