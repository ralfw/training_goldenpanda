namespace tvspike.sync
{
    internal class Event
    {
        private const char SIGNATUR_TRENNZEICHEN = '_';
        
        public string Signatur;
        public string Data;

        public string ClientId => Signatur.Split(SIGNATUR_TRENNZEICHEN)[1];
    }
}