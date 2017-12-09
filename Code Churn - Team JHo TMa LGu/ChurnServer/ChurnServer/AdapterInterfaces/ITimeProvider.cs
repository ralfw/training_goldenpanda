using System;

namespace ChurnServer.AdapterInterfaces
{
    public interface ITimeProvider
    {
        DateTime GetStartTime();
    }
}