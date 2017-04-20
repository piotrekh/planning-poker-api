using Microsoft.AspNetCore.Http;
using PlanningPoker.Domain.Providers.Context;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace PlanningPoker.Providers.Context
{
    public class UserContextProvider : IUserContextProvider
    {
        public bool IsAuthenticated { get; } = false;

        public int? Id { get; } = null;

        public string Email { get; } = null;

        public List<Claim> Claims { get; } = null;

        public string FirstName { get; } = null;

        public string LastName { get; } = null;

        public UserContextProvider(IHttpContextAccessor httpContextAccessor)
        {
            if(httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated == true)
            {
                IsAuthenticated = httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

                Claims = httpContextAccessor.HttpContext.User.Claims.ToList();
                Id = int.Parse(Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
                Email = Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
                FirstName = Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName).Value;
                LastName = Claims.FirstOrDefault(x => x.Type == ClaimTypes.Surname).Value;
            }
        }
    }
}
