using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanNumberImportAdapter
{
    public class ImportAdapter
    {
        public string ImportDirectory { get; set; }

        public ImportAdapter(string directory)
        {
            ImportDirectory = directory;
        }

        public string[] ReadFileNamesInFolder()
        {
            var currentDirectory = Directory.GetCurrentDirectory();

            return Directory.GetFiles(ImportDirectory);
        }

        public string[] ReadNumbersFromFiles(string[] filePaths)
        {
            var result = new List<string>();

            foreach (var filePath in filePaths)
            {
                result.AddRange(File.ReadAllLines(filePath));
            }

            return result.ToArray();
        }

        public Tuple<string[], int> Import()
        {
            var filePaths = ReadFileNamesInFolder();
            var numbers = ReadNumbersFromFiles(filePaths);
            return new Tuple<string[], int>(numbers, filePaths.Length);
            
        }
    }
}
