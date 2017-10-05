using System;
using System.Collections.Generic;
using System.IO;

namespace RomanNumberImportAdapter
{
    public class ImportAdapter
    {
        public string ImportDirectory { get; set; }

        public ImportAdapter(string directory)
        {
            ImportDirectory = directory;
        }

        public Tuple<string[], int> Import()
        {
            var filePaths = ReadFileNamesInFolder();
            var numbers = ReadNumbersFromFiles(filePaths);

            foreach (var filePath in filePaths)
            {
                File.Delete(filePath);
            }
            return new Tuple<string[], int>(numbers, filePaths.Length);
        }

        internal string[] ReadFileNamesInFolder()
        {
            return Directory.GetFiles(ImportDirectory);
        }

        internal string[] ReadNumbersFromFiles(string[] filePaths)
        {
            var result = new List<string>();

            foreach (var filePath in filePaths)
            {
                result.AddRange(File.ReadAllLines(filePath));
            }

            return result.ToArray();
        }
    }
}