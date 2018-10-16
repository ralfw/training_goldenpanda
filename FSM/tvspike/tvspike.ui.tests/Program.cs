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
        private static Terminübersicht _terminübersicht;

        static void Main(string[] args)
        {
            
            _terminRms = new List<TerminRM>
                            {
                                new TerminRM{Behandler = "Anja",Kunde = "Frau Meier",Von = DateTime.Parse("2018-10-16T12:00"),Bis = DateTime.Parse("2018-10-16T13:00"),Id=Guid.NewGuid().ToString()},
                                new TerminRM{Behandler = "Anja",Kunde = "Frau Müller",Von = DateTime.Parse("2018-10-16T13:00"),Bis = DateTime.Parse("2018-10-16T14:00"),Id=Guid.NewGuid().ToString()},
                                new TerminRM{Behandler = "Sonja",Kunde = "Frau Schmidt",Von = DateTime.Parse("2018-10-16T12:00"),Bis = DateTime.Parse("2018-10-16T14:30"),Id=Guid.NewGuid().ToString()},
                            };

            _terminübersicht = new Terminübersicht();
            _terminübersicht.TerminAnlegen += TerminübersichtTerminAnlegen;
            _terminübersicht.TerminLöschen += TerminübersichtTerminLöschen;
            _terminübersicht.TerminlisteAnzeigen += TerminübersichtTerminlisteAnzeigen;

            _terminübersicht.ZeigeTerminUI(_terminRms);
        }

        private static void TerminübersichtTerminlisteAnzeigen()
        {
            Console.WriteLine($"TEST: Terminliste anzeigen");
            _terminübersicht.TerminListeZeigen(_terminRms);
        }

        private static void TerminübersichtTerminLöschen(TerminLöschenCommand obj)
        {
            Console.WriteLine($"TEST: Termin gelöscht:ID:{obj.Id}");
        }

        private static void TerminübersichtTerminAnlegen(NeuerTerminCommand obj)
        {
            Console.WriteLine($"TEST: Termin angelegt für Kunde:{obj.Kunde}");
        }

        
    }
}
