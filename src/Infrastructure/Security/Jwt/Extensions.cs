using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskManagement.Shared;

namespace TaskManagement.Infrastructure.Security.Jwt
{
    internal static class Extensions
    {
        internal static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                           .AddJwtBearer(option =>
                           {
                               option.TokenValidationParameters = new TokenValidationParameters
                               {
                                   ValidateLifetime = true,
                                   ValidateIssuerSigningKey = true,
                                   ValidateIssuer = false,
                                   ValidateAudience = false,
                                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration[ProjectConstants.JWT_KEY_SECTION] ?? throw new NullReferenceException("JWT Key doesn't exist into configuration")))
                               };
                           }).Services;
        }
    }
}
