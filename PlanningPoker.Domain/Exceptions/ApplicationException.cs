using System;

namespace PlanningPoker.Domain.Exceptions
{
    public class ApplicationException : Exception
    {
        public string Reason { get; set; }

        public ApplicationException(string reason)
        {
            Reason = reason;
        }
    }
}
