using System.Collections.Generic;

namespace bankocrapp
{
    class BankOCR
    {
        public IEnumerable<string> decode(IEnumerable<string> sourcepaths)
        {
            var fileio = new FileIO();
            
            var lines = fileio.get_source_lines(sourcepaths);
            return Converter.convert(lines);
        }
    }
}