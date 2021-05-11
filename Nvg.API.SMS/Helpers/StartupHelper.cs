using Autofac;
using EventBus.Abstractions;
using EventBus.Subscription;
using EventBusRabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nvg.SMSService;
using Nvg.SMSService.Data.Models;
using RabbitMQ.Client;
using System;

namespace Nvg.API.SMS.Helpers
{
    public static class StartupHelper
    {
        public static void AddSMSService(this IServiceCollection services, string microservice, IConfiguration configuration)
        {
            string databaseProviderMain = configuration.GetSection("DatabaseProvider")?.Value;
            services.AddScoped<SMSDBInfo>(provider =>
            {
                var logger = provider.GetService<ILogger<SMSDBInfo>>();
                logger.LogDebug($"RESOLVING SMSDBInfo");
                string connectionString = configuration.GetSection("ConnectionString")?.Value;
                string databaseProvider = configuration.GetSection("DatabaseProvider")?.Value;
                return new SMSDBInfo(connectionString, databaseProvider);
            });
            SMSServiceExtension.AddSMSServices(services, microservice, databaseProviderMain);
        }

        public static void RegisterEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            string subscriptionClientName = configuration.GetValue<string>("EventBusQueue", string.Empty);
            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var factory = new ConnectionFactory();
                factory.Uri = new Uri(configuration["EventBusConnection"]);
                factory.DispatchConsumersAsync = true;
                var retryCount = 5;
                if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(configuration["EventBusRetryCount"]);
                }
                return new DefaultRabbitMQPersistentConnection(factory, retryCount);
            });

            services.AddSingleton<IEventBus, EventBusRabbitMQ.EventBusRabbitMQ>(sp =>
            {
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ.EventBusRabbitMQ>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<ISubscriptionManager>();

                var retryCount = 5;
                if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(configuration["EventBusRetryCount"]);
                }
                return new EventBusRabbitMQ.EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
            });
            services.AddSingleton<ISubscriptionManager, SubscriptionManager>();
        }
    }
}
