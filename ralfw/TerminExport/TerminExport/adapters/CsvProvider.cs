using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TerminExport.data;

namespace TerminExport.adapters
{
    class CsvProvider
    {
        public void Export(IEnumerable<Appointment> appointments, DateTime begin, DateTime end) {
            var csvLines = Convert(appointments);
            var filename = BuildFilename(begin, end);
            Write(filename, csvLines);
        }

        IEnumerable<string> Convert(IEnumerable<Appointment> appointments) {
            return new[] { "Begin;End;Subject" }.Concat(appointments.Select(Convert));

            string Convert(Appointment appointment)
                => $"{appointment.Begin:s};{appointment.End:s};\"{appointment.Subject}\"";
        }

        string BuildFilename(DateTime begin, DateTime end) {
            return $"{begin:yyyyMMdd}_{end:yyyyMMdd}_appointments.csv";
        }

        void Write(string filename, IEnumerable<string> lines) {
            File.WriteAllLines(filename, lines);
        }
    }
}