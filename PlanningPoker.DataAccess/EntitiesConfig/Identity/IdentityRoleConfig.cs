using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlanningPoker.DataAccess.EntitiesConfig.Identity
{
    internal class IdentityRoleConfig : EntityConfigurationBase<IdentityRole>
    {
        public override void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.ToTable(name: "AspNetRole", schema: "dbo");
        }
    }
}
