using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlanningPoker.DataAccess.Entities;
using PlanningPoker.DataAccess.EntitiesConfig;
using PlanningPoker.DataAccess.EntitiesConfig.Identity;
using PlanningPoker.DataAccess.Extensions;

namespace PlanningPoker.DataAccess
{
    public class PlanningPokerDbContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<Session> Sessions { get; set; }
        public DbSet<SessionPlayer> SessionPlayers { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameEstimate> GameEstimates { get; set; }

        public PlanningPokerDbContext(DbContextOptions<PlanningPokerDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {            
            //configure identity entities
            builder.AddConfiguration<User, UserConfig>();
            builder.AddConfiguration<Role, RoleConfig>();
            builder.AddConfiguration<IdentityUserClaim<int>, IdentityUserClaimConfig>();
            builder.AddConfiguration<IdentityUserLogin<int>, IdentityUserLoginConfig>();
            builder.AddConfiguration<IdentityRoleClaim<int>, IdentityRoleClaimConfig>();
            builder.AddConfiguration<IdentityUserRole<int>, IdentityUserRoleConfig>();
            builder.AddConfiguration<IdentityUserToken<int>, IdentityUserTokenConfig>();

            //configure game-related entities
            builder.AddConfiguration<Session, SessionConfig>();
            builder.AddConfiguration<SessionPlayer, SessionPlayerConfig>();
            builder.AddConfiguration<Game, GameConfig>();
            builder.AddConfiguration<GameEstimate, GameEstimateConfig>();

            //base.OnModelCreating(builder);
        }
    }
}
