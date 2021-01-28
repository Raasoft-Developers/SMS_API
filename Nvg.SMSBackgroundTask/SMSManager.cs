using Nvg.SMSBackgroundTask.Models;
using Nvg.SMSBackgroundTask.SMSProvider;
using Nvg.SMSService;
using Nvg.SMSService.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.SMSBackgroundTask
{
    public class SMSManager
    {
        private readonly ISMSProvider _smsProvider;
        private readonly ISMSTemplateInteractor _smsTemplateInteractor;
        private readonly ISMSHistoryInteractor _smsHistoryInteractor;
        private readonly ISMSCounterInteractor _smsCounterInteractor;

        public SMSManager(ISMSProvider smsProvider, ISMSTemplateInteractor smsTemplateInteractor, ISMSHistoryInteractor smsHistoryInteractor, ISMSCounterInteractor smsCountInteractor)
        {
            _smsProvider = smsProvider;
            _smsTemplateInteractor = smsTemplateInteractor;
            _smsHistoryInteractor = smsHistoryInteractor;
            _smsCounterInteractor = smsCountInteractor;
        }
        public void SendSMS(SMS sms)
        {
            string message = sms.GetMessage(_smsTemplateInteractor);
            _smsProvider.SendSMS(sms.To, message, sms.Sender);

            var smsObj = new SMSHistoryDto()
            {
                CreatedOn = DateTime.UtcNow,
                Message = message,
                ToPhNumbers = sms.To,
                TenantID = sms.TenantID,
                FacilityID = sms.FacilityID,
                SentOn = DateTime.UtcNow,
                Status = "SENT"
            };
            _smsHistoryInteractor.Add(smsObj);

            _smsCounterInteractor.UpdateSMSCounter(sms.TenantID, sms.FacilityID);
        }
    }
}
