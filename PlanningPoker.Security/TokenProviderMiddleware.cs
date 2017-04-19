using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PlanningPoker.Security.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using PlanningPoker.Security.Services;
using PlanningPoker.DataAccess.Entities;
using System.Collections.Generic;
using System.Security.Principal;

namespace PlanningPoker.Security
{
    /*
     * https://stormpath.com/blog/token-authentication-asp-net-core
     */
    public class TokenProviderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TokenProviderOptions _options;
        private readonly IAuthorizationService _authService;

        public TokenProviderMiddleware(
            RequestDelegate next,
            TokenProviderOptions options,
            IAuthorizationService authService)
        {
            _next = next;
            _options = options;
            _authService = authService;
        }

        public Task Invoke(HttpContext context)
        {
            // If the request path doesn't match, skip
            if (!context.Request.Path.Equals(_options.Path, StringComparison.Ordinal))
            {
                return _next(context);
            }

            // Request must be POST with Content-Type: application/x-www-form-urlencoded
            if (!context.Request.Method.Equals("POST")
               || !context.Request.HasFormContentType)
            {
                context.Response.StatusCode = 400;
                return context.Response.WriteAsync("Bad request.");
            }

            return GenerateToken(context);
        }

        private async Task GenerateToken(HttpContext context)
        {
            //username is actually an email
            var username = context.Request.Form["username"];
            var password = context.Request.Form["password"];

            User user = await _authService.Login(username, password);
            if (user == null)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid username or password.");
                return;
            }

            IList<Claim> userClaims = await _authService.GetClaims(user);

            var now = DateTime.UtcNow;

            // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.
            // You can add other claims here, if you want:
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUniversalTime().ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };
            //add identity claims
            claims.AddRange(userClaims);

            var tokenHandler = new JwtSecurityTokenHandler();

            // Create the JWT and write it to a string
            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                notBefore: now,
                expires: _options.Expiration == null ? (DateTime?)null : now.Add(_options.Expiration.GetValueOrDefault()),
                signingCredentials: _options.SigningCredentials);
            var encodedJwt = tokenHandler.WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                expires_in = (int?)_options.Expiration?.TotalSeconds
            };

            // Serialize and return the response
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.None }));
        }        
    }
}
