using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Survey.Models;


namespace Survey.Repository
{
    public class SurveyRepository : ISurveyRepository
    {
        private readonly CosmosClient _cosmosClient;
        private readonly string _databaseId = "InteriorsBySurya";
        private readonly string _containerId = "Surveys";

        public SurveyRepository(CosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
        }

        public async Task<SurveyModel> GetSurveyAsync(string surveyId)
        {
            var container = _cosmosClient.GetContainer(_databaseId, _containerId);
            return await container.ReadItemAsync<SurveyModel>(surveyId, new PartitionKey(surveyId));
        }

        public async Task<IEnumerable<SurveyModel>> GetAllSurveysAsync()
        {
            var container = _cosmosClient.GetContainer(_databaseId, _containerId);
            var query = container.GetItemQueryIterator<SurveyModel>("SELECT * FROM c");
            var results = new List<SurveyModel>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ReadNextAsync());
            }
            return results;
        }

        public async Task CreateSurveyAsync(SurveyModel survey)
        {
            //survey.Id = Guid.NewGuid().ToString();
            survey.CreatedAt = DateTime.UtcNow;
            var container = _cosmosClient.GetContainer(_databaseId, _containerId);
            await container.CreateItemAsync(survey, new PartitionKey(survey.ClientId));
        }

        public async Task UpdateSurveyAsync(SurveyModel survey)
        {
            var container = _cosmosClient.GetContainer(_databaseId, _containerId);
            try
            {
                var response = await container.ReplaceItemAsync(survey, survey.SurveyId, new PartitionKey(survey.ClientId));
                Console.WriteLine($"Item Found: {response.Resource}");
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("Item does not exist!");
            }   
        }

        public async Task DeleteSurveyAsync(string surveyId)
        {
            var container = _cosmosClient.GetContainer(_databaseId, _containerId);
            await container.DeleteItemAsync<SurveyModel>(surveyId, new PartitionKey(surveyId));
        }
    }

}
