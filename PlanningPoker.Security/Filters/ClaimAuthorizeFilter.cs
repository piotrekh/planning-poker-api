using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PlanningPoker.Security.Filters
{
    public class ClaimAuthorizeFilter : IAsyncActionFilter
    {
        readonly Claim _claim;

        public ClaimAuthorizeFilter(Claim claim)
        {
            _claim = claim;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == _claim.Type && c.Value == _claim.Value);
            if (!hasClaim)
            {
                context.Result = new UnauthorizedResult();
            }
            else
            {
                await next();
            }
        }
    }
}
