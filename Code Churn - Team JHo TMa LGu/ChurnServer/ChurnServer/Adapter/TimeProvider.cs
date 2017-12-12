using System;
using System.Threading;
using ChurnServer.AdapterInterfaces;

namespace ChurnServer.Adapter
{
    public class TimeProvider : ITimeProvider
    {
        public DateTime GetCurrentDateAndTime()
        {
            return DateTime.Now;
        }

        public IDisposable StartTimer(int sampleRate, TimerCallback timerTickAction)
        {
            return new Timer(timerTickAction, null, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(sampleRate));
        }
    }
}