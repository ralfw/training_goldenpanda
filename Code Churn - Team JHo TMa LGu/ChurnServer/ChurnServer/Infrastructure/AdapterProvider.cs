using ChurnServer.AdapterInterfaces;

namespace ChurnServer.Infrastructure
{
    public static class AdapterProvider
    {
        public static ITimeProvider TimeProvider { get; set; }
    }
}