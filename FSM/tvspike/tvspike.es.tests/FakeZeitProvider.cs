using System;
using System.Collections.Generic;

namespace tvspike.es.tests
{
    public class FakeZeitProvider : IZeitProvider
    {
        private readonly List<DateTime> _dateTimes;

        public FakeZeitProvider() : this(new List<DateTime>())
        {
        }

        public FakeZeitProvider(List<DateTime> dateTimes)
        {
            _dateTimes = dateTimes;
        }

        public void Add(DateTime dateTime)
        {
            _dateTimes.Add(dateTime);
        }

        public DateTime Now()
        {
            if (_dateTimes.Count == 0)
                throw new InvalidOperationException();

            var dateTime = _dateTimes[0];
            _dateTimes.RemoveAt(0);
            return dateTime;
        }
    }
}