using EventBus.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Nvg.SMSBackgroundTask.Events;
using Nvg.SMSBackgroundTask.Extensions;
using Nvg.SMSBackgroundTask.Models;
using Nvg.SMSService;
using Nvg.SMSService.Data.Models;
using Nvg.SMSService.SMSProvider;
using Nvg.SMSService.SMSQuota;
using Nvg.SMSService.SMSServiceProviders;
using System;
using System.Threading.Tasks;

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
                try { 
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
                var res = smsManager.SendSMS(sms);
                _logger.LogDebug($"SMS Sent: {JsonConvert.SerializeObject(res) }");
                    /*}
                    else
                        _logger.LogDebug($"SMS settings are not Enabled or you have crossed the MonthlySMSQuota");
                    */
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error while sending SMS: {ex}");
                }
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
                _logger.LogDebug($"smsProviderConfiguration : {smsProviderConfiguration}");
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
