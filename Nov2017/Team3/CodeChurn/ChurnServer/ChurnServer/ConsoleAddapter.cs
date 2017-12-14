using System;

namespace ChurnServer
{
    public class ConsoleAddapter
    {
        public void WriteToConsole(ProtocolEntry[] infos)
        {
            Console.WriteLine($"Lines written: {infos.Length}");
        }
    }
}