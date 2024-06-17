using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Domain.Interfaces;

namespace TaskManagement.Infrastructure.Repository
{
    internal static class Extensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services) => services.AddTransient<ITaskService, TaskService>();
    }
}
