using EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using Nvg.SMSService.DTOS;
using Nvg.SMSService.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.SMSService.SMS
{
    public class SMSEventInteractor : ISMSEventInteractor
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<SMSEventInteractor> _logger;

        public SMSEventInteractor(IEventBus eventBus, ILogger<SMSEventInteractor> logger)
        {
            _eventBus = eventBus;
            _logger = logger;
        }

        public void SendSMS(SMSDto smsInputs)
        {
            _logger.LogInformation("SendSMS method.");
            _logger.LogDebug($"Channel Key: {smsInputs.ChannelKey} , Template Name: {smsInputs.TemplateName}, Variant: {smsInputs.Variant}.");
            string user = !string.IsNullOrEmpty(smsInputs.Username) ? smsInputs.Username : smsInputs.Recipients;
            var sendSMSEvent = new SendSMSEvent();
            sendSMSEvent.ChannelKey = smsInputs.ChannelKey;
            sendSMSEvent.TemplateName = smsInputs.TemplateName;
            sendSMSEvent.Variant = smsInputs.Variant;
            sendSMSEvent.Sender = smsInputs.Sender;
            sendSMSEvent.Recipients = smsInputs.Recipients;
            sendSMSEvent.MessageParts = new Dictionary<string, string> {
                { "user", user},
                { "content", smsInputs.Content}
            };
            sendSMSEvent.Tag = smsInputs.Tag;
            _logger.LogInformation("Publishing SMS data.");
            _eventBus.Publish(sendSMSEvent);
        }
    }
}
