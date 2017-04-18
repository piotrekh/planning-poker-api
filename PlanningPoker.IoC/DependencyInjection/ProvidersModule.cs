using Autofac;
using PlanningPoker.Providers.Transactions;
using System.Reflection;

namespace PlanningPoker.IoC.DependencyInjection
{
    public class ProvidersModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            Assembly servicesAssembly = typeof(TransactionProvider).GetTypeInfo().Assembly;

            builder.RegisterAssemblyTypes(servicesAssembly)
                .Where(x => x.Name.EndsWith("Provider"))
                .AsImplementedInterfaces();
        }
    }
}
