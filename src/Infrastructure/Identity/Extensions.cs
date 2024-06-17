using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Domain.Interfaces;
using TaskManagement.Infrastructure.Persistence.Context;

namespace TaskManagement.Infrastructure.Identity
{
    internal static class Extensions
    {
        internal static IServiceCollection AddIdentityCustom(this IServiceCollection services) =>
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = false;
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 6;
                options.Lockout.DefaultLockoutTimeSpan = new TimeSpan(0, 5, 0);

            }).AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders().Services
              .AddTransient<ITokenService, TokenService>()
              .AddTransient<IUserService<IdentityUser>, UserService<IdentityUser>>();
    }
}
