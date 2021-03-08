using EventBus.Abstractions;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<SMSInteractor> _logger;

        public SMSInteractor(ISMSEventInteractor smsEventInteractor,
            ISMSPoolInteractor smsPoolInteractor, ISMSProviderInteractor smsProviderInteractor,
            ISMSChannelInteractor smsChannelInteractor, ISMSTemplateInteractor smsTemplateInteractor,
            ISMSHistoryInteractor smsHistoryInteractor, ILogger<SMSInteractor> logger)
        {
            _smsEventInteractor = smsEventInteractor;
            _smsPoolInteractor = smsPoolInteractor;
            _smsProviderInteractor = smsProviderInteractor;
            _smsChannelInteractor = smsChannelInteractor;
            _smsTemplateInteractor = smsTemplateInteractor;
            _smsHistoryInteractor = smsHistoryInteractor;
            _logger = logger;
        }

        public SMSResponseDto<SMSPoolDto> AddSMSPool(SMSPoolDto poolInput)
        {
            _logger.LogInformation("AddSMSPool interactor method.");
            SMSResponseDto<SMSPoolDto> poolResponse = new SMSResponseDto<SMSPoolDto>();
            try
            {
                poolResponse = _smsPoolInteractor.AddSMSPool(poolInput);
                _logger.LogDebug("Status: " + poolResponse.Status + ",Message: " + poolResponse.Message);
                return poolResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred in SMS Interactor while adding SMS pool: ", ex.Message);
                poolResponse.Message = "Error occurred while adding SMS pool: " + ex.Message;
                poolResponse.Status = false;
                return poolResponse;
            }
        }

        public SMSResponseDto<SMSProviderSettingsDto> AddSMSProvider(SMSProviderSettingsDto providerInput)
        {
            _logger.LogInformation("AddSMSProvider interactor method.");
            SMSResponseDto<SMSProviderSettingsDto> providerResponse = new SMSResponseDto<SMSProviderSettingsDto>();
            try
            {
                providerResponse = _smsProviderInteractor.AddSMSProvider(providerInput);
                _logger.LogDebug("Status: " + providerResponse.Status + ",Message: " + providerResponse.Message);
                return providerResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred in SMS Interactor while adding SMS provider: ", ex.Message);
                providerResponse.Message = "Error occurred while adding SMS provider: " + ex.Message;
                providerResponse.Status = false;
                return providerResponse;
            }
        }

        public SMSResponseDto<SMSChannelDto> AddSMSChannel(SMSChannelDto channelInput)
        {
            _logger.LogInformation("AddSMSChannel interactor method.");
            SMSResponseDto<SMSChannelDto> channelResponse = new SMSResponseDto<SMSChannelDto>();
            try
            {
                channelResponse = _smsChannelInteractor.AddSMSChannel(channelInput);
                _logger.LogDebug("Status: " + channelResponse.Status + ",Message: " + channelResponse.Message);
                return channelResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred in SMS Interactor while adding SMS channel: ", ex.Message);
                channelResponse.Message = "Error occurred while adding SMS channel: " + ex.Message;
                channelResponse.Status = false;
                return channelResponse;
            }
        }

        public SMSResponseDto<SMSTemplateDto> AddSMSTemplate(SMSTemplateDto templateInput)
        {
            _logger.LogInformation("AddSMSTemplate interactor method.");
            SMSResponseDto<SMSTemplateDto> templateResponse = new SMSResponseDto<SMSTemplateDto>();
            try
            {
                templateResponse = _smsTemplateInteractor.AddSMSTemplate(templateInput);
                _logger.LogDebug("Status: " + templateResponse.Status + ",Message: " + templateResponse.Message);
                return templateResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred in SMS Interactor while adding SMS template: ", ex.Message);
                templateResponse.Message = "Error occurred while adding SMS template: " + ex.Message;
                templateResponse.Status = false;
                return templateResponse;
            }
        }

        public SMSResponseDto<SMSChannelDto> GetSMSChannelByKey(string channelKey)
        {
            _logger.LogInformation("GetSMSChannelByKey interactor method.");
            SMSResponseDto<SMSChannelDto> channelResponse = new SMSResponseDto<SMSChannelDto>();
            try
            {
                channelResponse = _smsChannelInteractor.GetSMSChannelByKey(channelKey);
                _logger.LogDebug("Status: " + channelResponse.Status + ",Message: " + channelResponse.Message);
                return channelResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred in SMS Interactor while getting SMS channel by key: ", ex.Message);
                channelResponse.Message = "Error occurred while getting SMS channel by key: " + ex.Message;
                channelResponse.Status = false;
                return channelResponse;
            }
        }

        public SMSResponseDto<List<SMSProviderSettingsDto>> GetSMSProvidersByPool(string poolName, string providerName)
        {
            _logger.LogInformation("GetSMSProvidersByPool interactor method.");
            SMSResponseDto<List<SMSProviderSettingsDto>> poolResponse = new SMSResponseDto<List<SMSProviderSettingsDto>>();
            try
            {
                poolResponse = _smsProviderInteractor.GetSMSProvidersByPool(poolName, providerName);
                _logger.LogDebug("Status: "+ poolResponse .Status+ ",Message: " + poolResponse.Message);
                return poolResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred in SMS Interactor while getting SMS provider by pool: ", ex.Message);
                poolResponse.Message = "Error occurred while getting SMS provider by pool: " + ex.Message;
                poolResponse.Status = false;
                return poolResponse;
            }
        }

        public SMSResponseDto<List<SMSHistoryDto>> GetSMSHistoriesByTag(string channelKey, string tag)
        {
            _logger.LogInformation("GetSMSHistoriesByTag interactor method.");
            SMSResponseDto<List<SMSHistoryDto>> poolResponse = new SMSResponseDto<List<SMSHistoryDto>>();
            try
            {
                poolResponse = _smsHistoryInteractor.GetSMSHistoriesByTag(channelKey, tag);
                _logger.LogDebug("Status: " + poolResponse.Status + ",Message: " + poolResponse.Message);
                return poolResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred in SMS Interactor while getting SMS histories: ", ex.Message);
                poolResponse.Message = "Error occurred while getting SMS histories: " + ex.Message;
                poolResponse.Status = false;
                return poolResponse;
            }
        }

        public SMSResponseDto<string> SendSMS(SMSDto smsInputs)
        {

            _logger.LogInformation("SendSMS interactor method.");
            var response = new SMSResponseDto<string>();
            try
            {
                if (string.IsNullOrEmpty(smsInputs.ChannelKey))
                {
                    _logger.LogError("Channel key cannot be blank.");
                    response.Status = false;
                    response.Message = "Channel key cannot be blank.";
                    return response;
                }
                else
                {
                    var channelExist = _smsChannelInteractor.CheckIfChannelExist(smsInputs.ChannelKey).Result;
                    if (!channelExist)
                    {
                        _logger.LogError($"Invalid Channel key {smsInputs.ChannelKey}.");
                        response.Status = channelExist;
                        response.Message = $"Invalid Channel key {smsInputs.ChannelKey}.";
                        return response;
                    }
                }
                if (string.IsNullOrEmpty(smsInputs.TemplateName))
                {
                    _logger.LogError($"Template name cannot be blank.");
                    response.Status = false;
                    response.Message = "Template name cannot be blank.";
                    return response;
                }
                else
                {
                    var templateExist = _smsTemplateInteractor.CheckIfTemplateExist(smsInputs.ChannelKey, smsInputs.TemplateName).Result;
                    if (!templateExist)
                    {
                        _logger.LogError($"No template found for template name {smsInputs.TemplateName} and channel key {smsInputs.ChannelKey}.");
                        response.Status = templateExist;
                        response.Message = $"No template found for template name {smsInputs.TemplateName} and channel key {smsInputs.ChannelKey}.";
                        return response;
                    }
                }
                _logger.LogInformation("Trying to send sms.");
                _smsEventInteractor.SendSMS(smsInputs);
                response.Status = true;
                response.Message = $"SMS is sent successfully to {smsInputs.Recipients}.";
                _logger.LogDebug("" + response.Message);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred in SMSInteractor while sending SMS: ", ex.Message);
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }

    }
}
