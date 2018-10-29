using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using tvspike.contracts;

namespace tvspike.es
{
    /*
     * Idee: Ablage der Events im ordner _path
     *
     * Beispieleventstore/
     *      clientId.txt        //
     *      events/
     *          <Nummer>_<ClientId>_<Id>_<EventName>.txt    // Eventsignatur, Inhalt der Datei -> Nutzlast
     *          ...
     *
     */

    public class EventSourceProvider
    {
        private readonly string _path;
        private long? _lastId;

        public EventSourceProvider(string path)
        {
            _path = path;
        }

        public string ClientId { get; set; }

        public void Record(IEnumerable<Event> events)
        {
            foreach (var @event in events)
            {
                AssignUniqueNumberToEvent(@event);
                var eventFilename = BuildFileNameFromEvent(@event);
                PersistEvent(eventFilename, @event);
            }

            PersistLastId();
        }

        private void PersistLastId()
        {
            File.WriteAllText(Path.Combine(_path, "eventnumbers.txt"), _lastId.ToString());
        }

        private void AssignUniqueNumberToEvent(Event @event)
        {
            if (!_lastId.HasValue)
            {
                _lastId = LoadLastNumber();
            }

            @event.Nummer = _lastId.Value;
            _lastId += 1;
        }

        private long LoadLastNumber()
        {
            var readAllLines = File.ReadAllText(Path.Combine(_path, "eventnumbers.txt"));
            return long.Parse(readAllLines);
        }

        public string BuildFileNameFromEvent(Event @event)
        {
            var paddedNumber = @event.Nummer.ToString().PadLeft(20,'0');
            var paddedEventId = @event.Id.PadLeft(36, '0');

            if (@event.Name.Length > 20)
                throw new InvalidOperationException("Event name exceeds maximum of 20 characters.");

            return $"{paddedNumber}_{ClientId}_{paddedEventId}_{@event.Name}.txt";
        }

        public void PersistEvent(string filename, Event @event)
        {
            var eventData = "Fake for " + filename;
            var eventsFolder = Path.Combine(_path, "events\\");
            File.WriteAllText(Path.Combine(eventsFolder, filename), eventData);
        }
    }
}