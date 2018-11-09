﻿using System.IO;
using System.Linq;

namespace tvspike.es
{
    internal class FileEventStore
    {
        public FileEventStore(string absolutePathToWorkingDirectory)
        {
            _absolutePathToWorkingDirectory = absolutePathToWorkingDirectory;
        }

        public string[] GetAllFileNames()
        {
            return Directory.GetFiles(_absolutePathToWorkingDirectory);
        }

        public EventFileInfo[] CreateEventFileInfos(string[] filenames)
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

        private readonly string _absolutePathToWorkingDirectory;
    }
}