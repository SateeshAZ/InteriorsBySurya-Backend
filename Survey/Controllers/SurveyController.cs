using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
using Survey.Services;
using Survey.Models;

namespace Survey.Controllers
{
    public class SurveyController
    {
        private readonly ISurveyService _service;

        public SurveyController(ISurveyService service)
        {
            _service = service;
        }

        [Function("GetSurvey")]
        public async Task<HttpResponseData> GetSurvey(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "survey/{surveyId}")] HttpRequestData req,
            string surveyId,
            FunctionContext context)
        {
            var survey = await _service.GetSurveyAsync(surveyId);
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(survey);
            return response;
        }

        [Function("CreateSurvey")]
        public async Task<HttpResponseData> CreateSurvey(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req,
            FunctionContext context)
        {
            var survey = await req.ReadFromJsonAsync<SurveyModel>();
            await _service.CreateSurveyAsync(survey);
            var response = req.CreateResponse(HttpStatusCode.Created);
            await response.WriteStringAsync("Survey created successfully.");
            return response;
        }

        [Function("UpdateSurvey")]
        public async Task<HttpResponseData> UpdateSurvey(
            [HttpTrigger(AuthorizationLevel.Function, "put")] HttpRequestData req,
            FunctionContext context)
        {
            var survey = await req.ReadFromJsonAsync<SurveyModel>();
            await _service.UpdateSurveyAsync(survey);
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteStringAsync("Survey updated successfully.");
            return response;
        }

        [Function("DeleteSurvey")]
        public async Task<HttpResponseData> DeleteSurvey(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "survey/{surveyId}")] HttpRequestData req,
            string surveyId,
            FunctionContext context)
        {
            await _service.DeleteSurveyAsync(surveyId);
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteStringAsync("Survey deleted successfully.");
            return response;
        }
    }

}
