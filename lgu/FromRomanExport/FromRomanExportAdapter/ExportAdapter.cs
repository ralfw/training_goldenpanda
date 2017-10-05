using System;
using System.IO;
using System.Linq;

namespace FromRomanExportAdapter
{
    public static class ExportAdapter
    {
        public static void Export(int[] numbers, string outputDir)
        {
            var filename = GenerateUniqueFilename(outputDir);

            var fileContent = numbers.Select(n => n.ToString()).ToArray();

            File.WriteAllLines(filename, fileContent);
        }

        private static string GenerateUniqueFilename(string outputDir)
        {
            return $"{outputDir}{Guid.NewGuid()}.txt";
        }
    }
}