using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.Dtos.question
{
    public class QuestionDependencyDto
    {
        // ID of the dependent question
        public string QuestionId { get; set; } = string.Empty;

        // Expected answer for the dependency
        public string ExpectedAnswer { get; set; } = string.Empty;
    }
}
