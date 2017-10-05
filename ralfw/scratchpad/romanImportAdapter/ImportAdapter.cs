using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace romanConversion.adapters
{
    public class ImportAdapter
    {
        public string[] Import(string importDirectory) {
            var filenames = Find_input_files(importDirectory);
            var numbers = Import_numbers(filenames);
            Clear_input_files(filenames);
            return numbers;
        }
        
        internal string[] Find_input_files(string importDirectory) {
            return Directory.GetFiles(importDirectory);
        }

        internal string[] Import_numbers(IEnumerable<string> filenames)
        {
            var numbers = filenames.SelectMany(File.ReadAllLines)
                .Where(r => !string.IsNullOrWhiteSpace(r));
            return numbers.ToArray();
        }

        void Clear_input_files(IEnumerable<string> filenames) {
            filenames.ToList().ForEach(File.Delete);
        }
    }
}