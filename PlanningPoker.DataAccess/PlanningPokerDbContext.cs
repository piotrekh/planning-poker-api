using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlanningPoker.DataAccess.Entities;
using PlanningPoker.DataAccess.EntitiesConfig.Identity;
using PlanningPoker.DataAccess.Extensions;

namespace PlanningPoker.DataAccess
{
    public class PlanningPokerDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public PlanningPokerDbContext(DbContextOptions<PlanningPokerDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.AddConfiguration<User, UserConfig>();
            builder.AddConfiguration<IdentityRole, IdentityRoleConfig>();
            builder.AddConfiguration<IdentityUserClaim<int>, IdentityUserClaimConfig>();
            builder.AddConfiguration<IdentityUserLogin<int>, IdentityUserLoginConfig>();
            builder.AddConfiguration<IdentityRoleClaim<int>, IdentityRoleClaimConfig>();
            builder.AddConfiguration<IdentityUserRole<int>, IdentityUserRoleConfig>();
            builder.AddConfiguration<IdentityUserToken<int>, IdentityUserTokenConfig>();            
        }
    }
}
