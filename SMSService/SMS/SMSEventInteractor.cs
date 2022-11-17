using EventBus.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SMSService.DTOS;
using SMSService.Events;
using SMSService.SMSHistory;
using SMSService.SMSProvider;
using SMSService.SMSQuota;
using SMSService.SMSServiceProviders;
using System;

namespace SMSService.SMS
{
    public class SMSEventInteractor : ISMSEventInteractor
    {
        private readonly IEventBus _eventBus;
        private readonly IConfiguration _configuration;
        private readonly ISMSProviderInteractor _smsProviderInteractor;
        private readonly ISMSHistoryInteractor _smsHistoryInteractor;
        private readonly ISMSQuotaInteractor _smsQuotaInteractor;
        private readonly ISMSTemplateInteractor _smsTemplateInteractor;
        private readonly ILogger<SMSEventInteractor> _logger;
        private readonly ILogger<VariforrmProvider> _variforrmLogger;
        private readonly ILogger<KaleyraProvider> _kaleyraLogger;
        private SMSProviderConnectionString _smsProviderConnectionString;        

        // This constructor is used when SMSEventBusEnabled = true in appsettings.
        public SMSEventInteractor(IEventBus eventBus, IConfiguration configuration, ISMSProviderInteractor smsProviderInteractor,
            ISMSHistoryInteractor smsHistoryInteractor, ISMSQuotaInteractor smsQuotaInteractor, ISMSTemplateInteractor smsTemplateInteractor, ILogger<SMSEventInteractor> logger, 
            ILogger<VariforrmProvider> variforrmLogger, ILogger<KaleyraProvider> kaleyraLogger)
        {
            _eventBus = eventBus;
            _configuration = configuration;
            _smsProviderInteractor = smsProviderInteractor;
            _smsHistoryInteractor = smsHistoryInteractor;
            _smsQuotaInteractor = smsQuotaInteractor;
            _smsTemplateInteractor = smsTemplateInteractor;
            _logger = logger;
            _variforrmLogger = variforrmLogger;
            _kaleyraLogger = kaleyraLogger;
        }

        // This constructor is used when SMSEventBusEnabled = false in appsettings.
        public SMSEventInteractor(IConfiguration configuration, ISMSProviderInteractor providerInteractor,
            ISMSHistoryInteractor smsHistoryInteractor, ISMSQuotaInteractor smsQuotaInteractor, ISMSTemplateInteractor smsTemplateInteractor, ILogger<SMSEventInteractor> logger,
            ILogger<VariforrmProvider> variforrmLogger, ILogger<KaleyraProvider> kaleyraLogger)
        {
            _configuration = configuration;
            _smsProviderInteractor = providerInteractor;
            _smsHistoryInteractor = smsHistoryInteractor;
            _smsQuotaInteractor = smsQuotaInteractor;
            _smsTemplateInteractor = smsTemplateInteractor;
            _logger = logger;
            _variforrmLogger = variforrmLogger;
            _kaleyraLogger = kaleyraLogger;
        }

        public void SendSMS(SMSDto smsInputs)
        {
            _logger.LogInformation("SendSMS method.");
            _logger.LogDebug($"Channel Key: {smsInputs.ChannelKey} , Template Name: {smsInputs.TemplateName}, Variant: {smsInputs.Variant}.");
            //string user = !string.IsNullOrEmpty(smsInputs.Username) ? smsInputs.Username : smsInputs.Recipients;
            var recipientsList = smsInputs.Recipients.Split(new[] { "," },StringSplitOptions.RemoveEmptyEntries);
            _logger.LogDebug($"There are {recipientsList.Length} recipients.");
            var isEventBusEnabled = _configuration.GetValue<bool>("SMSEventBusEnabled");
            if (isEventBusEnabled)
            {
                // Use event bus to send sms.
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
            else
            {
                // Directly send sms to user via the SMS provider.
                var provider = _smsProviderInteractor.GetSMSProviderByChannel(smsInputs.ChannelKey).Result;
                if (provider != null)
                    _smsProviderConnectionString = new SMSProviderConnectionString(provider.Configuration);

                foreach (var recipient in recipientsList)
                {
                    var sms = new SMSSenderModel.SMS
                    {
                        Recipients = recipient,
                        Sender = smsInputs.Sender,
                        TemplateName = smsInputs.TemplateName,
                        Variant = smsInputs.Variant,
                        ChannelKey = smsInputs.ChannelKey,
                        MessageParts = smsInputs.MessageParts,
                        ProviderName = provider.Name,
                        Tag = smsInputs.Tag
                    };

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

                    ISMSProvider _smsProvider = GetSMSProvider(provider.Type);
                    var smsResponse = _smsProvider.SendSMS(recipient, message, sender).Result;
                  
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
                    //    credits = (long) smsResponse.SmsCost;
                    //}
                    if (smsResponse.SmsCost is null)
                    {
                        credits = 1;
                    }
                    else
                    {
                        credits = smsResponse.SmsCost.Value;
                    }
                
                    //  _logger.LogDebug("SMS Response Status: " + smsResponseStatus);
                    var smsObj = new SMSHistoryDto()
                    {
                        MessageSent = message,
                        Sender = sender,
                        Recipients = recipient,
                        TemplateName = sms.TemplateName,
                        TemplateVariant = sms.Variant,
                        ChannelKey = sms.ChannelKey,
                        ProviderName = sms.ProviderName,
                        Tags = sms.Tag,
                        SentOn = DateTime.UtcNow,
                        Status = smsResponse.StatusMessage,
                        Attempts = 1,
                        // CreditsCharged = credits
                        //ActualSMSCount =smsResponse[0].units,
                        ActualSMSCount = smsResponse.Unit,
                        ActualSMSCost = credits
                    };
                    _smsHistoryInteractor.AddSMSHistory(smsObj);

                    _smsQuotaInteractor.IncrementSMSQuota(smsInputs.ChannelKey, credits);
                }
            }
        }

        private ISMSProvider GetSMSProvider(string providerType)
        {
            _logger.LogInformation("GetSMSProvider method.");
            _logger.LogInformation("Getting appropriate Provider...");
            ISMSProvider emailProvider;

            switch (providerType.ToLower())
            {
                case "variforrm":
                    emailProvider = new VariforrmProvider(_smsProviderConnectionString, _variforrmLogger);
                    break;
                case "kaleyra":
                    emailProvider = new KaleyraProvider(_smsProviderConnectionString, _kaleyraLogger);
                    break;
                default:
                    emailProvider = new KaleyraProvider(_smsProviderConnectionString, _kaleyraLogger);
                    break;
            }
            _logger.LogInformation("Fetched appropriate Provider for " + providerType);
            return emailProvider;
        }
    }
}
