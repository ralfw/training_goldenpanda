using System;
using System.IO;
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

        public static EventFilename From(string filePath)
        {
            // File name format
            // number               clientId                             eventId                           event name
            // 00000000000000000500_1b5b501f-680f-4e53-b09b-8c39689e2f6e_000000000000000000000000000000000001_EventA.txt

            var fileName = Path.GetFileName(filePath);
            var fileNameParts = fileName.Split('_');
            var eventName = fileNameParts[3].Split('.')[0];
            if (eventName.Length > 20)
                throw new InvalidOperationException("Event name exceeds maximum of 20 characters.");

            return new EventFilename
            {
                Number = fileNameParts[0],
                ClientId = fileNameParts[1],
                EventId = fileNameParts[2],
                EventName = eventName
            };
        }
    }
}