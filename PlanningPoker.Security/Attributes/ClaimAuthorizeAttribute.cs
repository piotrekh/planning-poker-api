using Microsoft.AspNetCore.Mvc;
using PlanningPoker.Security.Filters;
using System.Security.Claims;

namespace PlanningPoker.Security.Attributes
{
    public class ClaimAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimAuthorizeAttribute(string claim = null) : base(typeof(ClaimAuthorizeFilter))
        {
            Claim c = null;

            if (!string.IsNullOrEmpty(claim))
                c = new Claim(claim, "true");

            Arguments = new object[] { c };
        }
    }
}
