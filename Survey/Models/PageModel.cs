using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Models
{
    public class PageModel
    {
        public string PageId { get; set; }
        public int PageOrder { get; set; }  // Max responses per page
        public int QuotaLimit { get; set; }  // Max responses per page
        public List<QuestionGridModel> Questions { get; set; }
    }

}
