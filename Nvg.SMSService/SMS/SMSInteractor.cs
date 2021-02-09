using EventBus.Abstractions;
using Nvg.SMSService.DTOS;
using Nvg.SMSService.Events;
using Nvg.SMSService.SMSChannel;
using Nvg.SMSService.SMSHistory;
using Nvg.SMSService.SMSPool;
using Nvg.SMSService.SMSProvider;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.SMSService.SMS
{
    public class SMSInteractor : ISMSInteractor
    {
        private readonly ISMSEventInteractor _smsEventInteractor;
        private readonly ISMSPoolInteractor _smsPoolInteractor;
        private readonly ISMSProviderInteractor _smsProviderInteractor;
        private readonly ISMSChannelInteractor _smsChannelInteractor;
        private readonly ISMSTemplateInteractor _smsTemplateInteractor;
        private readonly ISMSHistoryInteractor _smsHistoryInteractor;

        public SMSInteractor(ISMSEventInteractor smsEventInteractor, 
            ISMSPoolInteractor smsPoolInteractor, ISMSProviderInteractor smsProviderInteractor,
            ISMSChannelInteractor smsChannelInteractor, ISMSTemplateInteractor smsTemplateInteractor,
            ISMSHistoryInteractor smsHistoryInteractor)
        {
            _smsEventInteractor = smsEventInteractor;
            _smsPoolInteractor = smsPoolInteractor;
            _smsProviderInteractor = smsProviderInteractor;
            _smsChannelInteractor = smsChannelInteractor;
            _smsTemplateInteractor = smsTemplateInteractor;
            _smsHistoryInteractor = smsHistoryInteractor;
        }

        public SMSResponseDto<SMSPoolDto> AddSMSPool(SMSPoolDto poolInput)
        {
            var poolResponse = _smsPoolInteractor.AddSMSPool(poolInput);
            return poolResponse;
        }

        public SMSResponseDto<SMSProviderSettingsDto> AddSMSProvider(SMSProviderSettingsDto providerInput)
        {
            var providerResponse = _smsProviderInteractor.AddSMSProvider(providerInput);
            return providerResponse;
        }

        public SMSResponseDto<SMSChannelDto> AddSMSChannel(SMSChannelDto channelInput)
        {
            var channelResponse = _smsChannelInteractor.AddSMSChannel(channelInput);
            return channelResponse;
        }

        public SMSResponseDto<SMSTemplateDto> AddSMSTemplate(SMSTemplateDto templateInput)
        {
            var templateResponse = _smsTemplateInteractor.AddSMSTemplate(templateInput);
            return templateResponse;
        }

        public SMSResponseDto<SMSChannelDto> GetSMSChannelByKey(string channelKey)
        {
            var channelResponse = _smsChannelInteractor.GetSMSChannelByKey(channelKey);
            return channelResponse;
        }

        public SMSResponseDto<List<SMSProviderSettingsDto>> GetSMSProvidersByPool(string poolName, string providerName)
        {
            var poolResponse = _smsProviderInteractor.GetSMSProvidersByPool(poolName, providerName);
            return poolResponse;
        }

        public SMSResponseDto<List<SMSHistoryDto>> GetSMSHistoriesByTag(string channelKey, string tag)
        {
            var poolResponse = _smsHistoryInteractor.GetSMSHistoriesByTag(channelKey, tag);
            return poolResponse;
        }

        public SMSResponseDto<string> SendSMS(SMSDto smsInputs)
        {
            var response = new SMSResponseDto<string>();
            try
            {
                if (string.IsNullOrEmpty(smsInputs.ChannelKey))
                {
                    response.Status = false;
                    response.Message = "Channel key cannot be blank.";
                    return response;
                }
                else
                {
                    var channelExist = _smsChannelInteractor.CheckIfChannelExist(smsInputs.ChannelKey).Result;
                    if(!channelExist)
                    {
                        response.Status = channelExist;
                        response.Message = $"Invalid Channel key {smsInputs.ChannelKey}.";
                        return response;
                    }
                }
                if (string.IsNullOrEmpty(smsInputs.TemplateName))
                {
                    response.Status = false;
                    response.Message = "Template name cannot be blank.";
                    return response;
                }
                else
                {
                    var templateExist = _smsTemplateInteractor.CheckIfTemplateExist(smsInputs.ChannelKey, smsInputs.TemplateName).Result;
                    if (!templateExist)
                    {
                        response.Status = templateExist;
                        response.Message = $"No template found for template name {smsInputs.TemplateName} and channel key {smsInputs.ChannelKey}.";
                        return response;
                    }
                }
                _smsEventInteractor.SendSMS(smsInputs);
                response.Status = true;
                response.Message = $"SMS is sent successfully to {smsInputs.Recipients}.";
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }

    }
}
