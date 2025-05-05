using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Survey.Models;
using Survey.Repository;

namespace Survey.Services
{
    public class SurveyService : ISurveyService
    {
        private readonly ISurveyRepository _repository;

        public SurveyService(ISurveyRepository repository)
        {
            _repository = repository;
        }

        public Task<SurveyModel> GetSurveyAsync(string surveyId) => _repository.GetSurveyAsync(surveyId);
        public Task<IEnumerable<SurveyModel>> GetAllSurveysAsync() => _repository.GetAllSurveysAsync();
        public Task CreateSurveyAsync(SurveyModel survey) => _repository.CreateSurveyAsync(survey);
        public Task UpdateSurveyAsync(SurveyModel survey) => _repository.UpdateSurveyAsync(survey);
        public Task DeleteSurveyAsync(string surveyId) => _repository.DeleteSurveyAsync(surveyId);
    }

}
