using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace TaskManagement.Domain.Interfaces
{
    public interface IUserService<TUser> where TUser : IdentityUser
    {
        Task<IdentityResult> CreateAsync(TUser user, string password);
        Task<TUser> GetUserAsync(string userId);
        Task<TUser> ValidateUserIdAsync(string userId, string password);
        Task<TUser> ValidateEmailAsync(string userName, string password);
        Task<IList<Claim>> GetClaimsAsync(TUser user);
    }
}
