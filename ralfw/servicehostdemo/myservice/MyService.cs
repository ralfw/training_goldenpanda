using System;
using System.IO;
using servicehost.contract;

namespace myservice
{
    [Service]
    public class MyService
    {
        [EntryPoint(HttpMethods.Get, "/api/v1/store")] // usage: /api/v1/store?text=hello
        public string Store(string text)
        {
            var dbfilename = Environment.GetEnvironmentVariable("WAGOCREDENTIALS_DBFILENAME");
            Console.WriteLine($"Store in Db: {dbfilename}");
            File.WriteAllText(dbfilename, text);          
            Console.WriteLine("Stored: {0}", text);

            return $"Stored @ {DateTime.Now}";
        }


        [EntryPoint(HttpMethods.Get, "/api/v1/load")] // usage: /api/v1/load
        public string Load()
        {
            var dbfilename = Environment.GetEnvironmentVariable("WAGOCREDENTIALS_DBFILENAME");
            var text = File.ReadAllText(dbfilename);
            
            Console.WriteLine($"Loaded: {text}");

            return $"Loaded '{text.ToUpper()}' @ {DateTime.Now}";
        }
    }
}