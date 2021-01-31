using EventBus.Abstractions;
using Nvg.SMSService.DTOS;
using Nvg.SMSService.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.SMSService
{
    public class SMSInteractor : ISMSInteractor
    {
        private readonly IEventBus _eventBus;

        public SMSInteractor(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void SendSMS(SMSDto smsInputs)
        {
            string user = (!string.IsNullOrEmpty(smsInputs.Username) ? smsInputs.Username : smsInputs.To);

            var sendSMSEvent = new SendSMSEvent();
            sendSMSEvent.TemplateName = smsInputs.Template;
            sendSMSEvent.Recipients = smsInputs.To;
            sendSMSEvent.MessageParts = new Dictionary<string, string> {
                { "User", user},
                { "Content", smsInputs.Content}
            };
            sendSMSEvent.TenantID = smsInputs.TenantID;
            sendSMSEvent.FacilityID = smsInputs.FacilityID;
            _eventBus.Publish(sendSMSEvent);
        }
    }
}
