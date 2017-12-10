using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ChurnServer.AdapterInterfaces;

namespace ChurnServer.Adapter
{
    public class FileIo : IFileIo
    {
        public IEnumerable<string> GetObservableFiles(string observableDirectoryPath, string[] fileExtensions)
        {
            var files = new List<string>();
            foreach (var fileExtension in fileExtensions.ToArray())
                files.AddRange(Directory.GetFiles(observableDirectoryPath, $"*.{fileExtension}", SearchOption.AllDirectories));

            return files;
        }

        public string[] GetFileContent(string filePath)
        {
            return File.ReadAllLines(filePath);
        }

        public string ToUncPath(string filePath)
        {
            return filePath;
        }
    }
}