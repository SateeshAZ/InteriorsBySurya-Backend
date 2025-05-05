using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Questionnaire.Dtos.question;

namespace Questionnaire.Repos
{
    public interface IQuestionnaireRepo
    {
        Task<dynamic> GetQuestionnaireAsync();
        Task<dynamic> AddQuestionnaireAsync(AddQuestionDto question, ILogger log);
    }
    public class QuestionnaireRepo : IQuestionnaireRepo
    {
        private readonly CosmosClient _cosmosClient;
        private readonly string _databaseId = "InteriorsBySurya";
        private readonly string _containerId = "Questionnaire";

        public QuestionnaireRepo(CosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
        }

        public Task<dynamic> GetQuestionnaireAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<dynamic> AddQuestionnaireAsync(AddQuestionDto question, ILogger log)
        {
            try
            {
                var container = _cosmosClient.GetContainer(_databaseId, _containerId);
                await container.CreateItemAsync(question, new PartitionKey(question.QuestoinnaireId));
                log.LogInformation("Question was saved successfully.");
            }
            catch(Exception ex) {
                log.LogError($"Error: {ex.Message}");
            }
            return question;
        }
    }
}
