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

    public class EventStoreProvider
    {
        public EventStoreProvider(string storeRootFolderPath)
        : this(storeRootFolderPath, new FileNumberStore(storeRootFolderPath))
        {
            
        }

        internal EventStoreProvider(string storeRootFolderPath, FileNumberStore fileNumberStore)
        {
            _fileNumberStore = fileNumberStore;
            var fileClientIdStore = new FileClientIdStore(storeRootFolderPath);
            _fileEventStore = new FileEventStore(storeRootFolderPath, fileClientIdStore.ClientId);
        }

        public void Record(IEnumerable<Event> events)
        {
            events.ToList().ForEach(Record);
        }

        public void Record(Event @event)
        {
            @event.Nummer = _fileNumberStore.NextNumber();
            var eventFileInfo = CreateEventFileInfo(@event);
            _fileEventStore.Store(eventFileInfo);
        }

        internal static EventFileInfo CreateEventFileInfo(Event @event)
        {
            return new EventFileInfo
            {
                EventNumber = @event.Nummer.ToString(),
                EventId = @event.Id,
                EventName = @event.Name,
                EventData = @event.Daten
            };
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

        private static Event CreateEvent(EventFileInfo eventFileInfo)
        {
            return new Event
            {
                Nummer = long.Parse(eventFileInfo.EventNumber),
                Id = eventFileInfo.EventId,
                Name = eventFileInfo.EventName,
                Daten = eventFileInfo.EventData
            };
        }

        private readonly FileEventStore _fileEventStore;
        private readonly FileNumberStore _fileNumberStore;
    }
}