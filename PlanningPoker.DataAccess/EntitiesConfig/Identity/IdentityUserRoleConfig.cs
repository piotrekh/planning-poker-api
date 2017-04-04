﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlanningPoker.DataAccess.EntitiesConfig.Identity
{
    internal class IdentityUserRoleConfig : EntityConfigurationBase<IdentityUserRole<int>>
    {
        public override void Configure(EntityTypeBuilder<IdentityUserRole<int>> builder)
        {
            builder.ToTable("AspNetUserRole", "dbo");
            builder.Property(e => e.UserId).HasColumnName("AspNetUserId");
            builder.Property(e => e.RoleId).HasColumnName("AspNetRoleId");
        }
    }
}
