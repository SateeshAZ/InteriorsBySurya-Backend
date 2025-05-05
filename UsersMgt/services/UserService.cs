using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity;
using UsersMgt.repos;

namespace UsersMgt.services
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterUserAsync(string email, string password);
        //Task SendConfirmationEmailAsync(string email, string userId, string token, string callbackUrl);
        Task<IdentityResult> ConfirmEmailAsync(string userId, string token);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IdentityResult> RegisterUserAsync(string email, string password)
        {
            var user = new IdentityUser { UserName = email, Email = email };
            return await _userRepository.RegisterUserAsync(user, password);
        }

        //public async Task SendConfirmationEmailAsync(string email, string userId, string token, string callbackUrl)
        //{
        //    var confirmationLink = $"{callbackUrl}?userId={userId}&token={Uri.EscapeDataString(token)}";
        //    await _emailSender.SendEmailAsync(email, "Confirm your email",
        //        $"Please confirm your account by clicking <a href='{confirmationLink}'>here</a>.");
        //}

        public async Task<IdentityResult> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userRepository.FindByIdAsync(userId);
            if (user == null) throw new InvalidOperationException("User not found.");
            return await _userRepository.ConfirmEmailAsync(user, token);
        }
    }
}
