using System;

namespace tvspike.es
{
    public class ZeitProvider : IZeitProvider
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}