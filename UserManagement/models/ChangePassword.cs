using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.models
{
    public class ChangePassword
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
