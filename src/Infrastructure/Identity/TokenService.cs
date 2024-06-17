using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagement.Domain.Interfaces;
using TaskManagement.Shared;

namespace TaskManagement.Infrastructure.Identity
{
    internal class TokenService : ITokenService
    {
        private readonly IUserService<IdentityUser> _userService;

        private DateTime _expiration;
        private readonly TimeSpan _timeLife;
        public DateTime Expiration => _expiration;

        private readonly string _jwtKey;
        public TokenService(IConfiguration configuration, IUserService<IdentityUser> userService)
        {
            _jwtKey = configuration[ProjectConstants.JWT_KEY_SECTION];
            _timeLife = configuration.GetValue<TimeSpan>(ProjectConstants.TIME_LIFE_SECTION);
            _userService = userService;
        }

        public async Task<string> GetTokenAsync(string email, string password, CancellationToken cancellationToken)
        {
            var user = await _userService.ValidateEmailAsync(email, password);
            return await GenerateTokenAsync(user);
        }

        private async Task<string> GenerateTokenAsync(IdentityUser user) =>
            GenerateEncryptedToken(GetSigningCredentials(), await GetClaims(user), _expiration);

        private SigningCredentials GetSigningCredentials()
        {
            byte[] secretKey = Encoding.UTF8.GetBytes(_jwtKey ?? throw new NullReferenceException("JWT KEY doesn't exist into configuration"));
            return new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256);
        }

        private async Task<IEnumerable<Claim>> GetClaims(IdentityUser user)
        {
            var claimsDb = await _userService.GetClaimsAsync(user);
            _expiration = DateTime.UtcNow.Add(_timeLife);
            var claims = new List<Claim>
            {
                new (JwtRegisteredClaimNames.NameId, user.Id),
                new (JwtRegisteredClaimNames.Name, user.UserName ?? string.Empty),
                new (JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new (JwtRegisteredClaimNames.Exp, _expiration.ToString("yyyy-MM-dd HH:mm:ss.fffffff", CultureInfo.InvariantCulture))
            };
            claims.AddRange(claimsDb);
            return claims;
        }

        private static string GenerateEncryptedToken(SigningCredentials signingCredentials, IEnumerable<Claim> claims, DateTime expiration)
        {
            var token = new JwtSecurityToken(
               claims: claims,
               expires: expiration,
               signingCredentials: signingCredentials);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
