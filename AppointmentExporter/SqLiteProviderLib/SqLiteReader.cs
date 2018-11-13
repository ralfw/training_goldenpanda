using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AppointmentContract;
using SQLite;

namespace SqLiteProviderLib
{
    [Table("Appointment")]
    public class AppointmentDto
    {
        [Column("ID")]
        public string Id { get; set; }
        public string Begin { get; set; }
        public string End { get; set; }
        public string Subject { get; set; }
    }

    public class SqLiteReader
    {
        public IEnumerable<Appointment> ReadDataFromSqLite(DateTime begin, DateTime end)
        {
            var connection = new SQLiteConnection(Path.Combine("Testfiles", "appointments.sqlite"));

            var appointmentDtos = connection.Table<AppointmentDto>().ToList();

            return Map(appointmentDtos);
        }

        private IEnumerable<Appointment> Map(List<AppointmentDto> appointmentDtos)
        {
            return appointmentDtos.Select(dto => new Appointment
            {
                Begin = DateTime.Parse(dto.Begin),
                End = DateTime.Parse(dto.End),
                Subject = dto.Subject
            });
        }
    }
}
