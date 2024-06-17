using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Infrastructure.Identity;
using TaskManagement.Infrastructure.Mapping.Mapste;
using TaskManagement.Infrastructure.Mediator;
using TaskManagement.Infrastructure.Middleware;
using TaskManagement.Infrastructure.OpenApi;
using TaskManagement.Infrastructure.Persistence;
using TaskManagement.Infrastructure.Repository;
using TaskManagement.Infrastructure.Security;
using TaskManagement.Infrastructure.Serializer;
using TaskManagement.Infrastructure.Validator;
using TaskManagement.Infrastructure.Version;

namespace TaskManagement.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies()
                                                  .FirstOrDefault(assembly => assembly.FullName!.Contains("TaskManagement.Application"));
            services.AddSecurity(configuration)
                    .AddCarter()
                    .AddMapping()
                    .AddHttpContextAccessor()
                    .AddJsonConfiguration()
                    .AddRepositories()
                    .AddIdentityCustom()
                    .AddPersistence(configuration)
                    .AddValidator(assembly)
                    .AddMediator(assembly)
                    .AddApiVersion()
                    .AddOpenApiDocumentation(configuration);
            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseHttpsRedirection();
            applicationBuilder.UseExceptionMiddleware()
                   .UseAuthentication()
                   .UseAuthorization()
                   .UseOpenApiDocumentation();
            ((IEndpointRouteBuilder)applicationBuilder).MapEndpoints();
            return applicationBuilder;
        }

        private static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder builder)
        {
            builder.MapCarter();
            return builder;
        }
    }
}
