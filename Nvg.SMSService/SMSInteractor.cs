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
            var eventMessage = new SendSMSEvent()
            {
                TenantID = smsInputs.TenantID,
                //FacilityID = smsInputs.FacilityID,
                TemplateName = "LOGIN_OTP_NOTIFICATION",
                Recipients = smsInputs.To,
                MessageParts = new Dictionary<string, string>()
                        {
                            {"MobileNumber", smsInputs.To },
                            {"OTP", "1234" },
                        },
            };
            _eventBus.Publish(eventMessage);
        }
    }
}
