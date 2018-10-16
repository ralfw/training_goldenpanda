using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using tvspike.contracts;

namespace tvspike.rm
{
    public class ReadModelProvider
    {
        public static ICollection<TerminRM> Aufbauen(IEnumerable<Event> events)
        {
            var sortierteEvents = events.OrderBy(e => e.Nummer);
            return GeneriereTermine(sortierteEvents);
        }

        private static List<TerminRM> GeneriereTermine(IOrderedEnumerable<Event> sortierteEvents)
        {
            var terminRms = new List<TerminRM>();
            foreach (var esEvent in sortierteEvents)
            {
                switch (esEvent.Name)
                {
                    case "NeuerTermin":
                        var terminRm = ToTerminRm(esEvent);
                        terminRms.Add(terminRm);
                        break;
                    case "LoescheTermin":
                        terminRms = LoescheTermin(terminRms, esEvent.Id);
                        break;
                    default:
                        throw new ArgumentException("Unbekannter Event Name");
                }
            }

            return terminRms;
        }

        private static List<TerminRM> LoescheTermin(List<TerminRM> aktuelleTermine, string zuLoeschendeTerminId)
        {
            var zuLoeschendeTermin = aktuelleTermine.FirstOrDefault(t => t.Id == zuLoeschendeTerminId);
            if (zuLoeschendeTermin != null)
                aktuelleTermine.Remove(zuLoeschendeTermin);
            return aktuelleTermine;
        }

        private static TerminRM ToTerminRm(Event neuerTerminEvent)
        {
            var daten = new JavaScriptSerializer().Deserialize<TerminRM>(neuerTerminEvent.Daten);
            daten.Id = neuerTerminEvent.Id;
            return daten;
        }


    }

}
