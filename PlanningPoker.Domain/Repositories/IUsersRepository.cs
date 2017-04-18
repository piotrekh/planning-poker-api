using PlanningPoker.DataAccess.Entities;
using System.Threading.Tasks;

namespace PlanningPoker.Domain.Repositories
{
    public interface IUsersRepository
    {
        Task<bool> Create(User user, string password);

        Task<User> FindByEmail(string email);

        Task<bool> AddToRole(User user, string role);
    }
}
