using EventBus.Abstractions;
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

        public SMSEventInteractor(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void SendSMS(SMSDto smsInputs)
        {
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
            _eventBus.Publish(sendSMSEvent);
        }
    }
}
