using Microsoft.Extensions.Logging;
using SMSBackgroundTask.Models;
//using SMSBackgroundTask.SMSProvider;
using SMSService;
using SMSService.DTOS;
using SMSService.SMSHistory;
using SMSService.SMSQuota;
using SMSService.SMSServiceProviders;
using System;
using System.Threading.Tasks;

namespace SMSBackgroundTask
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

        public async Task<SmsProviderResponse> SendSMS(SMS sms)
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
            var smsResponse = await _smsProvider.SendSMS(sms.Recipients, message, sender);
            long credits;

            if (smsResponse.SmsCost is null)
            {
                credits = 1;
            }
            else
            {
                credits = smsResponse.SmsCost.Value;
            }

            if (smsResponse.Status != "Error")
            {
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
            }
            return smsResponse;
        }
    }
}
