using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlanningPoker.DataAccess.EntitiesConfig.Identity
{
    internal class IdentityRoleClaimConfig : EntityConfigurationBase<IdentityRoleClaim<int>>
    {
        public override void Configure(EntityTypeBuilder<IdentityRoleClaim<int>> builder)
        {
            builder.ToTable("AspNetRoleClaim", "dbo");
            builder.Property(e => e.RoleId).HasColumnName("AspNetRoleId");
        }
    }
}
