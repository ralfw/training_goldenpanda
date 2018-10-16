using System;
using System.Collections.Generic;
using tvspike.contracts;

namespace tvspike.es
{
    /*
     * Idee: Ablage der Events im ordner _path
     *
     * Beispieleventstore/
     *      clientId.txt        //
     *      events/
     *          <Nummer>_<ClientId>_<Id>_<EventName>.txt    // Eventsignatur, Inhalt der Datei -> Nutzlast
     *          ...
     *
     */

    public class EventSourceProvider
    {
        private readonly string _path;
        private readonly string _clientId;
        private readonly IZeitProvider _zeitProvider;

        public EventSourceProvider(string path, IZeitProvider zeitProvider = null)
        {
            _path = path;
            _zeitProvider = zeitProvider ?? new ZeitProvider();
            _clientId = _zeitProvider.Now().Ticks.ToString();
        }

        public void Record(IEnumerable<Event> events)
        {
            throw new NotImplementedException();
        }

        public string BuildFileName(Event @event)
        {
            // wir ignorieren event.nummer hier, weil wir hierfür die Verantwortlichkeit tragen.

            string number = _zeitProvider.Now().Ticks.ToString();
            return $"{number}_{_clientId}_{@event.Id}_{@event.Name}.txt";
        }

        public void Persist(string filename, Event @event)
        {
            throw new NotImplementedException();
        }
    }
}