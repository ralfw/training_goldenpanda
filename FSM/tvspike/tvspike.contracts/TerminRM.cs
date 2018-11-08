using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tvspike.contracts
{
    [Serializable]
    public class TerminRM
    {
        public string Id { get; set; }
        public DateTime Von { get; set; }
        public DateTime Bis { get; set; }
        public string Behandler { get; set; }
        public string Kunde { get; set; }
    }
}
