using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace TaskManagement.Infrastructure.Validator
{
    internal static class Extensions
    {
        internal static IServiceCollection AddValidator(this IServiceCollection services, Assembly? assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly), "Cannot found the Application Assembly to add Validators");
            }
            return services.AddValidatorsFromAssembly(assembly);
        }
    }
}
