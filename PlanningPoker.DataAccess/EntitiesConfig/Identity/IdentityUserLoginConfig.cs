using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlanningPoker.DataAccess.EntitiesConfig.Identity
{
    internal class IdentityUserLoginConfig : EntityConfigurationBase<IdentityUserLogin<int>>
    {
        public override void Configure(EntityTypeBuilder<IdentityUserLogin<int>> builder)
        {
            builder.ToTable("AspNetUserLogin", "dbo");
            builder.Property(e => e.UserId).HasColumnName("AspNetUserId");
        }
    }
}
