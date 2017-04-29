using System;

namespace PlanningPoker.Domain.Exceptions
{
    public class ApplicationException : Exception
    {
        public Enum Reason { get; set; }

        public ApplicationException(Enum reason)
        {
            Reason = reason;
        }
    }
}
