using System.IO;
using System.Linq;

namespace tvspike.es
{
    internal class FileEventStore
    {
        public FileEventStore(string parentDirectory)
        {
            SetWorkingDirectory(parentDirectory);
            EnsureWorkingDirectory();
        }

        private void SetWorkingDirectory(string parentDirectory)
        {
            _storageDirectory = Path.Combine(parentDirectory, "events");
        }

        private void EnsureWorkingDirectory()
        {
            if(!Directory.Exists(_storageDirectory))
                Directory.CreateDirectory(_storageDirectory);
        }

        // ToDo: check if we need integration test for this method
        internal EventFileInfo[] GetAllEventFileInfos()
        {
            var allFileNames = GetAllFileNames();
            return CreateEventFileInfos(allFileNames);
        }

        // ToDo: check if we need integration test for this method
        internal EventFileInfo[] GetEventFileInfosBy(string eventId)
        {
            var allFileNames = GetAllFileNames();
            var filteredFileNames = FilterFileNames(eventId, allFileNames);
            return CreateEventFileInfos(filteredFileNames);
        }

        public string[] GetAllFileNames()
        {
            return Directory.GetFiles(_storageDirectory);
        }

        internal EventFileInfo[] CreateEventFileInfos(string[] filenames)
        {
            return filenames.Select(CreateEventFileInfo)
                            .OrderBy(e => e.EventNumber)
                            .ToArray();
        }

        private EventFileInfo CreateEventFileInfo(string fullPath)
        {
            // get data from file
            var data = ReadDataFromEventFile(fullPath);

            // get file name only
            return CreateEventFileInfoFrom(fullPath, data);
        }

        internal static string[] FilterFileNames(string eventId, string[] fileNames)
        {
            return fileNames.Where(f => f.Contains(eventId)).ToArray();
        }

        private static string ReadDataFromEventFile(string fullPath)
        {
            return File.ReadAllLines(fullPath)[1].Trim();
        }

        private static EventFileInfo CreateEventFileInfoFrom(string fullPath, string data)
        {
            var eventFilename = EventFilename.From(fullPath);

            var eventFilenameNumber = eventFilename.Number.TrimStart('0');
            return new EventFileInfo
            {
                EventNumber = eventFilenameNumber,
                EventId = eventFilename.EventId,
                EventName = eventFilename.EventName,
                EventData = data
            };
        }

        private string _storageDirectory;
    }
}