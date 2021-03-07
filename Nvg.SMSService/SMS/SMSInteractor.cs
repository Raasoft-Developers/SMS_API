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
            _logger.LogInformation("In SMSInteractor: AddSMSPool interactor method hit.");
            SMSResponseDto<SMSPoolDto> poolResponse = new SMSResponseDto<SMSPoolDto>();
            try
            {
                poolResponse = _smsPoolInteractor.AddSMSPool(poolInput);
                _logger.LogDebug("In SMSInteractor: " + poolResponse.Message);
                return poolResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("In SMSInteractor: Error occurred in SMS Interactor while adding SMS pool: ", ex.Message);
                poolResponse.Message = "Error occurred while adding SMS pool: " + ex.Message;
                poolResponse.Status = false;
                return poolResponse;
            }
        }

        public SMSResponseDto<SMSProviderSettingsDto> AddSMSProvider(SMSProviderSettingsDto providerInput)
        {
            _logger.LogInformation("In SMSInteractor: AddSMSProvider interactor method hit.");
            SMSResponseDto<SMSProviderSettingsDto> providerResponse = new SMSResponseDto<SMSProviderSettingsDto>();
            try
            {
                providerResponse = _smsProviderInteractor.AddSMSProvider(providerInput);
                _logger.LogDebug("In SMSInteractor: " + providerResponse.Message);
                return providerResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("In SMSInteractor: Error occurred in SMS Interactor while adding SMS provider: ", ex.Message);
                providerResponse.Message = "Error occurred while adding SMS provider: " + ex.Message;
                providerResponse.Status = false;
                return providerResponse;
            }
        }

        public SMSResponseDto<SMSChannelDto> AddSMSChannel(SMSChannelDto channelInput)
        {
            _logger.LogInformation("In SMSInteractor: AddSMSChannel interactor method hit.");
            SMSResponseDto<SMSChannelDto> channelResponse = new SMSResponseDto<SMSChannelDto>();
            try
            {
                channelResponse = _smsChannelInteractor.AddSMSChannel(channelInput);
                _logger.LogDebug("In SMSInteractor: " + channelResponse.Message);
                return channelResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("In SMSInteractor: Error occurred in SMS Interactor while adding SMS channel: ", ex.Message);
                channelResponse.Message = "Error occurred while adding SMS channel: " + ex.Message;
                channelResponse.Status = false;
                return channelResponse;
            }
        }

        public SMSResponseDto<SMSTemplateDto> AddSMSTemplate(SMSTemplateDto templateInput)
        {
            _logger.LogInformation("In SMSInteractor: AddSMSTemplate interactor method hit.");
            SMSResponseDto<SMSTemplateDto> templateResponse = new SMSResponseDto<SMSTemplateDto>();
            try
            {
                templateResponse = _smsTemplateInteractor.AddSMSTemplate(templateInput);
                _logger.LogDebug("In SMSInteractor: " + templateResponse.Message);
                return templateResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("In SMSInteractor: Error occurred in SMS Interactor while adding SMS template: ", ex.Message);
                templateResponse.Message = "Error occurred while adding SMS template: " + ex.Message;
                templateResponse.Status = false;
                return templateResponse;
            }
        }

        public SMSResponseDto<SMSChannelDto> GetSMSChannelByKey(string channelKey)
        {
            _logger.LogInformation("In SMSInteractor: GetSMSChannelByKey interactor method hit.");
            SMSResponseDto<SMSChannelDto> channelResponse = new SMSResponseDto<SMSChannelDto>();
            try
            {
                channelResponse = _smsChannelInteractor.GetSMSChannelByKey(channelKey);
                _logger.LogDebug("In SMSInteractor: " + channelResponse.Message);
                return channelResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("In SMSInteractor: Error occurred in SMS Interactor while getting SMS channel by key: ", ex.Message);
                channelResponse.Message = "Error occurred while getting SMS channel by key: " + ex.Message;
                channelResponse.Status = false;
                return channelResponse;
            }
        }

        public SMSResponseDto<List<SMSProviderSettingsDto>> GetSMSProvidersByPool(string poolName, string providerName)
        {
            _logger.LogInformation("In SMSInteractor: GetSMSProvidersByPool interactor method hit.");
            SMSResponseDto<List<SMSProviderSettingsDto>> poolResponse = new SMSResponseDto<List<SMSProviderSettingsDto>>();
            try
            {
                poolResponse = _smsProviderInteractor.GetSMSProvidersByPool(poolName, providerName);
                _logger.LogDebug("In SMSInteractor: " + poolResponse.Message);
                return poolResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("In SMSInteractor: Error occurred in SMS Interactor while getting SMS provider by pool: ", ex.Message);
                poolResponse.Message = "Error occurred while getting SMS provider by pool: " + ex.Message;
                poolResponse.Status = false;
                return poolResponse;
            }
        }

        public SMSResponseDto<List<SMSHistoryDto>> GetSMSHistoriesByTag(string channelKey, string tag)
        {
            _logger.LogInformation("In SMSInteractor: GetSMSHistoriesByTag interactor method hit.");
            SMSResponseDto<List<SMSHistoryDto>> poolResponse = new SMSResponseDto<List<SMSHistoryDto>>();
            try
            {
                poolResponse = _smsHistoryInteractor.GetSMSHistoriesByTag(channelKey, tag);
                _logger.LogDebug("In SMSInteractor: " + poolResponse.Message);
                return poolResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("In SMSInteractor: Error occurred in SMS Interactor while getting SMS histories: ", ex.Message);
                poolResponse.Message = "Error occurred while getting SMS histories: " + ex.Message;
                poolResponse.Status = false;
                return poolResponse;
            }
        }

        public SMSResponseDto<string> SendSMS(SMSDto smsInputs)
        {

            _logger.LogInformation("In SMSInteractor: SendSMS interactor method hit.");
            var response = new SMSResponseDto<string>();
            try
            {
                if (string.IsNullOrEmpty(smsInputs.ChannelKey))
                {
                    _logger.LogError("In SMSInteractor: Channel key cannot be blank.");
                    response.Status = false;
                    response.Message = "Channel key cannot be blank.";
                    return response;
                }
                else
                {
                    var channelExist = _smsChannelInteractor.CheckIfChannelExist(smsInputs.ChannelKey).Result;
                    if (!channelExist)
                    {
                        _logger.LogError($"In SMSInteractor: Invalid Channel key {smsInputs.ChannelKey}.");
                        response.Status = channelExist;
                        response.Message = $"Invalid Channel key {smsInputs.ChannelKey}.";
                        return response;
                    }
                }
                if (string.IsNullOrEmpty(smsInputs.TemplateName))
                {
                    _logger.LogError($"In SMSInteractor: Template name cannot be blank.");
                    response.Status = false;
                    response.Message = "Template name cannot be blank.";
                    return response;
                }
                else
                {
                    var templateExist = _smsTemplateInteractor.CheckIfTemplateExist(smsInputs.ChannelKey, smsInputs.TemplateName).Result;
                    if (!templateExist)
                    {
                        _logger.LogError($"In SMSInteractor: No template found for template name {smsInputs.TemplateName} and channel key {smsInputs.ChannelKey}.");
                        response.Status = templateExist;
                        response.Message = $"No template found for template name {smsInputs.TemplateName} and channel key {smsInputs.ChannelKey}.";
                        return response;
                    }
                }
                _smsEventInteractor.SendSMS(smsInputs);
                response.Status = true;
                response.Message = $"SMS is sent successfully to {smsInputs.Recipients}.";
                _logger.LogDebug("In SMSInteractor: " + response.Message);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("In SMSInteractor: Error occurred in SMSInteractor while sending SMS: ", ex.Message);
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }

    }
}
