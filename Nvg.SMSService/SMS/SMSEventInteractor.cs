using EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using Nvg.SMSService.DTOS;
using Nvg.SMSService.Events;
using System;

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
            //string user = !string.IsNullOrEmpty(smsInputs.Username) ? smsInputs.Username : smsInputs.Recipients;
            var recipientsList = smsInputs.Recipients.Split(new[] { "," },StringSplitOptions.RemoveEmptyEntries);
            _logger.LogDebug($"There are {recipientsList.Length} recipients.");
            foreach (var recipient in recipientsList)
            {
                var sendSMSEvent = new SendSMSEvent();
                sendSMSEvent.ChannelKey = smsInputs.ChannelKey;
                sendSMSEvent.TemplateName = smsInputs.TemplateName;
                sendSMSEvent.Variant = smsInputs.Variant;
                sendSMSEvent.Sender = smsInputs.Sender;
                sendSMSEvent.Recipients = recipient.Trim();
                sendSMSEvent.MessageParts = smsInputs.MessageParts;
                sendSMSEvent.Tag = smsInputs.Tag;
                _logger.LogInformation("Publishing SMS data.");
                _eventBus.Publish(sendSMSEvent);
            }
        }
    }
}
