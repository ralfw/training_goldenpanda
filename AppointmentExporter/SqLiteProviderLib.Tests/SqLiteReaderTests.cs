using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentContract;
using FluentAssertions;
using NUnit.Framework;

namespace SqLiteProviderLib.Tests
{
    public class SqLiteReaderTests
    {
        [SetUp]
        public void SetUp()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
        }

        [Test]
        public void ShouldReturnAllAppointments()
        {
            var reader = new SqLiteReader();

            var begin = DateTime.Parse("01.11.2018");
            var end = DateTime.Parse("30.11.2018");

            var readDataFromSqLite = reader.ReadDataFromSqLite(begin, end);

            readDataFromSqLite.Count().Should().BeGreaterThan(0);

            foreach (var appointment in readDataFromSqLite)
            {
                Console.WriteLine($"{appointment.Begin}");
                Console.WriteLine($"{appointment.End}");
                Console.WriteLine($"{appointment.Subject}");
            }
        }

    }
}
