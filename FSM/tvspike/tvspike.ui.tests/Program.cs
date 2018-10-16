using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tvspike.contracts;

namespace tvspike.ui.tests
{
    class Program
    {
        static List<TerminRM> _terminRms;
        static void Main(string[] args)
        {
            
            _terminRms = new List<TerminRM>
                            {
                                new TerminRM{Behandler = "Anja",Kunde = "Frau Meier",Von = DateTime.Parse("2018-10-16T12:00"),Bis = DateTime.Parse("2018-10-16T13:00"),Id=Guid.NewGuid().ToString()},
                                new TerminRM{Behandler = "Anja",Kunde = "Frau Müller",Von = DateTime.Parse("2018-10-16T13:00"),Bis = DateTime.Parse("2018-10-16T14:00"),Id=Guid.NewGuid().ToString()},
                                new TerminRM{Behandler = "Sonja",Kunde = "Frau Schmidt",Von = DateTime.Parse("2018-10-16T12:00"),Bis = DateTime.Parse("2018-10-16T14:30"),Id=Guid.NewGuid().ToString()},
                            };

            var terminübersicht = new Terminübersicht();
            terminübersicht.TerminAnlegenEvent += TerminübersichtTerminAnlegenEvent;
            terminübersicht.TerminLöschenEvent += TerminübersichtTerminLöschenEvent;
            terminübersicht.TermineAktualisierenEvent += TerminübersichtTermineAktualisierenEvent;

            terminübersicht.ZeigeTerminUI(_terminRms);
        }

        private static List<TerminRM> TerminübersichtTermineAktualisierenEvent()
        {
            Console.WriteLine($"TEST: Terminliste aktualisiert!");
            return _terminRms;
        }

        private static void TerminübersichtTerminLöschenEvent(TerminRM termin)
        {
            Console.WriteLine($"TEST: Termin gelöscht:ID:{termin.Id}");
            _terminRms.Remove(termin);
        }

        private static void TerminübersichtTerminAnlegenEvent(TerminRM termin)
        {
            Console.WriteLine($"TEST: Termin angelegt:ID:{termin.Id}");
            _terminRms.Add(termin);
        }
    }
}
