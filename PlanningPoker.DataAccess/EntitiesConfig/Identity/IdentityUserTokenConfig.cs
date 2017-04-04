using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlanningPoker.DataAccess.EntitiesConfig.Identity
{
    internal class IdentityUserTokenConfig : EntityConfigurationBase<IdentityUserToken<int>>
    {
        public override void Configure(EntityTypeBuilder<IdentityUserToken<int>> builder)
        {
            builder.ToTable("AspNetUserToken", "dbo");
            builder.Property(e => e.UserId).HasColumnName("AspNetUserId");
        }
    }
}
