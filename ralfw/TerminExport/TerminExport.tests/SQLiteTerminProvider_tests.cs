using System;
using System.Linq;
using NUnit.Framework;
using TerminExport.adapters;

namespace TerminExport.tests
{
    [TestFixture]
    public class SQLiteTerminProviderTests
    {
        [SetUp]
        public void Setup() {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
        }
        
        [Test]
        public void Alle_Termine_laden()
        {
            var sut = new SQLiteAppointmentProvider("appointments.sqlite");
            var result = sut.AllAppointments.ToArray();
            Assert.AreEqual(8,result.Length);
        }
    }
}