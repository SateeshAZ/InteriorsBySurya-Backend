using Newtonsoft.Json;

namespace Questionnaire.Dtos.question
{
    public class AddQuestionDto
    {
        public string QuestoinnaireId { get; set; } = Guid.NewGuid().ToString();
        public string QuestoinnaireTitle { get; set; } = string.Empty;
        // Unique identifier for the question
        [JsonProperty("id")]

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string QuestionId { get; set; } = string.Empty;  

        // Question text
        public string QuestionText { get; set; } = string.Empty;

        // Answer options (e.g., Azure, AWS, GCP)
        public List<string> AnswerOptions { get; set; } = [];

        // List of dependencies for the question
        public List<QuestionDependencyDto> Dependencies { get; set; } = [];

        // Order of the question
        public int Order { get; set; }

        // Timestamp for creation
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Timestamp for last update
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
