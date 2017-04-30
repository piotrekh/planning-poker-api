using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlanningPoker.UnitOfWork.Abstractions;

namespace PlanningPoker.UnitOfWork
{
    public static class IServiceCollectionExtensions
    {
        public static void AddUnitOfWork<T>(this IServiceCollection services) where T : DbContext
        {
            //configure DbContext to be resolved to DbContext implementation used in the application
            services.AddScoped<DbContext>(x => x.GetService<T>());

            //register unit of work services
            services.AddScoped<ITransactionProvider, TransactionProvider>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
