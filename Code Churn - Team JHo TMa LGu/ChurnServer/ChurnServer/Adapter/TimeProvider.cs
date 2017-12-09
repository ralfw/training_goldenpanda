using System;
using ChurnServer.AdapterInterfaces;

namespace ChurnServer.Adapter
{
    public class TimeProvider : ITimeProvider
    {
        public DateTime GetCurrentDateAndTime()
        {
            return DateTime.Now;
        }
    }
}