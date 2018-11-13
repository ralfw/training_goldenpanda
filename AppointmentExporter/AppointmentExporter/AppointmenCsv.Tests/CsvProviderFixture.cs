using System;
using System.Globalization;
using AppointmentCsv;
using FluentAssertions;
using NUnit.Framework;

namespace AppointmenCsv.Tests
{
    [TestFixture]
    public class CsvProviderFixture
    {
        [Test]
        public void ShouldCreateFileName()
        {
            var begin = DateTime.Parse("2018-10-20T14:00");
            var end = DateTime.Parse("2018-11-02T14:00");

            var sut = new CsvProvider();
            var resultString =
                $"{begin.ToString("YYYYMMDD", CultureInfo.InvariantCulture)}_{end.ToString("YYYYMMDD", CultureInfo.InvariantCulture)}_appointments.csv";
            sut.GenerateFileName(begin, end).Should().Be(resultString);
        }
    }
}
