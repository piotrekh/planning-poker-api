using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlanningPoker.DataAccess;
using PlanningPoker.DataAccess.Entities;
using PlanningPoker.IoC.DependencyInjection;
using System;

namespace PlanningPoker.IoC
{
    public class IoCSetup : IIoCSetup
    {
        public IContainer Container { get; private set; }

        public IServiceProvider ServiceProvider { get; private set; }        

        public void RegisterServices(IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddDbContext<PlanningPokerDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("PlanningPokerDb")));
            
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<PlanningPokerDbContext, int>()
                .AddDefaultTokenProviders();

            var builder = new ContainerBuilder();
            
            builder.RegisterModule<ServicesModule>();
            builder.RegisterModule<RepositoriesModule>();
            builder.RegisterModule<ProvidersModule>();

            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().InstancePerLifetimeScope();

            builder.Populate(services);

            Container = builder.Build();
            ServiceProvider = new AutofacServiceProvider(Container);
        }

        public void Dispose()
        {
            Container.Dispose();
            Container = null;
            ServiceProvider = null;
        }
    }
}
