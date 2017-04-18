using Microsoft.AspNetCore.Identity;
using PlanningPoker.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PlanningPoker.Security.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public AuthorizationService(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<User> Login(string email, string password)
        {
            User user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                bool passwordVerified = await _userManager.CheckPasswordAsync(user, password);
                if (!passwordVerified)
                    user = null;
            }

            return user;
        }

        public async Task<IList<Claim>> GetClaims(User user)
        {
            IList<string> roleNames = await _userManager.GetRolesAsync(user);
            string roleName = roleNames.First();
            Role role = await _roleManager.FindByNameAsync(roleName);

            return await _roleManager.GetClaimsAsync(role);
        }
    }
}
