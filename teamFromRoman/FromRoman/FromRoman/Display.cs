using System;

namespace FromRoman
{
    public class Display
    {
        public void ShowStatistic(int numberOfFiles, int numberOfRomans)
        {
            Console.WriteLine($"{numberOfFiles} files and {numberOfRomans} numbers were processed.");
        }
    }
}