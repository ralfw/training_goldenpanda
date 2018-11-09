﻿using System;
using System.Collections;
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
        private readonly string _eventStoreFolderPath;
        private FileEventStore _fileEventStore;
        private FileNumberStore _fileNumberStore;
        private const string FILENAME_CLIENT_ID = "clientId.txt";
        private const string DIRNAME_EVENTS_SUBDIR = "events";

        public string ClientId { get; private set; }

        public EventSourceProvider(string eventStoreFolderPath)
        {
            _eventStoreFolderPath = eventStoreFolderPath;

            InitWorkFolder();

            _fileEventStore = new FileEventStore(Path.Combine(_eventStoreFolderPath, DIRNAME_EVENTS_SUBDIR));
            _fileNumberStore = new FileNumberStore(_eventStoreFolderPath);
        }

        private void InitWorkFolder()
        {
            EnsureWorkingDirectoryStructure(_eventStoreFolderPath);
            ClientId = GetClientId(_eventStoreFolderPath);
        }

        internal static void EnsureWorkingDirectoryStructure(string rootFolderPath)
        {
            if (!Directory.Exists(rootFolderPath))
                Directory.CreateDirectory(rootFolderPath);

            var eventSubDirPath = Path.Combine(rootFolderPath, DIRNAME_EVENTS_SUBDIR);
            if (!Directory.Exists(eventSubDirPath))
                Directory.CreateDirectory(eventSubDirPath);
        }

        internal static string GetClientId(string rootFolderPath)
        {
            var clientIdFilePath = Path.Combine(rootFolderPath, FILENAME_CLIENT_ID);
            if (File.Exists(clientIdFilePath))
            {
                var content = File.ReadAllText(clientIdFilePath).Trim();
                return Guid.Parse(content).ToString();
            }

            var clientId = Guid.NewGuid().ToString();
            File.WriteAllText(clientIdFilePath, clientId);
            return clientId;
        }

        public void Record(Event @event)
        {
            @event.Nummer = _fileNumberStore.NextNumber();
            var eventFilename = EventFilename.From(@event, ClientId).Name;
            PersistEvent(eventFilename, @event);
        }

        public void Record(IEnumerable<Event> events)
        {
            events.ToList().ForEach(Record);
        }

        private void PersistEvent(string filename, Event @event)
        {
            var lines = new[]
            {
                $"{filename}",
                $"{@event.Daten}"
            };

            var eventsFolder = Path.Combine(_eventStoreFolderPath, DIRNAME_EVENTS_SUBDIR);
            File.WriteAllLines(Path.Combine(eventsFolder, filename), lines);
        }

        public IEnumerable<Event> ReplayAll()
        {
            var eventFileInfos = _fileEventStore.GetAllEventFileInfos();
            return CreateEvents(eventFileInfos);
        }

        public IEnumerable<Event> ReplayFor(string eventId)
        {
            var eventFileInfos = _fileEventStore.GetEventFileInfosBy(eventId);
            return CreateEvents(eventFileInfos);
        }

        private IEnumerable<Event> CreateEvents(EventFileInfo[] eventFileInfos)
        {
            return eventFileInfos.Select(CreateEvent);
        }

        private Event CreateEvent(EventFileInfo eventFileInfo)
        {
            var parsedNumber = long.Parse(eventFileInfo.EventNumber);
            return new Event
            {
                Nummer = parsedNumber,
                Id = eventFileInfo.EventId,
                Name = eventFileInfo.EventName,
                Daten = eventFileInfo.EventData
            };
        }

        private bool MatchesAggregateId(string filename, string eventId)
        {
            var parts = filename.Split('_');
            return parts[2] == eventId;
        }

        private static string GetFileNameFromFullPath(string fullPath)
        {
            return fullPath.Substring(fullPath.LastIndexOf(Path.DirectorySeparatorChar) + 1 );
        }

        public static Event CreateEventFromFile(string directory, string filename)
        {
            // e.g.
            // number               clientId                             eventId                           event name
            // 00000000000000000500_1b5b501f-680f-4e53-b09b-8c39689e2f6e_000000000000000000000000000000000001_EventA.txt
            // 1st line in file contains filename.
            // 2nd line in file contains data string.

            // parse filename
            var parts = filename.Split('_');

            var parsedNumber = long.Parse(parts[0]);
            var parsedId = parts[2];
            var eventName = parts[3].Split('.')[0];

            // load event
            var data = File.ReadAllLines(Path.Combine(directory, filename))[1];

            return new Event
            {
                Nummer = parsedNumber,
                Id = parsedId,
                Name = eventName,
                Daten = data
            };
        }
    }
}