using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOCR
{
    public class FileIO
    {
        public List<string> GetFilePaths(string source)
        {
            string[] filePaths = Directory.GetFiles(source);

            return filePaths.ToList();
        }

        public List<string> ReadLines(List<string> filePaths)
        {
            var result = new List<string>();
            foreach (var filePath in filePaths)
            {
                var readLines = File.ReadLines(filePath);
                result.AddRange(readLines);
            }
            return result;
        }
    }
}
