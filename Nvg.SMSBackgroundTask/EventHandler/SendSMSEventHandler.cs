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
                using IServiceScope scope = GetScope(@event.TenantID, @event.FacilityID);
                var smsManager = scope.ServiceProvider.GetService<SMSManager>();

                var smsCounterInteractor = scope.ServiceProvider.GetService<ISMSCounterInteractor>();
                var smsCount = smsCounterInteractor.GetSMSCounter(@event.TenantID, @event.FacilityID);

                //var smsSettings = scope.ServiceProvider.GetService<SMSSettings>();
                //_logger.LogDebug($"Enable SMS Info: {smsSettings}");

                /*if (smsSettings.EnableSMS && Convert.ToInt32(smsCount) <= smsSettings.MonthlySMSQuota)
                {*/
                var sms = new SMS
                {
                    FacilityID = @event.FacilityID,
                    MessageParts = @event.MessageParts,
                    Sender = @event.Sender,
                    TemplateName = @event.TemplateName,
                    TenantID = @event.TenantID,
                    To = @event.Recipients
                };
                smsManager.SendSMS(sms);
                /*}
                else
                    _logger.LogDebug($"SMS settings are not Enabled or you have crossed the MonthlySMSQuota");
                */
            }
            return Task.FromResult(true);
        }

        private IServiceScope GetScope(string tenantID, string facilityID)
        {
            var serviceProvider = new ServiceCollection();
            var configuration = Program.GetConfiguration();
            serviceProvider.AddScoped(_ => configuration);
            serviceProvider.AddLogging();
            serviceProvider.AddSMSBackgroundTask();
            serviceProvider.AddSMSServices(Program.AppName);

            serviceProvider.AddScoped<SMSDBInfo>(provider =>
            {
                string microservice = Program.AppName;
                string connectionString = configuration.GetSection("ConnectionString")?.Value;
                return new SMSDBInfo(connectionString);
            });

            serviceProvider.AddScoped<SMSProviderConnectionString>(provider =>
            {
                string smsGatewayProvider = configuration.GetSection("SMSGatewayProvider")?.Value;
                return new SMSProviderConnectionString(smsGatewayProvider);
            });

            var scope = serviceProvider.BuildServiceProvider().CreateScope();
            return scope;
        }
    }
}
