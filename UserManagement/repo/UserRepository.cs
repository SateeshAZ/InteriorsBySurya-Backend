using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Azure.Communication.Email;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UserManagement.models;


namespace UserManagement.repo
{
    public interface IUserRepository
    {
        Task<IdentityResult> RegisterUserAsync(IdentityUser user, string password);
        Task<string> LoginAsync(LoginModel login);
        Task<string> GenerateEmailConfirmationTokenAsync(IdentityUser user);
        Task<string> GeyUniqueIdByemailAsync(string user);
        Task<IdentityUser> FindByEmailIdAsync(string userId);
        Task<IdentityUser> FindByUserIdAsync(string userId);
        Task<IdentityResult> ConfirmEmailAsync(IdentityUser user, string token);
    }

    public class UserRepository : IUserRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public UserRepository(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<string> LoginAsync(LoginModel loginModel)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                return null;
            }

            var token = await GenerateJwtToken(user);
            return token;
        }
        public async Task<IdentityResult> RegisterUserAsync(IdentityUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }


        public async Task<string> GenerateEmailConfirmationTokenAsync(IdentityUser user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<string> GeyUniqueIdByemailAsync(string emailAddress)
        {
            var normalizedEmail = emailAddress.ToUpperInvariant();
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedEmail == normalizedEmail);
            return user.Id;
        }

        public async Task<IdentityUser> FindByEmailIdAsync(string userId)
        {
            var result = await _userManager.FindByEmailAsync(userId);
            return result;
        }
        public async Task<IdentityUser> FindByUserIdAsync(string userId)
        {
            var result = await _userManager.FindByIdAsync(userId);
            return result;
        }

        public async Task<IdentityResult> ConfirmEmailAsync(IdentityUser user, string token)
        {
            var result = await _userManager.ConfirmEmailAsync(user, token);

            return result;
        }
        private async Task<string> GenerateJwtToken(IdentityUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);


            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt_SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt_Issuer"],
                audience: _configuration["Jwt_Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
