using Microsoft.Extensions.DependencyInjection;

namespace TaskManagement.Infrastructure.Security.Permissions
{
    internal static class Extensions
    {
        internal static IServiceCollection AddPermissionsPolicy(this IServiceCollection services) =>
            services.AddAuthorization();
    }
}
