using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tvspike.contracts;

namespace tvspike.ui
{
    internal static class Terminbearbeitung
    {
        public static void TerminBearbeiten(ref TerminRM termin)
        {
            var kunde = KundenDatenEingeben(out var behandler, out var startDatum, out var startZeit, out var endDatum, out var endZeit);
            KundenDatenSetzen(termin, kunde, behandler, startDatum, startZeit, endDatum, endZeit);
            Console.WriteLine();
        }

        private static void KundenDatenSetzen(TerminRM termin, string kunde, string behandler, string startDatum, string startZeit, string endDatum, string endZeit)
        {
            termin.Id = Guid.NewGuid()
                            .ToString();
            termin.Kunde = kunde;
            termin.Behandler = behandler;
            if (DateTime.TryParse($"{startDatum}T{startZeit}", out DateTime von))
                termin.Von = von;
            if (DateTime.TryParse($"{endDatum}T{endZeit}", out DateTime bis))
                termin.Bis = bis;
        }

        private static string KundenDatenEingeben(out string behandler, out string startDatum, out string startZeit, out string endDatum, out string endZeit)
        {
            Console.Write("Kunden eingeben:");
            var kunde = Console.ReadLine();
            Console.Write("Behandler eingeben:");
            behandler = Console.ReadLine();
            Console.Write("Startdatum eingeben:");
            startDatum = Console.ReadLine();
            Console.Write("Startzeit eingeben:");
            startZeit = Console.ReadLine();
            Console.Write("Enddatum eingeben:");
            endDatum = Console.ReadLine();
            Console.Write("Endzeit eingeben:");
            endZeit = Console.ReadLine();
            return kunde;
        }
    }
}
