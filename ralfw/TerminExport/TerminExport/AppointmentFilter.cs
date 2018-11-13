using System;
using System.Collections.Generic;
using System.Linq;
using TerminExport.data;

namespace TerminExport
{
    class AppointmentFilter
    {
        private readonly IEnumerable<Appointment> _appointments;
        public AppointmentFilter(IEnumerable<Appointment> appointments) {
            _appointments = appointments;
        }

        
        public IEnumerable<Appointment> Select(DateTime begin, DateTime end) {
            return new Appointment[0]
                .Concat(_appointments.Where(a => OverlapsInterval(a, begin, end)))
                .Concat(_appointments.Where(a => WithinInterval(a, begin, end)))
                .Concat(_appointments.Where(a => IncludesInterval(a, begin, end)))
                .OrderBy(a => a.Begin).ThenBy(a => a.End);
        }


        private bool OverlapsInterval(Appointment appointment, DateTime begin, DateTime end)
            => (appointment.Begin <= begin && (begin <= appointment.End && appointment.End <= end)) ||
               ((begin <= appointment.Begin && appointment.Begin <= end) && end <= appointment.End);
        
        private bool WithinInterval(Appointment appointment, DateTime begin, DateTime end)
            => begin <= appointment.Begin && appointment.End <= end;

        private bool IncludesInterval(Appointment appointment, DateTime begin, DateTime end)
            => appointment.Begin <= begin && end <= appointment.End;
    }
}