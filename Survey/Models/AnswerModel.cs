using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Models
{
    public class AnswerModel
    {
        public string AnswerId { get; set; }
        public string Text { get; set; }
        public int QuotaLimit { get; set; }  // Max selections for this answer
    }

}
