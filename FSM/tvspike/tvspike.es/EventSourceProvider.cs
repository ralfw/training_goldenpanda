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
     *      eventnumbers.txt    // enthält die nächste zu vergebene EventNummer, long Wert als String
     *      clientId.txt        // enthält die generierte ClientId, vom Typ Guid
     *      events/
     *          <Nummer>_<ClientId>_<Id>_<EventName>.txt    // Eventsignatur, Inhalt der Datei -> Nutzlast
     *          ...
     *
     */

    public class EventSourceProvider
    {
        public EventSourceProvider(string eventStoreFolderPath)
        {
            StoreEventStoreFolderPath(eventStoreFolderPath);
            InitializeWorkFolder();
            // Question: Should we better use a factory to build these? Or a factory which builds this whole instance?
            BuildDependencies();    
        }

        public void Record(Event @event)
        {
            @event.Nummer = _fileNumberStore.NextNumber();
            var eventFilename = EventFilename.From(@event, _fileClientIdStore.ClientId).Name;
            PersistEvent(eventFilename, @event);
        }

        public void Record(IEnumerable<Event> events)
        {
            events.ToList().ForEach(Record);
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

        private void PersistEvent(string filename, Event @event)
        {
            var lines = new[]
            {
                $"{filename}",
                $"{@event.Daten}"
            };
            // TODO: use FileEventStore to persist the event /TMa
            var eventsFolder = Path.Combine(_eventStoreFolderPath, DIRNAME_EVENTS_SUBDIR);
            File.WriteAllLines(Path.Combine(eventsFolder, filename), lines);
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

        private void StoreEventStoreFolderPath(string eventStoreFolderPath)
        {
            _eventStoreFolderPath = eventStoreFolderPath;
        }

        private void InitializeWorkFolder()
        {
            EnsureWorkingDirectoryStructure(_eventStoreFolderPath);
        }

        internal static void EnsureWorkingDirectoryStructure(string rootFolderPath)
        {
            if (!Directory.Exists(rootFolderPath))
                Directory.CreateDirectory(rootFolderPath);

            // todo move the responsibility to the FileEventStore class
            var eventSubDirPath = Path.Combine(rootFolderPath, DIRNAME_EVENTS_SUBDIR);
            if (!Directory.Exists(eventSubDirPath))
                Directory.CreateDirectory(eventSubDirPath);
        }

        private void BuildDependencies()
        {
            _fileEventStore = new FileEventStore(Path.Combine(_eventStoreFolderPath, DIRNAME_EVENTS_SUBDIR));
            _fileNumberStore = new FileNumberStore(_eventStoreFolderPath);
            _fileClientIdStore = new FileClientIdStore(_eventStoreFolderPath);
        }

        private const string DIRNAME_EVENTS_SUBDIR = "events";

        private string _eventStoreFolderPath;
        private FileEventStore _fileEventStore;
        private FileNumberStore _fileNumberStore;
        private FileClientIdStore _fileClientIdStore;
    }
}