using System.Collections.Generic;
using System.Globalization;
using AppointmentContract;

namespace AppointmentCsv
{
    public static class CsvGenerator
    {
        public static string GenerateContent(IEnumerable<Appointment> appointments)
        {
            var csvContent = "Begin;End;Subject\n";
            foreach (var appointment in appointments)
            {
                var beginString = appointment.Begin.ToString("yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture);
                var endString = appointment.End.ToString("yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture);
                csvContent += $"{beginString};{endString};\"{appointment.Subject}\"\n";
            }

            return csvContent;
        }
    }
}
