using System.IO;
using System.Management;

namespace Server
{
    public static class Extensions
    {
        public static string UncPath(this string filePath)
        {
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
                uncRoot = (string) managementObject["ProviderName"];
            }

            return filePath.Replace(drivePrefix, uncRoot);
        }
    }
}