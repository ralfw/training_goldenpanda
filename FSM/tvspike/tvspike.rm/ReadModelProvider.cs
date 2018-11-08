using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.Script.Serialization;
using tvspike.contracts;

namespace tvspike.rm
{
    public class ReadModelProvider
    {
        public ReadModelProvider(string readModelStorageFile)
        {
            _readModelStorageFile = readModelStorageFile;
        }

        public ICollection<TerminRM> Aufbauen(IEnumerable<Event> events)
        {
            var sortierteEvents = events.OrderBy(e => e.Nummer).ToList();
            var rmStorage = GeneriereTermine(sortierteEvents);
            Speichern(rmStorage);
            return rmStorage.Termine;
        }

        #region Private methods

        private ReadModelStorage Laden()
        {
            var bf = new BinaryFormatter();

            try
            {
                using (var fsin = new FileStream(_readModelStorageFile, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    return (ReadModelStorage) bf.Deserialize(fsin);
                }
            }
            catch
            {
                return new ReadModelStorage();
            }
        }

        private void Speichern(ReadModelStorage storage)
        {
            var bf = new BinaryFormatter();
            try
            {
                using (var fsout = new FileStream(_readModelStorageFile, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    bf.Serialize(fsout, storage);
                }
            }
            catch (Exception exception)
            {
                throw new Exception($"Fehler beim Speichern des ReadModelStorages ({_readModelStorageFile})", exception);
            }
        }

        private ReadModelStorage GeneriereTermine(ICollection<Event> sortierteEvents)
        {
            var geladenerStorage = Laden();
            var terminRms = geladenerStorage.Termine;
            var neueEvents = FiltereNeueEvents(sortierteEvents, geladenerStorage.LastStoredEventNumber);

            foreach (Event esEvent in neueEvents)
            {
                terminRms = EventVerarbeiten(terminRms, esEvent);
            }

            var letzteEventNummer = neueEvents.Any() ? neueEvents.Last().Nummer : geladenerStorage.LastStoredEventNumber;

            return new ReadModelStorage
            {
                LastStoredEventNumber = letzteEventNummer,
                Termine = terminRms
            };
        }

        private ICollection<Event> FiltereNeueEvents(ICollection<Event> sortierteEvents, long letzteEventNr)
        {
            if (letzteEventNr == 0)
            {
                return sortierteEvents;
            }

            if (sortierteEvents.All(e => e.Nummer != letzteEventNr))
            {
                throw new Exception("Die Events passen nicht zu den gespeicherten ReadModels");
            }

            return sortierteEvents.SkipWhile(e => e.Nummer != letzteEventNr).Skip(1).ToList();
        }

        private List<TerminRM> EventVerarbeiten(List<TerminRM> aktuellesReadModel, Event naechstesEvent)
        {
            switch (naechstesEvent.Name)
            {
                case "NeuerTermin":
                    var terminRm = ToTerminRm(naechstesEvent);
                    aktuellesReadModel.Add(terminRm);
                    break;
                case "LoescheTermin":
                    aktuellesReadModel = LoescheTermin(aktuellesReadModel, naechstesEvent.Id);
                    break;
                default:
                    throw new ArgumentException("Unbekannter Event Name");
            }

            return aktuellesReadModel;
        }

        private List<TerminRM> LoescheTermin(List<TerminRM> aktuelleTermine, string zuLoeschendeTerminId)
        {
            var zuLoeschendeTermin = aktuelleTermine.FirstOrDefault(t => t.Id == zuLoeschendeTerminId);
            if (zuLoeschendeTermin != null)
                aktuelleTermine.Remove(zuLoeschendeTermin);
            return aktuelleTermine;
        }

        private TerminRM ToTerminRm(Event neuerTerminEvent)
        {
            var daten = new JavaScriptSerializer().Deserialize<TerminRM>(neuerTerminEvent.Daten);
            daten.Id = neuerTerminEvent.Id;
            return daten;
        }

        #endregion

        #region Fields

        private readonly string _readModelStorageFile;

        #endregion
    }
}