using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tvspike.contracts;

namespace tvspike.ui
{
    public delegate void TerminLöschenDelegate(TerminRM termin);
    public delegate void TerminAnlegenDelegate(TerminRM termin);
    public delegate List<TerminRM> TermineAktualisierenDelegate();

    public class Terminübersicht
    {
        public event TerminLöschenDelegate TerminLöschenEvent = delegate { };
        public event TerminAnlegenDelegate TerminAnlegenEvent = delegate { };
        public event TermineAktualisierenDelegate TermineAktualisierenEvent;

        public void ZeigeTerminUI(IEnumerable<TerminRM> termine)
        {
            var terminListe = termine.ToList();
            TerminListeZeigen(terminListe);
            ZeigeMenüUndWarteAufEingabe(terminListe);
        }

        private void  TerminListeZeigen(IEnumerable<TerminRM> termine)
        {
            var index = 0;
            foreach (var termin in termine)
            {
                Console.WriteLine($"Nr.:{index++}:Von:{termin.Von:g}; Bis:{termin.Bis:g}; Behandler:{termin.Behandler}; Kunde:{termin.Kunde}");
            }
            
        }

        private void ZeigeMenüUndWarteAufEingabe(List<TerminRM> termine)
        {
            var ende = false;
            while (!ende)
            {
                ZeigeMenü();
                var menüAuswahl = WarteAufMenüAuswahl();
                MenüBefehlAusführen(ref termine, menüAuswahl, ref ende);
            }

        }

        private void MenüBefehlAusführen(ref List<TerminRM> termine, string menüAuswahl, ref bool ende)
        {
            switch (menüAuswahl)
            {
                case "e":
                {
                    MenüBefehlEndeAusführen(out ende);
                    break;
                }
                case "r":
                {
                    TerminListeZeigen(termine);
                    break;
                }
                case "a":
                {
                    TerminListeAktualisierenBefehlAusführen(ref termine);
                    break;
                }
                case "l":
                {
                    TerminLöschenBefehlAusführen(termine);
                    break;
                }
                case "n":
                {
                    TerminNeuAnlegenBefehlAusführen();
                    break;
                }
            }
        }

        private void TerminListeAktualisierenBefehlAusführen(ref List<TerminRM> termine)
        {
            var termineAkualisiert = TermineAktualisierenEvent?.Invoke();
            termine = termineAkualisiert ?? termine;
        }

        private static void MenüBefehlEndeAusführen(out bool ende)
        {
            ende = true;
        }

        private void TerminNeuAnlegenBefehlAusführen()
        {
            var termin = new TerminRM();
            Terminbearbeitung.TerminBearbeiten(ref termin);
            TerminAnlegenEvent(termin);
        }

        private void TerminLöschenBefehlAusführen(List<TerminRM> termine)
        {
            Console.WriteLine("Terminnummer eingeben:");
            var eingabe = Console.ReadLine();
            Console.WriteLine();
            if (!int.TryParse(eingabe, out int terminnummer)) return;
            var termin = termine[terminnummer];
            TerminLöschenEvent(termin);
        }

        private static string WarteAufMenüAuswahl()
        {
            var befehl = Console.ReadLine();
            Console.WriteLine();
            return befehl;
        }

        private static void ZeigeMenü()
        {
            Console.WriteLine("e: Programm beenden");
            Console.WriteLine("r: Terminliste anzeigen");
            Console.WriteLine("a: Terminliste aktualisieren");
            Console.WriteLine("l: Termin löschen");
            Console.WriteLine("n: Termin anlegen");
            Console.Write("Befehl eingeben:");
        }
    }
}
