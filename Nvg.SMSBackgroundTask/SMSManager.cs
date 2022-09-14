using Microsoft.Extensions.Logging;
using Nvg.SMSBackgroundTask.Models;
//using Nvg.SMSBackgroundTask.SMSProvider;
using Nvg.SMSService;
using Nvg.SMSService.DTOS;
using Nvg.SMSService.SMSHistory;
using Nvg.SMSService.SMSQuota;
using Nvg.SMSService.SMSServiceProviders;
using System;

namespace Nvg.SMSBackgroundTask
{
    public class SMSManager
    {
        private readonly ISMSProvider _smsProvider;
        private readonly ISMSTemplateInteractor _smsTemplateInteractor;
        private readonly ISMSHistoryInteractor _smsHistoryInteractor;
        private readonly ISMSQuotaInteractor _smsQuotaInteractor;
        private readonly SMSProviderConnectionString _smsProviderConnectionString;
        private readonly ILogger<SMSManager> _logger;     

        public SMSManager(ISMSProvider smsProvider, ISMSTemplateInteractor smsTemplateInteractor,
            ISMSHistoryInteractor smsHistoryInteractor, ISMSQuotaInteractor smsQuotaInteractor,
            SMSProviderConnectionString smsProviderConnectionString, ILogger<SMSManager> logger)
        {
            _smsProvider = smsProvider;
            _smsTemplateInteractor = smsTemplateInteractor;
            _smsHistoryInteractor = smsHistoryInteractor;
            _smsQuotaInteractor = smsQuotaInteractor;
            _smsProviderConnectionString = smsProviderConnectionString;
            _logger = logger;
        }

        public void SendSMS(SMS sms)
        {
            try
            {
                _logger.LogInformation("SendSMS method");
                string sender = string.Empty;
                string message = sms.GetMessage(_smsTemplateInteractor);
                _logger.LogDebug("Message: " + message);
                // If external application didnot send the sender value, get it from template.
                if (string.IsNullOrEmpty(sms.Sender))
                    sender = sms.GetSender(_smsTemplateInteractor);
                else
                    sender = sms.Sender;

                if (string.IsNullOrEmpty(sender))
                    sender = _smsProviderConnectionString.Fields["sender"];
                _logger.LogDebug("sender: " + sender);
                var smsResponse = _smsProvider.SendSMS(sms.Recipients, message, sender).Result;

                long credits;
                //if (smsResponse is string)
                //{
                //    smsStatus = smsResponse.StatusMessage;
                //    credits = smsResponse.Equals("SENT") ? 1 : 0;
                //}
                //else
                //{
                //    smsStatus = "SENT";
                //    //  credits = smsResponse[0].charges.Value;
                //    credits = (long)smsResponse.SmsCost;
                //}

                if (smsResponse.SmsCost is null)
                {
                    credits = 1;
                }
                else
                {
                    credits = smsResponse.SmsCost.Value;
                }

                //_logger.LogDebug("SMS Response Status: " + smsResponseStatus);
                var smsObj = new SMSHistoryDto()
                {
                    MessageSent = message,
                    Sender = sender,
                    Recipients = sms.Recipients,
                    TemplateName = sms.TemplateName,
                    TemplateVariant = sms.Variant,
                    ChannelKey = sms.ChannelKey,
                    ProviderName = sms.ProviderName,
                    Tags = sms.Tag,
                    SentOn = DateTime.UtcNow,
                    Status = smsResponse.StatusMessage,
                    Attempts = 1,
                    ActualSMSCount = smsResponse.Unit,
                    ActualSMSCost = credits
                };
                _smsHistoryInteractor.AddSMSHistory(smsObj);

                _smsQuotaInteractor.IncrementSMSQuota(sms.ChannelKey, credits);
            }catch(Exception ex)
            {
                _logger.LogError($"Error while sending SMS: {ex}");
            }
        }
    }
}
