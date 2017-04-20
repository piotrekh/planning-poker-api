namespace PlanningPoker.Api.Models
{
    public class ApplicationExceptionResult
    {
        public string Reason { get; set; }

        public ApplicationExceptionResult(string reason)
        {
            Reason = reason;
        }
    }

    public class ApplicationExceptionResult<T> : ApplicationExceptionResult
    {
        public T Data { get; set; }

        public ApplicationExceptionResult(string reason, T data) : base(reason)
        {
            Data = data;
        }
    }
}
