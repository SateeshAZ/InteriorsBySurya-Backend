using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Models
{
    public class QuestionGridModel
    {
        public bool IsGrid { get; set; } = false;
        public List<QuestionModel> Questions { get; set; }  // Questions in the grid
    }

}
