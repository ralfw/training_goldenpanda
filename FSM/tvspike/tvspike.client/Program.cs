using System;
using System.Collections.Generic;
using System.IO;
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
            // build
            var pathToEventStore = Path.Combine(Path.GetTempPath(), "eventStore");
            var eventSourceProvider = new EventSourceProvider(pathToEventStore);
            _requestHandler = new RequestHandler(eventSourceProvider);
            _terminübersicht = new Terminübersicht();

            // bind
            Terminlöschen_verdrahten();
            
            _terminübersicht.TerminlisteAnzeigen += TerminübersichtTerminlisteAnzeigen;
            _terminübersicht.TerminAnlegen += TerminübersichtTerminAnlegen;

            // run
            var terminliste = _requestHandler.Handle(new QueryTerminliste());
            _terminübersicht.ZeigeTerminUI(terminliste);
        }


        private static void Terminlöschen_verdrahten()
        {
            _terminübersicht.TerminLöschen += cmd =>
            {
                _requestHandler.Handle(cmd);
                var übersicht = _requestHandler.Handle(new QueryTerminliste());
                _terminübersicht.TerminListeZeigen(übersicht);
            };
        }
        
        
        private static void TerminübersichtOnTerminLöschen(TerminLöschenCommand terminLöschenKommando)
        {
            _requestHandler.Handle(terminLöschenKommando);
        }

        private static void TerminübersichtTerminAnlegen(NeuerTerminCommand neuerTerminKommando)
        {
            _requestHandler.Handle(neuerTerminKommando);
        }

        // TODO: query need to to into the ui project
        private static void TerminübersichtTerminlisteAnzeigen()
        {
            _terminübersicht.TerminListeZeigen(_requestHandler.Handle(new QueryTerminliste()));
        }
    }
}
