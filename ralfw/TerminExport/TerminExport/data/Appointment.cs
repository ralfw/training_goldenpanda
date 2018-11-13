using System;
using TerminExport.adapters.SQLite;

namespace TerminExport.data
{
    internal class Appointment {
        [PrimaryKey]
        public string ID { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public string Subject { get; set; }
    }
}