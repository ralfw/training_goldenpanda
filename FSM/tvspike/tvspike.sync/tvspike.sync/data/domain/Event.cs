using System.IO;

namespace tvspike.sync
{
    internal class Event {
        private const char SIGNATUR_TRENNZEICHEN = '_';
        
        public string Dateiname;
        public string Daten;

        public string Signatur => Path.GetFileNameWithoutExtension(Dateiname);
        public string ClientId => Dateiname.Split(SIGNATUR_TRENNZEICHEN)[1];
    }
}