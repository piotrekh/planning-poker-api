using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PlanningPoker.Security.Configuration;

namespace PlanningPoker.Security.DependencyInjection
{
    public static class IApplicationBuilderExtensions
    {
        public static void UseSecurity(this IApplicationBuilder app)
        {
            var authSettings = app.ApplicationServices.GetService<IOptions<AuthenticationSettings>>();
            var tokenProviderOptions = new TokenProviderOptions(authSettings.Value);
            app.UseMiddleware<TokenProviderMiddleware>(tokenProviderOptions);
            app.UseJwtBearerAuthentication(new JwtBearerOptions()
            {
                AuthenticationScheme = "Bearer",
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = tokenProviderOptions.SigningCredentials.Key,

                    ValidateIssuer = true,
                    ValidIssuer = authSettings.Value.Issuer,
                    
                    ValidateAudience = false,
                    ValidateLifetime = false                    
                }
            });
        }
    }
}
