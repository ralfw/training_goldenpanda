using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using AppointmentContract;
using AppointmentData;

namespace AppointmentCsv
{
    public class CsvProvider
    {
        public int WriteData(DateTime begin, DateTime end, ICollection<Appointment> appointments)
        {
            CreateCsv(begin, end, appointments);
            return appointments.Count;
        }

        internal async  Task CreateCsv(DateTime begin, DateTime end, IEnumerable<Appointment> appointments)
        {
            var fileName = GenerateFileName(begin, end);
            var csvContent = CsvGenerator.GenerateContent(appointments);
             await FileWriter.WriteFileContentAsync(fileName, csvContent);
        }

        internal string GenerateFileName(DateTime begin, DateTime end)
        {
            return
                $"{begin.ToString("yyyyMMdd", CultureInfo.InvariantCulture)}_{end.ToString("yyyyMMdd", CultureInfo.InvariantCulture)}_appointments.csv";
        }
    }
}
