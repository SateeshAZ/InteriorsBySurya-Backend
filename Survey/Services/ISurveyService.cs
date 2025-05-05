using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Survey.Models;

namespace Survey.Services
{
    public interface ISurveyService
    {
        Task<SurveyModel> GetSurveyAsync(string surveyId);
        Task<IEnumerable<SurveyModel>> GetAllSurveysAsync();
        Task CreateSurveyAsync(SurveyModel survey);
        Task UpdateSurveyAsync(SurveyModel survey);
        Task DeleteSurveyAsync(string surveyId);
    }

}
