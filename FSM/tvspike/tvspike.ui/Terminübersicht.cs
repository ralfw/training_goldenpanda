using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tvspike.contracts;

namespace tvspike.ui
{
    public class Terminübersicht
    {
        public event Action<TerminLöschenCommand> TerminLöschen;
        public event Action<NeuerTerminCommand> TerminAnlegen;
        public event Action TerminlisteAnzeigen;

        public void ZeigeTerminUI(IEnumerable<TerminRM> termine)
        {
            var terminListe = termine.ToList();
            TerminListeZeigen(terminListe);
            ZeigeMenüUndWarteAufEingabe(terminListe);
        }

        public void  TerminListeZeigen(IEnumerable<TerminRM> termine)
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
                    TerminlisteAnzeigen?.Invoke();
                    break;
                }
                case "a":
                {
                    //TODO
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
        
        private static void MenüBefehlEndeAusführen(out bool ende)
        {
            ende = true;
        }

        private void TerminNeuAnlegenBefehlAusführen()
        {
            var neuerTerminCommand = Terminbearbeitung.TerminBearbeiten();
            TerminAnlegen?.Invoke(neuerTerminCommand);
        }

        private void TerminLöschenBefehlAusführen(List<TerminRM> termine)
        {
            Console.WriteLine("Terminnummer eingeben:");
            var eingabe = Console.ReadLine();
            Console.WriteLine();
            if (!int.TryParse(eingabe, out int terminnummer)) return;
            var termin = termine[terminnummer];
            TerminLöschen?.Invoke(new TerminLöschenCommand { Id = termin.Id });
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
