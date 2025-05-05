using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace UsersMgt.repos
{
    public interface IUserRepository
    {
        Task<IdentityResult> RegisterUserAsync(IdentityUser user, string password);
        Task<string> GenerateEmailConfirmationTokenAsync(IdentityUser user);
        Task<IdentityUser> FindByIdAsync(string userId);
        Task<IdentityResult> ConfirmEmailAsync(IdentityUser user, string token);
    }

    public class UserRepository : IUserRepository
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserRepository(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> RegisterUserAsync(IdentityUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(IdentityUser user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<IdentityUser> FindByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(IdentityUser user, string token)
        {
            return await _userManager.ConfirmEmailAsync(user, token);
        }
    }
}
