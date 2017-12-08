using System;

namespace ChurnServer
{
    public static class TimeProvider
    {
        public static DateTime GetStartTime()
        {
            return DateTime.Now;
        }
    }
}