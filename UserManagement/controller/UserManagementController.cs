// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Azure.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UserManagement.models;
using UserManagement.services;

namespace UserManagement.controller
{
    public class UserRegistration
    {
        private readonly IUserService _userService;

        private readonly ILogger<UserRegistration> _logger;
        private readonly UserManager<IdentityUser> _userManager;


        public UserRegistration(ILogger<UserRegistration> logger, IUserService userService, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _userService = userService;
            _userManager = userManager;

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
                var token = await _userService.GenerateEmailConfirmationTokenAsync(new IdentityUser { Email = data.Email });
                var callbackUrl = $"{req.Scheme}://{req.Host}/api/confirm-email";
                await _userService.SendConfirmationEmailAsync(data.Email, token, callbackUrl);

                return new OkObjectResult("Registration successful. Please check your email for confirmation.");
            }

            return new BadRequestObjectResult(result.Errors);
        }

        [Function("ChangePassword")]
        public async Task<IActionResult> ChangePasswordAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "ChangePassword")] HttpRequest req)
        {

            var authHeader = req.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(authHeader))
            {
                return new UnauthorizedResult();
            }

            var token = authHeader.Substring("Bearer ".Length);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var userId = jwtToken.Claims.FirstOrDefault().Value;

            var data = await new StreamReader(req.Body).ReadToEndAsync();
            var passwordModel = JsonConvert.DeserializeObject<ChangePassword>(data);
            var result = await _userService.ChangePasswordAsync(passwordModel, userId);
            return result.Succeeded ? new OkResult() : new BadRequestObjectResult(result.Errors);
        }

        [Function("ConfirmEmail")]
        public async Task<IActionResult> RunConfirmEmail(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "confirm-email")] HttpRequest req)
        {
            string userId = req.Query["userId"];
            string token = req.Query["token"];
            var user = await _userService.FindByUserIdAsync(userId);
            if (user == null)
            {
                return new BadRequestObjectResult("User not found.");
            }

            // Check if the email is already confirmed
            if (user.EmailConfirmed)
            {
                return new BadRequestObjectResult("Email has already been confirmed.");
            }

            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
            {
                return new BadRequestObjectResult("Invalid parameters.");
            }

            var result = await _userService.ConfirmEmailAsync(userId, token);
            if (result.Succeeded) return new OkObjectResult("Email confirmed successfully.");

            return new BadRequestObjectResult("Email confirmation failed.");
        }


        [Function("Login")]
        public async Task<IActionResult> LoginAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var loginModel = JsonConvert.DeserializeObject<LoginModel>(requestBody);
            string token = await _userService.LoginAsync(loginModel);

            
            if (token != null)
            {
                return new OkObjectResult(new { Token = token });

            }
            return new BadRequestObjectResult(new { messages = "Username or Password is incorrect"});
           
            
        }   
    }
}

