using System.Collections;
using System.Collections.Generic;

namespace ChurnServer.AdapterInterfaces
{
    public interface IFileIo
    {
        IEnumerable<string> GetObservableFiles(string observableDirectoryPath, string[] fileExtensions);

        string[] GetFileContent(string filePath);

        string ToUncPath(string filePath);

        void StoreFileContent(string protocolFilePath, string[] protocolLines);
    }
}