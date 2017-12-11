using System;
using System.Threading;

namespace ChurnServer.AdapterInterfaces
{
    public interface ITimeProvider
    {
        DateTime GetCurrentDateAndTime();
        IDisposable StartTimer(int sampleRate, TimerCallback timerTickAction);
    }
}