using System.Collections.Generic;
using TerminExport.adapters.SQLite;
using TerminExport.data;

namespace TerminExport.adapters
{
    internal class SQLiteAppointmentProvider
    {
        private SQLiteConnection _db;
        
        public SQLiteAppointmentProvider() : this("appointments.sqlite"){}
        public SQLiteAppointmentProvider(string dbPath) {
            _db = new SQLiteConnection(dbPath);
            _db.CreateTable<Appointment>();
        }
        
        public IEnumerable<Appointment> AllAppointments => _db.Table<Appointment>();
    }
}