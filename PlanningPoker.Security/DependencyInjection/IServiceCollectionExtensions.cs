using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlanningPoker.Security.Configuration;
using PlanningPoker.Security.Services;

namespace PlanningPoker.Security.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static void AddSecurity(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.Configure<AuthenticationSettings>(configuration.GetSection("Authentication"));

            services.AddScoped<IAuthorizationService, AuthorizationService>();
        }
    }
}
