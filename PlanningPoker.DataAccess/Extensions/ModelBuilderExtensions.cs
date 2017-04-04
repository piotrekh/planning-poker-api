using Microsoft.EntityFrameworkCore;
using PlanningPoker.DataAccess.EntitiesConfig;
using System;

namespace PlanningPoker.DataAccess.Extensions
{
    internal static class ModelBuilderExtensions
    {
        public static void AddConfiguration<TEntity, TConfig>(this ModelBuilder modelBuilder)
            where TEntity : class
            where TConfig : EntityConfigurationBase<TEntity>
        {
            TConfig config = Activator.CreateInstance<TConfig>();
            modelBuilder.Entity<TEntity>(config.Configure);
        }
    }
}
