using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Models
{
    public class UserActivityModel
    {
        public UserInfoModel User { get; set; }
        public DateTime UpdatedAt { get; set; }  // Timestamp of contribution
    }

}
