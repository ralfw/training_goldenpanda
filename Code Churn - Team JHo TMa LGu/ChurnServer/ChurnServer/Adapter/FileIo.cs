using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
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

        public string ToUncPath(string pathToFile)
        {
                var info = new FileInfo(pathToFile);
                var filePath = info.FullName;

                if (filePath.StartsWith(@"\\"))
                    return filePath;

                if (new DriveInfo(Path.GetPathRoot(filePath)).DriveType != DriveType.Network)
                    return filePath;

                var drivePrefix = Path.GetPathRoot(filePath).Substring(0, 2);
                string uncRoot;

                using (var managementObject = new ManagementObject())
                {
                    var managementPath = $"Win32_LogicalDisk='{drivePrefix}'";
                    managementObject.Path = new ManagementPath(managementPath);
                    uncRoot = (string)managementObject["ProviderName"];
                }

                return filePath.Replace(drivePrefix, uncRoot);
        }

        public void StoreFileContent(string protocolFilePath, string[] protocolLines)
        {
            if (File.Exists(protocolFilePath))
                File.AppendAllLines(protocolFilePath, protocolLines);
            else
                File.WriteAllLines(protocolFilePath, protocolLines);
        }
    }
}