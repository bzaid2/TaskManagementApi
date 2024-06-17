using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using TaskManagement.Domain.Interfaces;

namespace TaskManagement.Infrastructure.Identity
{
    internal class UserService<TUser>(UserManager<TUser> userManager, SignInManager<TUser> signInManager)
        : IUserService<TUser> where TUser : IdentityUser
    {
        private readonly UserManager<TUser> _userManager = userManager;
        private readonly SignInManager<TUser> _signInManager = signInManager;

        public async Task<IdentityResult> CreateAsync(TUser user, string password) => await _userManager.CreateAsync(user, password);

        public async Task<IList<Claim>> GetClaimsAsync(TUser user) => await _userManager.GetClaimsAsync(user);

        public async Task<TUser> ValidateEmailAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new ApplicationException();
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: true);
            if (!result.Succeeded)
            {
                throw new ApplicationException();
            }
            return user;
        }

        public async Task<TUser> ValidateUserIdAsync(string userId, string password)
        {
            if (await _userManager.FindByIdAsync(userId) is not { } user || !await _userManager.CheckPasswordAsync(user, password))
            {
                throw new ApplicationException();
            }
            return user;
        }

        public async Task<TUser> GetUserAsync(string userId) => await _userManager.FindByIdAsync(userId);
    }
}
