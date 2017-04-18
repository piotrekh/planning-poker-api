using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace PlanningPoker.IoC
{
    public interface IIoCSetup : IDisposable
    {
        IContainer Container { get; }

        IServiceProvider ServiceProvider { get; }

        void RegisterServices(IServiceCollection services, IConfigurationRoot configuration);
    }
}
