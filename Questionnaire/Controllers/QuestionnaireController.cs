// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}

using System;
using Azure.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Questionnaire.Dtos.question;
using Questionnaire.Services;

namespace Questionnaire.Controllers
{
    public class QuestionnaireController
    {
        private readonly ILogger<QuestionnaireController> _logger;
        private readonly IQuestionnaireService _questionnaireService;

        public QuestionnaireController(ILogger<QuestionnaireController> logger, IQuestionnaireService questionnaireService)
        {
            _logger = logger;
            _questionnaireService = questionnaireService;
        }

        [Function("GetQuestionnaire")]
        public IActionResult GetQuestionnaire([HttpTrigger(AuthorizationLevel.Function, "get", Route = "GetQuestionnaire")] HttpRequest req)
        {
            _logger.LogInformation("Event type");
            return new OkObjectResult("Done");
        }

        [Function("AddeQuestionnaire")]
        public async Task<IActionResult> AddQuestionnaireAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "AddQuestionnaire")] HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            AddQuestionDto question = JsonConvert.DeserializeObject<AddQuestionDto>(requestBody);
            try
            {
                if(question != null)
                {
                    var result = await _questionnaireService.AddQuestionnaireAsync(question, _logger);
                    return new OkObjectResult(result);
                }
                else
                {
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                }
                
            }
            catch (Exception  ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
