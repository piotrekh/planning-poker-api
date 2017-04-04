﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlanningPoker.DataAccess.EntitiesConfig.Identity
{
    internal class IdentityUserClaimConfig : EntityConfigurationBase<IdentityUserClaim<int>>
    {
        public override void Configure(EntityTypeBuilder<IdentityUserClaim<int>> builder)
        {
            builder.ToTable("AspNetUserClaim", "dbo");
            builder.Property(e => e.UserId).HasColumnName("AspNetUserId");
        }
    }
}
