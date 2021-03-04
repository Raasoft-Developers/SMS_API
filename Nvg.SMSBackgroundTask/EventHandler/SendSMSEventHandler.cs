using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBus.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nvg.SMSBackgroundTask.Events;
using Nvg.SMSService;
using Nvg.SMSBackgroundTask.Models;
using Nvg.SMSBackgroundTask.Extensions;
using Nvg.SMSService.Data.Models;
using Nvg.SMSBackgroundTask.SMSProvider;
using Nvg.SMSService.SMSQuota;
using Nvg.SMSService.SMSProvider;

namespace Nvg.SMSBackgroundTask.EventHandler
{
    public class SendSMSEventHandler : IIntegrationEventHandler<SendSMSEvent>
    {
        private readonly ILogger<SendSMSEventHandler> _logger;

        public SendSMSEventHandler(ILogger<SendSMSEventHandler> logger)
        {
            _logger = logger;
        }
        public async Task<dynamic> Handle(SendSMSEvent @event)
        {
            _logger.LogDebug($"Subscriber received a SendSMSEvent notification.");
            if (@event.Id != Guid.Empty)
            {
                using IServiceScope scope = GetScope(@event.ChannelKey);
                var smsManager = scope.ServiceProvider.GetService<SMSManager>();

                var smsQuotaInteractor = scope.ServiceProvider.GetService<ISMSQuotaInteractor>();
                var smsQuota = smsQuotaInteractor.GetSMSQuota(@event.ChannelKey)?.Result;

                var smsProviderService = scope.ServiceProvider.GetService<ISMSProviderInteractor>();
                var providerName = smsProviderService.GetSMSProviderByChannel(@event.ChannelKey)?.Result?.Name;

                //var smsSettings = scope.ServiceProvider.GetService<SMSSettings>();
                //_logger.LogDebug($"Enable SMS Info: {smsSettings}");

                /*if (smsSettings.EnableSMS && Convert.ToInt32(smsCount) <= smsSettings.MonthlySMSQuota)
                {*/
                var sms = new SMS
                {
                    Sender = @event.Sender,
                    Recipients = @event.Recipients,
                    ChannelKey = @event.ChannelKey,
                    ProviderName = providerName,
                    Variant = @event.Variant,
                    MessageParts = @event.MessageParts,
                    TemplateName = @event.TemplateName,
                    Tag = @event.Tag
                };
                smsManager.SendSMS(sms);
                /*}
                else
                    _logger.LogDebug($"SMS settings are not Enabled or you have crossed the MonthlySMSQuota");
                */
            }
            return Task.FromResult(true);
        }

        private IServiceScope GetScope(string channelKey)
        {
            var services = new ServiceCollection();
            var configuration = Program.GetConfiguration();
            string databaseProvider = configuration.GetSection("DatabaseProvider")?.Value;
            services.AddScoped(_ => configuration);
            services.AddLogging();
            services.AddSMSBackgroundTask(channelKey);
            services.AddSMSServices(Program.AppName, databaseProvider);

            services.AddScoped<SMSDBInfo>(provider =>
            {
                string microservice = Program.AppName;
                string connectionString = configuration.GetSection("ConnectionString")?.Value;
                string databaseProvider = configuration.GetSection("DatabaseProvider")?.Value;
                return new SMSDBInfo(connectionString, databaseProvider);
            });

            services.AddScoped<SMSProviderConnectionString>(provider =>
            {
                var smsProviderService = provider.GetService<ISMSProviderInteractor>();
                var smsProviderConfiguration = smsProviderService.GetSMSProviderByChannel(channelKey)?.Result?.Configuration;
                /*
                if(string.IsNullOrEmpty(smsProviderConfiguration))
                    smsProviderConfiguration = configuration.GetSection("SMSGatewayProvider")?.Value;
                */
                return new SMSProviderConnectionString(smsProviderConfiguration);
            });

            var scope = services.BuildServiceProvider().CreateScope();
            return scope;
        }
    }
}
