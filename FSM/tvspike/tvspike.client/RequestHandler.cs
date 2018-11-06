using System.Collections.Generic;
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

        public IEnumerable<TerminRM> Handle(QueryTerminliste queryTerminliste)
        {
            var events = _eventSourceProvider.ReplayAll();
            return ReadModelProvider.Aufbauen(events);
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