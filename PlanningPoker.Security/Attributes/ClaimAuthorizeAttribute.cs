using Microsoft.AspNetCore.Mvc;
using PlanningPoker.Security.Filters;
using System;
using System.Security.Claims;

namespace PlanningPoker.Security.Attributes
{
    public class ClaimAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimAuthorizeAttribute(string claim) : base(typeof(ClaimAuthorizeFilter))
        {
            if (string.IsNullOrEmpty(claim))
                throw new ArgumentNullException(nameof(claim));

            Arguments = new object[] { new Claim(claim, "true") };
        }
    }
}
