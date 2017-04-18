using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlanningPoker.DataAccess.Entities;

namespace PlanningPoker.DataAccess.EntitiesConfig
{
    internal class SessionPlayerConfig : EntityConfigurationBase<SessionPlayer>
    {
        public override void Configure(EntityTypeBuilder<SessionPlayer> builder)
        {
            builder.ToTable("SessionPlayer", "dbo");

            builder.HasKey(x => new { x.SessionId, x.UserId });

            builder.HasOne(x => x.Session)
                   .WithMany(x => x.Players)
                   .HasForeignKey(x => x.SessionId);

            builder.HasOne(x => x.User)
                   .WithMany(x => x.Sessions)
                   .HasForeignKey(x => x.UserId);
        }
    }
}
