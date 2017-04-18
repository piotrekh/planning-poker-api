using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlanningPoker.DataAccess.Entities;

namespace PlanningPoker.DataAccess.EntitiesConfig
{
    internal class GameEstimateConfig : EntityConfigurationBase<GameEstimate>
    {
        public override void Configure(EntityTypeBuilder<GameEstimate> builder)
        {
            builder.ToTable("GameEstimate", "dbo");

            builder.HasKey(x => new { x.GameId, x.UserId });

            builder.HasOne(x => x.Game)
                   .WithMany(x => x.Estimates)
                   .HasForeignKey(x => x.GameId);

            builder.HasOne(x => x.User)
                   .WithMany(x => x.Estimates)
                   .HasForeignKey(x => x.UserId);
        }
    }
}
