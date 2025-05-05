using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Models
{
    public class CompanyModel
    {
        [Key]
        public string CompanyId { get; set; }
        public string Title { get; set; }
        public string Industry { get; set; }
        public int EmployeeCount { get; set; }
        public string UserId { get; set; }  // Link Company to a User

    }
}
