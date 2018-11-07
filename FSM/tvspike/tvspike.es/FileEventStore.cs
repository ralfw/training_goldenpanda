using System.IO;

namespace tvspike.es
{
    public class FileEventStore
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
    }
}