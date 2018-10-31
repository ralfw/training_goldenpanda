using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tvspike.contracts;
using tvspike.es;
using tvspike.rm;
using tvspike.ui;

namespace tvspike.client
{
    class Program
    {
        private static RequestHandler _requestHandler;
        private static Terminübersicht _terminübersicht;

        static void Main(string[] args)
        {
            var pathToEventStore = @"c:\temp\eventStore";
            var eventSourceProvider = new EventSourceProvider(pathToEventStore);
            _requestHandler = new RequestHandler(eventSourceProvider);
            _terminübersicht = new Terminübersicht();
            _terminübersicht.TerminlisteAnzeigen += TerminübersichtTerminlisteAnzeigen;

            var terminliste = _requestHandler.TerminlisteLaden();
            _terminübersicht.ZeigeTerminUI(terminliste);
        }

        private static void TerminübersichtTerminlisteAnzeigen()
        {
            _terminübersicht.TerminListeZeigen(_requestHandler.TerminlisteLaden());
        }
    }
}
