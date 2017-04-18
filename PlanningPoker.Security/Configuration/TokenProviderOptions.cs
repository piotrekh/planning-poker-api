using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace PlanningPoker.Security.Configuration
{
    public class TokenProviderOptions
    {
        public string Path { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; } = "all";

        public TimeSpan? Expiration { get; set; } = null;

        public SigningCredentials SigningCredentials { get; set; }

        public TokenProviderOptions(AuthenticationSettings settings)
        {
            Path = settings.Path;
            Issuer = settings.Issuer;

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(settings.SecretKey));
            SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
        }
    }
}
