using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace bankocrapp.adapters
{
    class FileIO
    {
        public string[] get_source_lines(IEnumerable<string> sourcepaths)
        {
            var filepaths = get_filepaths(sourcepaths);
            return read_lines(filepaths).ToArray();
        }

        private IEnumerable<string> get_filepaths(IEnumerable<string> sourcepaths)
        {
            return sourcepaths.SelectMany(map_to_filenames);

            IEnumerable<string> map_to_filenames(string sourcepath) {
                if (File.Exists(sourcepath))
                    yield return sourcepath;
                else
                    foreach (var filepath in Directory.GetFiles(sourcepath))
                        yield return filepath;
            }
        }

        private IEnumerable<string> read_lines(IEnumerable<string> filepaths) {
            return filepaths.SelectMany(File.ReadAllLines);
        }
    }
}