using System;

namespace tvspike.sync
{
    internal class CLI
    {
        public CLI(string[] args)
        {
            if (args.Length != 3) {
                Console.WriteLine("Usage: tvspike.sync.exe <client Id> <eventstore Pfad>");
                Environment.Exit(1);
            }
            
            ClientId = args[0];
            EventstorePfad = args[1];
            SyncServerUrl = args[2];
        }
        
        public string ClientId { get; private set; }
        public string EventstorePfad { get; private set; }
        public string SyncServerUrl { get; private set; }
    }
}