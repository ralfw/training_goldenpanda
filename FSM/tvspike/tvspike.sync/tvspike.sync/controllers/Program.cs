using System.Net;
using System.Runtime.CompilerServices;

namespace tvspike.sync
{
    /*
     * Usage: tvspike.sync.exe <client Id> <eventstore Pfad> <sync server URL>
     *
     * Event store:
     *     - jeder Event steht in einer Datei
     *     - Im Event Store Pfad stehen nur Event-Dateien
     *     - Dateinamenstruktur: <EventNr> "-" <ClientId> "-" <ContextId> "-" <EventName> ".txt"
     *
     * Die Sync Server Url kann auf einen REST-Server zeigen - oder ein Verzeichnis.
     *
     * Beispiele:
     *     tvspike.sync.exe 1234 c:\myeventstore\events http://localhost:8000
     *     tvspike.sync.exe 1234 c:\myeventstore\events c:\outeventstoresyncserver
     */
    internal class Program
    {
        public static void Main(string[] args) {
            var cli = new CLI(args);
            var syncServer = SyncServerFactory.Create(cli.SyncServerUrl);
            var eventStore = new EventStoreProvider(cli.EventstorePfad);
            using (var synclogProvider = new SyncLogProvider()) {
                var app = new App(cli, syncServer, eventStore, synclogProvider);
                app.Run();
            }
        }
    }
}