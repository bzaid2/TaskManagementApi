using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace TaskManagement.Infrastructure.OpenApi
{
    internal static class Extensions
    {
        internal static IServiceCollection AddOpenApiDocumentation(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SwaggerOptions>(configuration.GetSection(SwaggerOptions.SECTION));
            var config = services.BuildServiceProvider().GetService<IOptions<SwaggerOptions>>();
            if (config is null || !config.Value.Enable)
            {
                return services;
            }
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
            });
            services.ConfigureOptions<ConfigureSwaggerOptions>();
            return services;
        }

        internal static IApplicationBuilder UseOpenApiDocumentation(this IApplicationBuilder app)
        {
            var config = app.ApplicationServices.GetService<IOptions<SwaggerOptions>>();
            if (config is null || !config.Value.Enable)
            {
                return app;
            }
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                var title = app.ApplicationServices.GetService<IOptions<SwaggerOptions>>()!.Value?.Title;
                if (!string.IsNullOrEmpty(title))
                {
                    options.DocumentTitle = title;
                }
                options.DisplayRequestDuration();
                options.DocExpansion(DocExpansion.List);
                options.ShowCommonExtensions();
                options.RoutePrefix = config.Value.RoutePrefix;
                var versions = ((IEndpointRouteBuilder)app).DescribeApiVersions();
                foreach (var version in versions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{version.GroupName}/swagger.json",
                        $"v{version.ApiVersion}");
                }
            });
            return app;
        }
    }
}
