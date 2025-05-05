using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Models
{
    public class QuestionDependencyModel
    {
        public string QuestionId { get; set; }  // Question it depends on
        public string AnswerId { get; set; }  // Answer required for visibility
    }

}
