using System;
using System.Collections.Generic;
using AppointmentContract;
using AppointmentCsv;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AppointmenCsv.Tests
{
    [TestFixture]
    public class CsvGeneratorFixture
    {
        [Test]
        public void ShouldGenerateCsvContent()
        {
            var testAppointments = new List<Appointment>
            {
                new Appointment{ Begin = DateTime.Parse("2018-10-02T13:00"), End = DateTime.Parse("2018-10-10T14:00"), Subject = "Termin"}
            };

            var result = CsvGenerator.GenerateContent(testAppointments);

            result.Should().Be("Begin;End;Subject\n2018-10-02T13:00;2018-10-10T14:00;Termin");
        }
    }
}
