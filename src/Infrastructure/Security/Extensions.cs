using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Infrastructure.Security.Jwt;
using TaskManagement.Infrastructure.Security.Permissions;
namespace TaskManagement.Infrastructure.Security
{
    internal static class Extensions
    {
        internal static IServiceCollection AddSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddJwtAuthentication(configuration)
                    .AddPermissionsPolicy();
            return services;
        }
    }
}
