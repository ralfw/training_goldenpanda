using System.Collections;
using System.Collections.Generic;

namespace ChurnServer.AdapterInterfaces
{
    public interface IFileIo
    {
        IEnumerable<string> GetObservableFiles(string observableDirectoryPath, string[] fileExtensions);
    }
}