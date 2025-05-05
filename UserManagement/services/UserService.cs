using System.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserManagement.models;
using UserManagement.repo;
using UserManagement.utils;

namespace UserManagement.services
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterUserAsync(string email, string password);
        Task<string> LoginAsync(LoginModel login);
        Task SendConfirmationEmailAsync(string emailAddress, string token, string callbackUrl);
        Task<IdentityResult> ConfirmEmailAsync(string userId, string token);
        Task<string> GenerateEmailConfirmationTokenAsync(IdentityUser user);
        Task<IdentityUser> FindByEmailIdAsync(string userId);
        Task<IdentityUser> FindByUserIdAsync(string userId);
    }

    public class UserService : IUserService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public UserService(IUserRepository userRepository, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<string> LoginAsync(LoginModel login)
        {
            return await _userRepository.LoginAsync(login);   
        }

        public async Task<IdentityResult> RegisterUserAsync(string email, string password)
        {
            var roleExists = await _roleManager.RoleExistsAsync("super_admin");
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole("super_admin"));
            }

            var user = new IdentityUser { UserName = email, Email = email };
            var result = await _userRepository.RegisterUserAsync(user, password);
            if (result.Succeeded)
            {
                result = await _userManager.AddToRoleAsync(user, "super_admin");
            }
            return result;

        }

        public async Task SendConfirmationEmailAsync(string emailAddress, string token, string callbackUrl)
        {
            string uid = await _userRepository.GeyUniqueIdByemailAsync(emailAddress);
            var emailSender = new EmailSender();
            var confirmationLink = $"{callbackUrl}?userId={uid}&token={HttpUtility.UrlEncode(token)}";
            await emailSender.SendEmailAsync(emailAddress, $"Please confirm your account by clicking <a href='{confirmationLink}'>here</a>.");
        }

        public async Task<IdentityResult> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userRepository.FindByUserIdAsync(userId);
            if (user == null) throw new InvalidOperationException("User not found.");
            // Check if the email is already confirmed
            

            return await _userRepository.ConfirmEmailAsync(user, token);
        }
        public async Task<string> GenerateEmailConfirmationTokenAsync(IdentityUser user)
        {
            var _user = await _userManager.FindByEmailAsync(user.Email);

            return await _userManager.GenerateEmailConfirmationTokenAsync(_user);
        }
        public async Task<IdentityUser> FindByEmailIdAsync(string userId)
        {
            var result = await _userRepository.FindByEmailIdAsync(userId);
            return result;
        }
        public async Task<IdentityUser> FindByUserIdAsync(string userId)
        {
            var result = await _userRepository.FindByUserIdAsync(userId); ;
            return result;
        }
    }

}
