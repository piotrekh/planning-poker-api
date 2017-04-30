using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using PlanningPoker.UnitOfWork.Abstractions;
using PlanningPoker.UnitOfWork.Mvc.Attributes;
using System;
using System.Linq;
using System.Reflection;

namespace PlanningPoker.UnitOfWork.Mvc.Filters
{
    public class UnitOfWorkTransactionFilter : IActionFilter
    {
        private IUnitOfWork _uow;

        public UnitOfWorkTransactionFilter(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //check if transaction isolation level was specified for the request and if so, set it in Unit of Work
            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var isolationLevelAttribute = descriptor.MethodInfo.GetCustomAttributes<TransactionIsolationLevelAttribute>(true).FirstOrDefault();
            if (isolationLevelAttribute != null)            
                _uow.SetIsolationLevel(isolationLevelAttribute.Level);                        
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
                _uow.RollbackTransaction();
            else
            {
                try
                {
                    _uow.CommitTransaction();
                }
                catch (Exception ex)
                {
                    _uow.RollbackTransaction();
                    context.Exception = ex;
                    context.Result = null;
                }
            }
        }        
    }
}
