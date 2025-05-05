using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Questionnaire.Dtos.question;
using Questionnaire.Repos;

namespace Questionnaire.Services
{
    public interface IQuestionnaireService
    {
        Task<AddQuestionDto> AddQuestionnaireAsync(AddQuestionDto question, ILogger log);
        Task<dynamic> GetQuestionnaire();
    }
    public class QuestionnaireService : IQuestionnaireService
    {
        private readonly IQuestionnaireRepo _questionnaireRepo;
        public QuestionnaireService(IQuestionnaireRepo questionnaireRepo) {
            _questionnaireRepo = questionnaireRepo;
        }
        public async Task<AddQuestionDto> AddQuestionnaireAsync(AddQuestionDto question, ILogger log)
        {
            //return await _repository.GetItemsAsync();
            return await _questionnaireRepo.AddQuestionnaireAsync(question, log);
        }

        public Task<dynamic> GetQuestionnaire()
        {
            throw new NotImplementedException();
        }
    }
}
