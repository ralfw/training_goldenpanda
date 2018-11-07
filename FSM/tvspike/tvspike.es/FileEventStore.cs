using System.IO;
using System.Linq;

namespace tvspike.es
{
    internal class FileEventStore
    {
        private readonly string _absolutePathToWorkingDirectory;

        public string[] GetAllFileNames()
        {
            return Directory.GetFiles(_absolutePathToWorkingDirectory);
        }

        public FileEventStore(string absolutePathToWorkingDirectory)
        {
            _absolutePathToWorkingDirectory = absolutePathToWorkingDirectory;
        }

        public EventFileInfo[] CreateEventFileInfos(string[] filenames)
        {
            return filenames.Select(CreateEventFileInfoFromFilename)
                            .ToArray();
        }

        private EventFileInfo CreateEventFileInfoFromFilename(string fullPath)
        {
            // get data from file
            var data = "";

            // get file name only
            var fileName = new FileInfo(fullPath).Name;

            return new EventFileInfo
            {
                EventName = fileName
            };
        }
    }

    internal class EventFileInfo
    {
        public string EventNumber { get; set; }
        public string EventId { get; set; }
        public string EventName { get; set; }
        public string EventData { get; set; }
    }
}