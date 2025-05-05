using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SurveyMetaCore.data_base_contexts;
using SurveyMetaCore.models;

public interface ISurveyRepository
{
    Task<List<Survey>> GetAllSurveysAsync();
    Task<Survey> GetSurveyByIdAsync(string id);
    Task AddSurveyAsync(Survey survey);
    Task UpdateSurveyAsync(Survey survey);
    Task DeleteSurveyAsync(string id);
}

namespace SurveyMetaCore.repos
{
    public class SurveyRepository : ISurveyRepository
    {
        private readonly SurveyDbContext _dbContext;

        public SurveyRepository(SurveyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Survey>> GetAllSurveysAsync()
        {
            return await _dbContext.Surveys.ToListAsync();
        }

        public async Task<Survey> GetSurveyByIdAsync(string id)
        {
            return await _dbContext.Surveys.FindAsync(id);
        }

        public async Task AddSurveyAsync(Survey survey)
        {
            await _dbContext.Surveys.AddAsync(survey);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateSurveyAsync(Survey survey)
        {
            _dbContext.Surveys.Update(survey);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteSurveyAsync(string id)
        {
            var survey = await GetSurveyByIdAsync(id);
            if (survey != null)
            {
                _dbContext.Surveys.Remove(survey);
                await _dbContext.SaveChangesAsync();
            }
        }
    }

}
