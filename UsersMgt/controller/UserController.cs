// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using UserManagement.models;
using UserManagement.services;

namespace UsersMgt.controller
{
    public class UserController
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [Function("RegisterUser")]
        public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "register")] HttpRequest req)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<RegisterUserModel>(requestBody);

            if (data == null || string.IsNullOrWhiteSpace(data.Email) || string.IsNullOrWhiteSpace(data.Password))
            {
                return new BadRequestObjectResult("Invalid input.");
            }

            var result = await _userService.RegisterUserAsync(data.Email, data.Password);
            if (result.Succeeded)
            {
                //var token = await _userService.GenerateEmailConfirmationTokenAsync(new IdentityUser { Email = data.Email });
                //var callbackUrl = $"{req.Scheme}://{req.Host}/api/confirm-email";
                //await _userService.SendConfirmationEmailAsync(data.Email, data.Email, token, callbackUrl);

                return new OkObjectResult("Registration successful. Please check your email for confirmation.");
            }

            return new BadRequestObjectResult(result.Errors);
        }
    }
}
