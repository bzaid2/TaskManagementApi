using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Infrastructure.Persistence.Context;

namespace TaskManagement.Infrastructure.Persistence
{
    internal static class Extensions
    {
        internal static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Application") ?? throw new NullReferenceException("ConnectionString doesn't exist into configuration");
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                // Reemplazar si se requiere hacer pruebas en memoria
                // options.UseInMemoryDatabase(connectionString);
                options.UseSqlServer(connectionString);
            });
            return services;
        }
    }
}
