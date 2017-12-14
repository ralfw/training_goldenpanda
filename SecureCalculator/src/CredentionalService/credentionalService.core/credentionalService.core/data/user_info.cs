using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sc.contracts;

namespace credentionalService.core.data
{
    class user_info
    {
        public string user_email;
        public string password_hash;
        public PermissionSet permissions;
    }
}
