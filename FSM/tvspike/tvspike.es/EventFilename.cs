using System;
using tvspike.contracts;

namespace tvspike.es
{
    public class EventFilename
    {
        public string Number { get; set; }
        public string ClientId { get; set; }
        public string EventId { get; set; }
        public string EventName { get; set; }

        public string Name  => $"{Number}_{ClientId}_{EventId}_{EventName}.txt";

        public static EventFilename From(Event @event, string clientId)
        {
            var paddedNumber = @event.Nummer.ToString().PadLeft(20, '0');
            var paddedEventId = @event.Id.PadLeft(36, '0');

            if (@event.Name.Length > 20)
                throw new InvalidOperationException("Event name exceeds maximum of 20 characters.");

            return new EventFilename {Number = paddedNumber, ClientId = clientId, EventId = paddedEventId, EventName = @event.Name};
        }
    }
}