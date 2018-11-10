using System;
using System.IO;
using NUnit.Framework;
using tvspike.contracts;

namespace tvspike.es.tests
{
    public class EventStoreTestHelper
    {
        public static void DumpEvent(Event @event)
        {
            Console.WriteLine($"{@event.Nummer}, {@event.Id}, {@event.Name}\r\n    Data '{@event.Daten}'");
        }

        internal static string EnsureEmptyRootFolder(string folderName)
        {
            var rootFolder = EnsureDeletedRootFolder(folderName);
            Directory.CreateDirectory(rootFolder);
            return rootFolder;
        }

        internal static string EnsureDeletedRootFolder(string folderName)
        {
            var rootFolder = Path.Combine(TestContext.CurrentContext.TestDirectory, folderName);
            if (Directory.Exists(rootFolder))
                Directory.Delete(rootFolder, true);
            return rootFolder;
        }
    }
}