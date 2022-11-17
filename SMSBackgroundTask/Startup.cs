using Autofac;
using Autofac.Extensions.DependencyInjection;
using EventBus.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SMSBackgroundTask.AutofacModules;
using SMSBackgroundTask.EventHandler;
using SMSBackgroundTask.Events;
using SMSBackgroundTask.Extensions;
using SMSBackgroundTask.Models;
using System;

namespace SMSBackgroundTask
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services/*.AddCustomHealthCheck(Configuration)*/
                .Configure<BackgroundTaskSettings>(Configuration)
                .AddOptions()
                .AddEventBus(Configuration);
            if (Configuration["logsService"] == "Azure")
            {
                // The following line enables Application Insights telemetry collection.
                services.AddApplicationInsightsTelemetry();
            }
            var container = new ContainerBuilder();
            container.Populate(services);
            container.RegisterModule(new ApplicationModule());
            return new AutofacServiceProvider(container.Build());
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            
            ConfigureEventBus(app);
        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<SendSMSEvent, SendSMSEventHandler>();
        }
    }
}
