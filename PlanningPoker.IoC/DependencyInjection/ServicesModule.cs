using Autofac;
using PlanningPoker.Services;
using System.Reflection;

namespace PlanningPoker.IoC.DependencyInjection
{
    public class ServicesModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {            
            Assembly servicesAssembly = typeof(UserManagerService).GetTypeInfo().Assembly;

            builder.RegisterAssemblyTypes(servicesAssembly)
                .Where(x => x.Name.EndsWith("Service"))
                .AsImplementedInterfaces();
        }
    }
}
