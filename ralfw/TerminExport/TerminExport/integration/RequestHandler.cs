using System;
using System.Linq;
using TerminExport.adapters;

namespace TerminExport.integration
{
    class RequestHandler
    {
        private readonly SQLiteAppointmentProvider _repo;
        private readonly CsvProvider _csv;
        
        public RequestHandler(SQLiteAppointmentProvider repo) {
            _repo = repo;
            _csv = new CsvProvider();
        }
        
        public int HandleExportRequest(DateTime begin, DateTime end) {
            var appointments = _repo.AllAppointments;
            
            var proc = new AppointmentFilter(appointments);
            var filtered = proc.Select(begin, end).ToArray();
            
            _csv.Export(filtered, begin, end);
            
            return filtered.Count();
        }
    }
}