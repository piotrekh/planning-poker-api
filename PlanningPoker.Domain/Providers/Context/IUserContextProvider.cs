using System.Collections.Generic;
using System.Security.Claims;

namespace PlanningPoker.Domain.Providers.Context
{
    public interface IUserContextProvider
    {
        bool IsAuthenticated { get; }

        int? Id { get; }

        string Email { get; }

        List<Claim> Claims { get; }

        string FirstName { get; }

        string LastName { get; }
    }
}
