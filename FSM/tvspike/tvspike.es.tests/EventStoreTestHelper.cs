using System;
using System.IO;
using FluentAssertions;
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

        public static void AssertFileContent(string fullPath, string expectedContent)
        {
            File.ReadAllText(fullPath).Trim().Should().Be(expectedContent);
        }

        public static void AssertFileContent(string fullPath, Func<string, bool> comparisonFunc)
        {
            var trim = File.ReadAllText(fullPath).Trim();
            comparisonFunc(trim).Should().BeTrue();
        }

        public static void CreateTestFile(string fullPath, string content)
        {
            File.WriteAllText(fullPath, content);
        }

        public static void CreateTestFile(string fullDirectoryPath, string fileName, string content)
        {
            File.WriteAllText(Path.Combine(fullDirectoryPath, fileName), content);
        }

        public static void CreateTestFile(string fullDirectoryPath, string fileName, string[] content)
        {
            File.WriteAllLines(Path.Combine(fullDirectoryPath, fileName), content);
        }
    }
}