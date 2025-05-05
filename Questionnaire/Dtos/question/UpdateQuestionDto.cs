using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.Dtos.question
{
    public class UpdateQuestionDto
    {
        // Unique identifier for the question
        public string Id { get; set; } = string.Empty;

        // Question text
        public string QuestionText { get; set; } = string.Empty;

        // Answer options (e.g., Azure, AWS, GCP)
        public List<string> AnswerOptions { get; set; } = [];

        // List of dependencies for the question
        public List<QuestionDependencyDto> Dependencies { get; set; } = [];

        // Order of the question
        public int Order { get; set; }

        // Timestamp for creation
        public DateTime CreatedAt { get; set; }

        // Timestamp for last update
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
