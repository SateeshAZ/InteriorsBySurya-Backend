// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SurveyMetaCore.models;

namespace SurveyMetaCore.controllers
{
    public class SurveyController
    {
        private readonly ILogger<SurveyController> _logger;
        private readonly ISurveyService _service;

        public SurveyController(ILogger<SurveyController> logger, ISurveyService service)
        {
            _logger = logger;
            _service = service;
        }

        [Function("GetAllSurveys")]
        public async Task<IActionResult> GetAllSurveys(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "surveys")] HttpRequest req)
        {
            var surveys = await _service.GetAllSurveysAsync();
            return new OkObjectResult(surveys);
        }

        [Function("GetSurveyById")]
        public async Task<IActionResult> GetSurveyById(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "surveys/{id:guid}")] HttpRequest req, string id)
        {
            var survey = await _service.GetSurveyByIdAsync(id);
            return survey != null ? new OkObjectResult(survey) : new NotFoundResult();
        }

        [Function("AddSurvey")]
        public async Task<IActionResult> AddSurvey(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "surveys")] HttpRequest req)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var survey = JsonConvert.DeserializeObject<Survey>(requestBody);
            await _service.AddSurveyAsync(survey);
            return new OkResult();
        }

        [Function("UpdateSurvey")]
        public async Task<IActionResult> UpdateSurvey(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "surveys/{id:guid}")] HttpRequest req, string id)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var updatedSurvey = JsonConvert.DeserializeObject<Survey>(requestBody);
            updatedSurvey.SurveyID = id.ToString();
            await _service.UpdateSurveyAsync(updatedSurvey);
            return new OkResult();
        }

        [Function("DeleteSurvey")]
        public async Task<IActionResult> DeleteSurvey(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "surveys/{id:guid}")] HttpRequest req, string id)
        {
            await _service.DeleteSurveyAsync(id);
            return new OkResult();
        }

    }
}
