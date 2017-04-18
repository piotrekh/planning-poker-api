using PlanningPoker.DataAccess.Entities;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PlanningPoker.Security.Services
{
    public interface IAuthorizationService
    {
        Task<User> Login(string email, string password);

        Task<IList<Claim>> GetClaims(User user);
    }
}
