using System;

namespace AppointmentContract
{
    public class Appointment
    {
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }

        public string Subject { get; set; }
    }
}
