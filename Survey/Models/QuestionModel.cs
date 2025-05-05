using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Models
{
    public class QuestionModel
    {
        public string QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }  // MultipleChoice, Text, Rating, etc.
        public int QuestionQuotaLimit { get; set; }  // Max responses per question
        public List<AnswerModel> Answers { get; set; }
        public QuestionDependencyModel? Dependency { get; set; }  // Conditional logic for questions
    }

}
