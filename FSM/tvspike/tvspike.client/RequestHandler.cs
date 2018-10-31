﻿using System.Collections.Generic;
using tvspike.contracts;
using tvspike.es;
using tvspike.rm;

namespace tvspike.client
{
    public class RequestHandler
    {
        private readonly EventSourceProvider _eventSourceProvider;

        public RequestHandler(EventSourceProvider eventSourceProvider)
        {
            _eventSourceProvider = eventSourceProvider;
        }

        public IEnumerable<TerminRM> TerminlisteLaden()
        {
            var events = _eventSourceProvider.Replay();
            return ReadModelProvider.Aufbauen(events);
        }

        public void TerminHinzufuegen(NeuerTerminCommand neuerTerminCommand)
        {
            var neuesEvent = TerminAggregator.ErstelleNeuerTerminEvent(neuerTerminCommand);
            _eventSourceProvider.Record(new []{neuesEvent});
        }
    }
}