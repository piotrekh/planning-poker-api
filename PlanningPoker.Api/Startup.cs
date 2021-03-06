﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Converters;
using PlanningPoker.Api.Filters;
using PlanningPoker.IoC;
using PlanningPoker.Security.DependencyInjection;
using PlanningPoker.UnitOfWork.Mvc.Filters;
using System;

namespace PlanningPoker.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            IoC = new IoCSetup();
        }

        public IConfigurationRoot Configuration { get; }

        public IIoCSetup IoC { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddCors();
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(UnitOfWorkTransactionFilter));
                options.Filters.Add(new ExceptionResultFilter());
            })
            .AddJsonOptions(options =>
            {
                //convert all enums in models to strings
                options.SerializerSettings.Converters.Add(new StringEnumConverter(false));
            });
            services.AddSecurity(Configuration);
            
            IoC.RegisterServices(services, Configuration);

            return IoC.ServiceProvider;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IApplicationLifetime appLifetime, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors(builder => builder.AllowAnyOrigin()
                                          .AllowAnyMethod()
                                          .AllowAnyHeader()
                                          .AllowCredentials());
            app.UseSecurity();
            app.UseMvc();            

            appLifetime.ApplicationStopped.Register(() => IoC.Dispose());
        }
    }
}
