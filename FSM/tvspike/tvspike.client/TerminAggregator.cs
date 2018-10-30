using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using tvspike.contracts;

namespace tvspike.client
{
    public class TerminAggregator
    {
        public static Event ErstelleTerminLöschenEvent(TerminLöschenCommand terminLöschenCommand)
        {
            var id = GeneriereNeueEventId();
            return ErstelleEvent(id, "", "LoescheTermin");
        }

        public static Event ErstelleNeuerTerminEvent(NeuerTerminCommand neuerTerminCommand)
        {
            var id = GeneriereNeueEventId();
            var daten = ZuDatenString(neuerTerminCommand);
            return ErstelleEvent(id, daten, "NeuerTermin");
        }

        private static string GeneriereNeueEventId()
        {
            return Guid.NewGuid().ToString();
        }

        private static string ZuDatenString(NeuerTerminCommand neuerTerminCommand)
        {
            return new JavaScriptSerializer().Serialize(neuerTerminCommand);
        }

        private static Event ErstelleEvent(string id, string daten, string name)
        {
            return new Event
            {
                Id = id,
                Name = name,
                Daten = daten
            };
        }

    }
}
