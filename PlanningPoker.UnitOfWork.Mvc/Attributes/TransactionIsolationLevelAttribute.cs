using System;
using System.Data;

namespace PlanningPoker.UnitOfWork.Mvc.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class TransactionIsolationLevelAttribute : Attribute
    {
        public IsolationLevel Level { get; set; }

        public TransactionIsolationLevelAttribute(IsolationLevel level)
        {
            Level = level;
        }
    }
}
