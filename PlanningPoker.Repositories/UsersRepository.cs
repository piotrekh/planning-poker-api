using Microsoft.AspNetCore.Identity;
using PlanningPoker.DataAccess.Entities;
using PlanningPoker.Domain.Repositories;
using System.Threading.Tasks;

namespace PlanningPoker.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public UsersRepository(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> Create(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }

        public async Task<User> FindByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<bool> AddToRole(User user, string role)
        {
            var result = await _userManager.AddToRoleAsync(user, role);
            return result.Succeeded;
        }
    }
}
