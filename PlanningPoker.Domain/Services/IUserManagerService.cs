using PlanningPoker.Domain.Enums;
using PlanningPoker.Domain.Models.Users;
using System.Threading.Tasks;

namespace PlanningPoker.Domain.Services
{
    public interface IUserManagerService
    {
        Task<CreateUserResult> CreateUser(CreateUser data);
    }
}
