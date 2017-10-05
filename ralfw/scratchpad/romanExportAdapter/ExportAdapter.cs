using System;
using System.Collections.Generic;
using System.IO;


namespace romanConversion.adapters
{
    public class ExportAdapter
    {
        public void Export(IEnumerable<string> numbers, string exportDirectory) {
            Directory.CreateDirectory(exportDirectory);
            var filename = Path.Combine(exportDirectory, Guid.NewGuid() + ".txt");
            File.WriteAllLines(filename, numbers);
        }
    }
}