using Asp.Versioning;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Shared;

namespace TaskManagement.Infrastructure.Version
{
    internal static class Extensions
    {
        internal static IServiceCollection AddApiVersion(this IServiceCollection services)
        {
            services.AddApiVersioning(setup =>
            {
                setup.DefaultApiVersion = new ApiVersion(1, 0);
                setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.ReportApiVersions = true;
                setup.ApiVersionReader = new HeaderApiVersionReader(ProjectConstants.VERSION_HEADER);
            }).AddApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });
            return services;
        }
    }
}
