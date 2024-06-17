using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TaskManagement.Application.Common.Behaviors;

namespace TaskManagement.Infrastructure.Mediator
{
    internal static class Extensions
    {
        internal static IServiceCollection AddMediator(this IServiceCollection services, Assembly? assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly), "Cannot found the Application Assembly to add MediatR");
            }
            return services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly))
                           .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
    }
}
