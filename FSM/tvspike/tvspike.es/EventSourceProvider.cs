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
        private long _nextEventNumber;
        private readonly string _eventStoreFolderPath;
        private const string FILENAME_CLIENT_ID = "clientId.txt";
        private const string FILENAME_EVENT_NUMBERS = "eventnumbers.txt";
        private const string DIRNAME_EVENTS_SUBDIR = "events\\";

        public string ClientId { get; private set; }

        public EventSourceProvider(string eventStoreFolderPath)
        {
            _eventStoreFolderPath = eventStoreFolderPath;

            InitWorkFolder();
        }

        private void InitWorkFolder()
        {
            EnsureWorkingDirectoryStructure(_eventStoreFolderPath);
            ClientId = EnsureClientId(_eventStoreFolderPath);
            _nextEventNumber = EnsureNextUniqueEventNumber(_eventStoreFolderPath);
        }

        internal static long EnsureNextUniqueEventNumber(string rootFolderPath)
            {
            var eventNumbersFilePath = Path.Combine(rootFolderPath, FILENAME_EVENT_NUMBERS);
            if (File.Exists(eventNumbersFilePath))
            {
                var eventNumberFileContent = File.ReadAllText(eventNumbersFilePath).Trim();
                return long.Parse(eventNumberFileContent);
            }

            const long id = 500; // initial value
            File.WriteAllText(eventNumbersFilePath, id.ToString());
            return id;
        }

        internal static void EnsureWorkingDirectoryStructure(string rootFolderPath)
        {
            if (!Directory.Exists(rootFolderPath))
                Directory.CreateDirectory(rootFolderPath);

            var eventSubDirPath = Path.Combine(rootFolderPath, DIRNAME_EVENTS_SUBDIR);
            if (!Directory.Exists(eventSubDirPath))
                Directory.CreateDirectory(eventSubDirPath);
        }

        internal static string EnsureClientId(string rootFolderPath)
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

        public void Record(IEnumerable<Event> events)
        {
            events.ToList().ForEach(Record);
            PersistLastId();
        }

        public void Record(Event @event) {
            AssignUniqueNumberToEvent();
                var eventFilename = BuildFileNameFromEvent(@event);
                PersistEvent(eventFilename, @event);

            void AssignUniqueNumberToEvent() => @event.Nummer = _lastId++;
        }


        public string BuildFileNameFromEvent(Event @event)
        {
            var paddedNumber = @event.Nummer.ToString().PadLeft(20,'0');
            var paddedEventId = @event.Id.PadLeft(36, '0');

            if (@event.Name.Length > 20)
                throw new InvalidOperationException("Event name exceeds maximum of 20 characters.");

            return $"{paddedNumber}_{ClientId}_{paddedEventId}_{@event.Name}.txt";
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

        private void PersistNextId()
        {
            File.WriteAllText(Path.Combine(_eventStoreFolderPath, FILENAME_EVENT_NUMBERS), _lastId.ToString());
        }


        public IEnumerable<Event> Replay() // ReplayAll()?
        {
            return Replay(Guid.Empty);
        }

        public IEnumerable<Event> Replay(Guid id) // ReplayFrom()?
        {
            // get all files
            var eventsFolder = Path.Combine(_eventStoreFolderPath, DIRNAME_EVENTS_SUBDIR);
            var allFiles = Directory.GetFiles(eventsFolder).ToList();

            // filter files
            var eventFiles = allFiles.Where(IsEventFile);
            if (id != Guid.Empty)
                eventFiles = eventFiles.Where(file => MatchesAggregateId(file, id));

            // foreach file, get event
            return eventFiles.Select(CreateEventFromFile);
        }

        private bool MatchesAggregateId(string fullPath, Guid id)
        {
            var filename = GetFileNameFromFullPath(fullPath);

            var parts = filename.Split('_');
            return Guid.Parse(parts[2]) == id;
        }

        private bool IsEventFile(string fullPath)
        {
            var filename = GetFileNameFromFullPath(fullPath);
            // workaround to ignore test dummy and other test files for now
            return filename.Length >= 94;
        }

        private static string GetFileNameFromFullPath(string fullPath)
        {
            return fullPath.Substring(fullPath.LastIndexOf('\\') + 1 );
        }

        public Event CreateEventFromFile(string fullPath)
        {
            // e.g.
            // number               clientId                             eventId                           event name
            // 00000000000000000500_1b5b501f-680f-4e53-b09b-8c39689e2f6e_000000000000000000000000000000000001_EventA.txt
            // 1st line in file contains filename.
            // 2nd line in file contains data string.

            var filename = GetFileNameFromFullPath(fullPath);
            
            // parse filename
            var parts = filename.Split('_');
            var parsedNumber = long.Parse(parts[0]);
            var parsedId = parts[2];
            var eventName = parts[3].Split('.')[0];

            // load event
            var data = File.ReadAllLines(fullPath)[1];
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