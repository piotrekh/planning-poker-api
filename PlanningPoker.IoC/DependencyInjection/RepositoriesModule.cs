using Autofac;
using PlanningPoker.IoC.Extensions;
using PlanningPoker.Repositories;
using System.Reflection;

namespace PlanningPoker.IoC.DependencyInjection
{
    public class RepositoriesModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            Assembly servicesAssembly = typeof(UsersRepository).GetTypeInfo().Assembly;

            builder.RegisterAssemblyTypes(servicesAssembly)
                .Where(x => x.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerAspNetCoreRequest();
        }
    }
}
