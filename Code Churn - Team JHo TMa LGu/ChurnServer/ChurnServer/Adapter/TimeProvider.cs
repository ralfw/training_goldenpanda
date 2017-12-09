using System;
using ChurnServer.AdapterInterfaces;

namespace ChurnServer.Adapter
{
    public class TimeProvider : ITimeProvider
    {
        public DateTime GetStartTime()
        {
            return DateTime.Now;
        }
    }
}