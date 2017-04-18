using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlanningPoker.DataAccess.Entities;

namespace PlanningPoker.DataAccess.EntitiesConfig
{
    internal class GameConfig : EntityConfigurationBase<Game>
    {
        public override void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.ToTable("Game", "dbo");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Session)
                   .WithMany(x => x.Games)
                   .HasForeignKey(x => x.SessionId);
        }
    }
}
