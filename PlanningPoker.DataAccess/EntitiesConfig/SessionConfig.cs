using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlanningPoker.DataAccess.Entities;

namespace PlanningPoker.DataAccess.EntitiesConfig
{
    internal class SessionConfig : EntityConfigurationBase<Session>
    {
        public override void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable("Session", "dbo");
            builder.HasOne(x => x.Moderator).WithOne().HasForeignKey<Session>(x => x.ModeratorId);
        }
    }
}
