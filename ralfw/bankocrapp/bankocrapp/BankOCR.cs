using System.Collections.Generic;
using bankocrapp.adapters;
using bankocrapp.interior;

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