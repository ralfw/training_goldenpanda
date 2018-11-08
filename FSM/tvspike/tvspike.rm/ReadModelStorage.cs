using System;
using System.Collections.Generic;
using tvspike.contracts;

namespace tvspike.rm
{
    [Serializable]
    public class ReadModelStorage
    {
        public long LastStoredEventNumber { get; set; }
        public List<TerminRM> Termine { get; set; } = new List<TerminRM>();
    }
}