using System.Collections.Generic;
using tvspike.contracts;
using tvspike.es;
using tvspike.rm;

namespace tvspike.client
{
    public class RequestHandler
    {
        private readonly EventSourceProvider _eventSourceProvider;
        private readonly ReadModelProvider _readModelProvider;

        public RequestHandler(EventSourceProvider eventSourceProvider, ReadModelProvider readModelProvider)
        {
            _eventSourceProvider = eventSourceProvider;
            _readModelProvider = readModelProvider;
        }

        public IEnumerable<TerminRM> Handle(QueryTerminliste queryTerminliste)
        {
            var events = _eventSourceProvider.ReplayAll();
            return _readModelProvider.Aufbauen(events);
        }

        public void Handle(TerminLöschenCommand terminLöschenCommand) {
            var events = TerminAggregator.Process(terminLöschenCommand);
            _eventSourceProvider.Record(events);
        }

        public void Handle(NeuerTerminCommand neuerTerminCommand)
        {
            var neuesEvent = TerminAggregator.Process(neuerTerminCommand);
            _eventSourceProvider.Record(new []{neuesEvent});
        }
    }
}