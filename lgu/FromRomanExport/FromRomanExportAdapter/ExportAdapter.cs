using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FromRomanExportAdapter
{
    public static class ExportAdapter
    {
        public static void Export(int[] numbers, string outputDir)
        {
            var filename = GenerateUniqueFilename(outputDir);

            var fileContent = CreateFileContent(numbers);

            File.WriteAllLines(filename, fileContent);
        }

        private static string[] CreateFileContent(IEnumerable<int> numbers)
        {
            return numbers.Select(n => n.ToString()).ToArray();
        }

        private static string GenerateUniqueFilename(string outputDir)
        {
            return $"{outputDir}{Guid.NewGuid()}.txt";
        }
    }
}