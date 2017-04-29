using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PlanningPoker.Api.Models;
using PlanningPoker.Domain.Exceptions;

namespace PlanningPoker.Api.Filters
{
    public class ExceptionResultFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        { 
            if (context.Exception is ApplicationException)
            {
                var exception = context.Exception as ApplicationException;
                var result = new ApplicationExceptionResult(exception.Reason.ToString());

                context.Result = new BadRequestObjectResult(result);
            }

            base.OnException(context);
        }
    }
}
