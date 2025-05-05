using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SurveyMetaCore.models;

public interface ISurveyService
{
    Task<List<Survey>> GetAllSurveysAsync();
    Task<Survey> GetSurveyByIdAsync(string id);
    Task AddSurveyAsync(Survey survey);
    Task UpdateSurveyAsync(Survey survey);
    Task DeleteSurveyAsync(string id);
}

namespace SurveyMetaCore.services
{
    public class SurveyService : ISurveyService
    {
        private readonly ISurveyRepository _repository;

        public SurveyService(ISurveyRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Survey>> GetAllSurveysAsync() => await _repository.GetAllSurveysAsync();

        public async Task<Survey> GetSurveyByIdAsync(string id) => await _repository.GetSurveyByIdAsync(id);

        public async Task AddSurveyAsync(Survey survey) => await _repository.AddSurveyAsync(survey);

        public async Task UpdateSurveyAsync(Survey survey) => await _repository.UpdateSurveyAsync(survey);

        public async Task DeleteSurveyAsync(string id) => await _repository.DeleteSurveyAsync(id);
    }

}

