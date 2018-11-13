using System;

namespace AppointmentExporter
{
    public class FakeRequestHandler
    {
        public void Export(DateTime begin, DateTime end)
        {
            onExported(10);
        }

        public event Action<int> onExported;
    }
}