using Autofac;
using EventBus.Abstractions;
using EventBus.Subscription;
using EventBusRabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nvg.SMSBackgroundTask.SMSProvider;
using Nvg.SMSService.SMSProvider;
using RabbitMQ.Client;
using Serilog;

namespace Nvg.SMSBackgroundTask.Extensions
{
    public static class CustomExtensionMethods
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            var subscriptionClientName = configuration["EventBusQueue"];
            if (configuration.GetValue<bool>("AzureServiceBusEnabled"))
            {
                //services.AddSingleton<IServiceBusPersisterConnection>(sp =>
                //{
                //    var logger = sp.GetRequiredService<ILogger<DefaultServiceBusPersisterConnection>>();

                //    var serviceBusConnectionString = configuration["EventBusConnection"];
                //    var serviceBusConnection = new ServiceBusConnectionStringBuilder(serviceBusConnectionString);

                //    return new DefaultServiceBusPersisterConnection(serviceBusConnection, logger);
                //});

                //services.AddSingleton<IEventBus, EventBusServiceBus>(sp =>
                //{
                //    var serviceBusPersisterConnection = sp.GetRequiredService<IServiceBusPersisterConnection>();
                //    var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                //    var logger = sp.GetRequiredService<ILogger<EventBusServiceBus>>();
                //    var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                //    return new EventBusServiceBus(serviceBusPersisterConnection, logger, eventBusSubcriptionsManager, subscriptionClientName, iLifetimeScope);
                //});
            }
            else
            {
                services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
                {
                    var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                    var factory = new ConnectionFactory()
                    {
                        Uri = new System.Uri(configuration["EventBusConnection"]),
                        DispatchConsumersAsync = true
                    };
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
                    return new EventBusRabbitMQ.EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope,
                        eventBusSubcriptionsManager, subscriptionClientName, retryCount);
                });
            }
            services.AddSingleton<ISubscriptionManager, SubscriptionManager>();
            return services;
        }

        public static ILoggingBuilder UseSerilog(this ILoggingBuilder builder, IConfiguration configuration)
        {
            var seqServerUrl = configuration["Serilog:SeqServerUrl"];
            var logstashUrl = configuration["Serilog:LogstashgUrl"];

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.WithProperty("ApplicationContext", Program.AppName)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Seq(string.IsNullOrWhiteSpace(seqServerUrl) ? "http://seq" : seqServerUrl)
                .WriteTo.Http(string.IsNullOrWhiteSpace(logstashUrl) ? "http://logstash:8080" : logstashUrl)
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            return builder;
        }

        public static void AddSMSBackgroundTask(this IServiceCollection services, string channelKey)
        {
            services.AddScoped<SMSManager>();
            services.AddScoped<ISMSProvider>(provider =>
            {
                var cs = provider.GetService<SMSProviderConnectionString>();

                var smsProviderService = provider.GetService<ISMSProviderInteractor>();
                var smsProviderConfiguration = smsProviderService.GetSMSProviderByChannel(channelKey)?.Result;
                var loggerKaleyra = provider.GetRequiredService<ILogger<KaleyraProvider>>();
                if(smsProviderConfiguration != null)
                {
                    if (smsProviderConfiguration.Type.ToLowerInvariant() == "kaleyra")
                        return new KaleyraProvider(cs, loggerKaleyra);
                    else if (smsProviderConfiguration.Type.ToLowerInvariant() == "variforrm")
                    {
                        var loggerVariforrm = provider.GetRequiredService<ILogger<VariforrmProvider>>();
                        return new VariforrmProvider(cs, loggerVariforrm);
                    }
                }
                /*
                if (cs.Provider.ToLowerInvariant() == "kaleyra")
                {
                    return new KaleyraProvider(cs);
                }*/
                return new KaleyraProvider(cs, loggerKaleyra);
            });
        }

        /*
        public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            var hcBuilder = services.AddHealthChecks();

            hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());

            hcBuilder.AddNpgSql(
                    configuration["ConnectionString"],
                    name: "SMSBackgroundTaskDB-check",
                    tags: new string[] { "smsdb" });

            if (configuration.GetValue<bool>("AzureServiceBusEnabled"))
            {
                hcBuilder.AddAzureServiceBusTopic(
                        configuration["EventBusConnection"],
                        topicName: "eshop_event_bus",
                        name: "orderingtask-servicebus-check",
                        tags: new string[] { "servicebus" });
            }
            else
            {
                hcBuilder.AddRabbitMQ(
                        $"amqp://{configuration["EventBusConnection"]}",
                        name: "orderingtask-rabbitmqbus-check",
                        tags: new string[] { "rabbitmqbus" });
            }
            return services;
        }*/
    }
}
