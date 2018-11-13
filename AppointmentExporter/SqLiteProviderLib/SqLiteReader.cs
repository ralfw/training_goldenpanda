using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentContract;

namespace SqLiteProviderLib
{
    public class SqLiteReader
    {
        public IEnumerable<Appointment> ReadDataFromSqLite(DateTime begin, DateTime end)
        {
            return new List<Appointment>{new Appointment()};
        }
    }
}
