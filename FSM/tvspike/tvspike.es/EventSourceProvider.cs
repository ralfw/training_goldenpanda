using System;
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

    // TODO rename this to EventStoreProvider
    public class EventSourceProvider
    {
        public EventSourceProvider(string eventStoreFolderPath)
        {
            StoreWorkingDirectoryPath(eventStoreFolderPath);
            InitializeWorkFolder();
            // Question: Should we better use a factory to build these? Or a factory which builds this whole instance?
            BuildDependencies();    
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
            var parsedNumber = long.Parse(eventFileInfo.EventNumber);
            return new Event
            {
                Nummer = parsedNumber,
                Id = eventFileInfo.EventId,
                Name = eventFileInfo.EventName,
                Daten = eventFileInfo.EventData
            };
        }

        private void StoreWorkingDirectoryPath(string storePath)
        {
            _storePath = storePath;
        }

        private void InitializeWorkFolder()
        {
            if (!Directory.Exists(_storePath))
                Directory.CreateDirectory(_storePath);
        }

        private void BuildDependencies()
        {
            _fileNumberStore = new FileNumberStore(_storePath);
            _fileClientIdStore = new FileClientIdStore(_storePath);
            _fileEventStore = new FileEventStore(_storePath, _fileClientIdStore.ClientId);
        }

        private string _storePath;
        private FileEventStore _fileEventStore;
        private FileNumberStore _fileNumberStore;
        private FileClientIdStore _fileClientIdStore;
    }
}