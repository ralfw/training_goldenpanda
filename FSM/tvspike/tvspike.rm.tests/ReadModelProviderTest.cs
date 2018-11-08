﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using FluentAssertions;
using NUnit.Framework;
using tvspike.contracts;

namespace tvspike.rm.tests
{
    [TestFixture]
    public class ReadModelProviderTest
    {
        [SetUp]
        public void ReleteStorageFile()
        {
            if (File.Exists("store.rm"))
            {
                File.Delete("store.rm");
            }
        }

        [Test]
        public void Sollte3TermineAus3EventsAufbauen()
        {
            var rmProiver = new ReadModelProvider("store.rm");
            var events = Generiere3FakeEvents();

            var result = rmProiver.Aufbauen(events);
            result.Should().HaveCount(3);
        }

        [Test]
        public void Sollte2TermineAus3NeuUnd1LoeschEventAufbauen()
        {
            var rmProiver = new ReadModelProvider("store.rm");
            var events = Generiere3FakeEvents();
            events.Add(new Event()
            {
                Id = events[1].Id,
                Name = "LoescheTermin",
                Nummer = events[1].Nummer + 1
            });

            var result = rmProiver.Aufbauen(events);
            result.Should().HaveCount(2);
        }

        [Test]
        public void Sollte2TermineAus2NeuUnd2GleichenLoeschEventsAufbauen()
        {
            var rmProiver = new ReadModelProvider("store.rm");
            var events = Generiere3FakeEvents();
            events.Add(new Event()
            {
                Id = events[1].Id,
                Name = "LoescheTermin",
                Nummer = events[1].Nummer + 1
            });
            events.Add(new Event()
            {
                Id = events[1].Id,
                Name = "LoescheTermin",
                Nummer = events[1].Nummer + 2
            });

            var result = rmProiver.Aufbauen(events);
            result.Should().HaveCount(2);
        }

        [Test]
        public void SollteStorageFileAnlegen()
        {
            var rmProiver = new ReadModelProvider("store.rm");
            var events = Generiere3FakeEvents();

            rmProiver.Aufbauen(events);

            File.Exists("store.rm").Should().BeTrue();
        }

        private static List<Event> Generiere3FakeEvents()
        {
            var obj = new TerminRM
            {
                Id = Guid.NewGuid().ToString(),
                Behandler = "Dan",
                Kunde = "Johann",
                Von = new DateTime(2018, 10, 16, 18, 00, 00),
                Bis = new DateTime(2018, 10, 16, 18, 45, 00)
            };
            var data = new JavaScriptSerializer().Serialize(obj);
            var events = new List<Event>();
            for (int i = 0; i < 3; i++)
            {
                events.Add(new Event
                {
                    Id = Guid.NewGuid().ToString(),
                    Nummer = DateTime.Now.Ticks,
                    Daten = data,
                    Name = "NeuerTermin"
                });
            }

            return events;
        }

    }
}
