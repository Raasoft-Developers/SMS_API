using Autofac;
using Autofac.Extensions.DependencyInjection;
using EventBus.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nvg.SMSBackgroundTask.AutofacModules;
using Nvg.SMSBackgroundTask.EventHandler;
using Nvg.SMSBackgroundTask.Events;
using Nvg.SMSBackgroundTask.Extensions;
using Nvg.SMSBackgroundTask.Models;
using System;

namespace Nvg.SMSBackgroundTask
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

            var container = new ContainerBuilder();
            container.Populate(services);
            container.RegisterModule(new ApplicationModule());
            return new AutofacServiceProvider(container.Build());
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            /*app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
                {
                    Predicate = r => r.Name.Contains("self")
                });
            });*/
            ConfigureEventBus(app);
        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<SendSMSEvent, SendSMSEventHandler>();
        }
    }
}
