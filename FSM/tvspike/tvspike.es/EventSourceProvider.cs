﻿using System;
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
        private long? _lastId;
        private readonly string _eventStoreFolderPath;
        private const string FILENAME_CLIENT_ID = "clientId.txt";
        private const string FILENAME_EVENT_NUMBERS = "eventnumbers.txt";
        private const string DIRNAME_EVENTS_SUBDIR = "events\\";

        public EventSourceProvider(string eventStoreFolderPath)
        {
            _eventStoreFolderPath = eventStoreFolderPath;
            LoadOrCreateClientId();
        }

        private void LoadOrCreateClientId()
        {
            var clientIdFilePath = Path.Combine(_eventStoreFolderPath, FILENAME_CLIENT_ID);
            if (!File.Exists(clientIdFilePath))
            {
                ClientId = Guid.NewGuid().ToString();
                File.WriteAllText(clientIdFilePath, ClientId);
            }
            else
            {
                ClientId = Guid.Parse(File.ReadAllText(clientIdFilePath)).ToString();
            }
        }

        public string ClientId { get; private set; }

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
            File.WriteAllText(Path.Combine(_eventStoreFolderPath, FILENAME_EVENT_NUMBERS), _lastId.ToString());
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
            var readAllLines = File.ReadAllText(Path.Combine(_eventStoreFolderPath, FILENAME_EVENT_NUMBERS));
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
            var lines = new[]
            {
                $"{filename}",
                $"{@event.Daten}"
            };

            var eventsFolder = Path.Combine(_eventStoreFolderPath, DIRNAME_EVENTS_SUBDIR);
            File.WriteAllLines(Path.Combine(eventsFolder, filename), lines);
        }

        public IEnumerable<Event> Replay()
        {
            // get all files
            var eventsFolder = Path.Combine(_eventStoreFolderPath, DIRNAME_EVENTS_SUBDIR);
            var allFiles = Directory.GetFiles(eventsFolder).ToList();

            // filter files
            var eventFile = allFiles.Where(IsEventFile);

            // foreach file, get event
            return eventFile.Select(CreateEventFromFile);
        }

        private bool IsEventFile(string fullPath)
        {
            Console.WriteLine(fullPath);
            var filename = fullPath.Substring(fullPath.LastIndexOf('\\') + 1 );
            Console.WriteLine(filename);
            
            // workaround to ignore test dummy and other test files for now
            return filename.Length >= 94;
        }

        public static Event CreateEventFromFile(string fullPath)
        {
            // e.g.
            // number               clientId                             eventId                           event name
            // 00000000000000000500_1b5b501f-680f-4e53-b09b-8c39689e2f6e_000000000000000000000000000000000001_EventA.txt
            // 1st line in file contains filename.
            // 2nd line in file contains data string.

            var filename = fullPath.Substring(fullPath.LastIndexOf('\\') + 1);
            var parts = filename.Split('_');

            var parsedNumber = long.Parse(parts[0]);
            var parsedId = parts[2];
            var eventName = parts[3].Split('.')[0];

            return new Event
            {
                Nummer = parsedNumber,
                Id = parsedId,
                Name = eventName
            };
        }
    }
}