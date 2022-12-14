using Microsoft.Extensions.Logging;
using SMSService.DTOS;
using SMSService.SMSChannel;
using SMSService.SMSHistory;
using SMSService.SMSPool;
using SMSService.SMSProvider;
using SMSService.SMSQuota;
using System;
using System.Collections.Generic;

namespace SMSService.SMS
{
    public class SMSInteractor : ISMSInteractor
    {
        private readonly ISMSEventInteractor _smsEventInteractor;
        private readonly ISMSPoolInteractor _smsPoolInteractor;
        private readonly ISMSProviderInteractor _smsProviderInteractor;
        private readonly ISMSChannelInteractor _smsChannelInteractor;
        private readonly ISMSTemplateInteractor _smsTemplateInteractor;
        private readonly ISMSHistoryInteractor _smsHistoryInteractor;
        private readonly ISMSQuotaInteractor _smsQuotaInteractor;
        private readonly ILogger<SMSInteractor> _logger;

        public SMSInteractor(ISMSEventInteractor smsEventInteractor,
            ISMSPoolInteractor smsPoolInteractor, ISMSProviderInteractor smsProviderInteractor,
            ISMSChannelInteractor smsChannelInteractor, ISMSTemplateInteractor smsTemplateInteractor,
            ISMSHistoryInteractor smsHistoryInteractor, ISMSQuotaInteractor smsQuotaInteractor, ILogger<SMSInteractor> logger)
        {
            _smsEventInteractor = smsEventInteractor;
            _smsPoolInteractor = smsPoolInteractor;
            _smsProviderInteractor = smsProviderInteractor;
            _smsChannelInteractor = smsChannelInteractor;
            _smsTemplateInteractor = smsTemplateInteractor;
            _smsHistoryInteractor = smsHistoryInteractor;
            _smsQuotaInteractor = smsQuotaInteractor;
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

        public SMSResponseDto<SMSProviderSettingsDto> UpdateSMSProvider(SMSProviderSettingsDto providerInput)
        {
            _logger.LogInformation("UpdateSMSProvider interactor method.");
            SMSResponseDto<SMSProviderSettingsDto> providerResponse = new SMSResponseDto<SMSProviderSettingsDto>();
            try
            {
                providerResponse = _smsProviderInteractor.UpdateSMSProvider(providerInput);
                _logger.LogDebug("Status: " + providerResponse.Status + ",Message: " + providerResponse.Message);
                return providerResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred in SMS Interactor while updating SMS provider: ", ex.Message);
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
                if (channelInput.IsRestrictedByQuota && channelInput.MonthlyQuota > 0 && channelInput.TotalQuota > 0 && channelInput.MonthlyQuota > channelInput.TotalQuota)
                {
                    channelResponse.Status = false;
                    channelResponse.Message = "Monthly quota cannot be greater than Total quota. Channel and Quota have not been added to the database.";
                    return channelResponse;
                }
                else if (channelInput.IsRestrictedByQuota && (channelInput.MonthlyQuota == 0 || channelInput.TotalQuota == 0))
                {
                    channelResponse.Status = false;
                    channelResponse.Message = "Monthly quota and/or Total quota cannot have value as 0. Channel and Quota have not been added to the database.";
                    return channelResponse;
                }
                channelResponse = _smsChannelInteractor.AddSMSChannel(channelInput);
                if (channelResponse.Status && channelInput.IsRestrictedByQuota)
                {
                    channelInput.ID = channelResponse.Result.ID;
                    //If channel has been added and sms is restricted by quota, add sms quota for channel
                    var quotaResponse = _smsQuotaInteractor.AddSMSQuota(channelInput);
                    channelResponse.Message += quotaResponse.Message;
                    if (quotaResponse.Status)
                    {
                        channelResponse.Result.TotalQuota = quotaResponse.Result.TotalQuota;
                        channelResponse.Result.TotalConsumption = quotaResponse.Result.TotalConsumption;
                        channelResponse.Result.MonthlyConsumption = quotaResponse.Result.MonthlyConsumption;
                        channelResponse.Result.MonthlyQuota = quotaResponse.Result.MonthlyQuota;
                        channelResponse.Result.CurrentMonth = quotaResponse.Result.CurrentMonth;
                        channelResponse.Result.IsRestrictedByQuota = channelInput.IsRestrictedByQuota;
                    }
                }
                _logger.LogDebug("Status: " + channelResponse.Status + ",Message: " + channelResponse.Message);
                return channelResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred in SMS Interactor while adding SMS channel: ", ex.Message);
                channelResponse.Message = "Error occurred while updating SMS channel: " + ex.Message;
                channelResponse.Status = false;
                return channelResponse;
            }
        }

        public SMSResponseDto<SMSChannelDto> UpdateSMSChannel(SMSChannelDto channelInput)
        {
            _logger.LogInformation("UpdateSMSChannel interactor method.");
            SMSResponseDto<SMSChannelDto> channelResponse = new SMSResponseDto<SMSChannelDto>();
            try
            {
                if (channelInput.IsRestrictedByQuota && channelInput.MonthlyQuota > channelInput.TotalQuota)
                {
                    channelResponse.Status = false;
                    channelResponse.Message = "Monthly quota cannot be greater than total quota. Channel and quota has not been updated in the database.";
                    return channelResponse;
                }
                channelResponse = _smsChannelInteractor.UpdateSMSChannel(channelInput);
                if(channelInput.IsRestrictedByQuota &&  channelInput.MonthlyQuota ==0 && channelInput.TotalQuota == 0)
                {
                    _logger.LogDebug("Status: " + channelResponse.Status + ",Message: " + channelResponse.Message);
                    return channelResponse;
                }
                channelInput.ID = _smsChannelInteractor.GetSMSChannelByKey(channelInput.Key).Result?.ID;
                if (!string.IsNullOrEmpty(channelInput.ID))
                {
                    var quotaResponse = _smsQuotaInteractor.UpdateSMSQuota(channelInput);
                    if (!channelResponse.Status && quotaResponse.Status)
                    {
                        //if sms channel is not updated , then take response of sms quota updation
                        channelResponse.Status = quotaResponse.Status;
                        channelResponse.Message = quotaResponse.Message;
                    }
                    else if (channelResponse.Status && !quotaResponse.Status)
                    {
                        channelResponse.Status = channelResponse.Status;
                        channelResponse.Message += quotaResponse.Message;
                    }
                    else
                    {
                        channelResponse.Status = channelResponse.Status && quotaResponse.Status;
                        channelResponse.Message += quotaResponse.Message;
                    }
                    if (quotaResponse.Status)
                    {
                        channelResponse.Result.TotalQuota = quotaResponse.Result.TotalQuota;
                        channelResponse.Result.TotalConsumption = quotaResponse.Result.TotalConsumption;
                        channelResponse.Result.MonthlyConsumption = quotaResponse.Result.MonthlyConsumption;
                        channelResponse.Result.MonthlyQuota = quotaResponse.Result.MonthlyQuota;
                        channelResponse.Result.CurrentMonth = quotaResponse.Result.CurrentMonth;
                        channelResponse.Result.IsRestrictedByQuota = channelInput.IsRestrictedByQuota;
                    }
                }
                else
                {
                    channelResponse.Status = false;
                    channelResponse.Message = "Invalid Channel Key.";
                }

                _logger.LogDebug("Status: " + channelResponse.Status + ",Message: " + channelResponse.Message);
                return channelResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred in SMS Interactor while updating SMS channel: ", ex.Message);
                channelResponse.Message = "Error occurred while updating SMS channel: " + ex.Message;
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

        public SMSResponseDto<SMSTemplateDto> UpdateSMSTemplate(SMSTemplateDto templateInput)
        {
            _logger.LogInformation("UpdateSMSTemplate interactor method.");
            SMSResponseDto<SMSTemplateDto> templateResponse = new SMSResponseDto<SMSTemplateDto>();
            try
            {
                templateResponse = _smsTemplateInteractor.UpdateSMSTemplate(templateInput);
                _logger.LogDebug("Status: " + templateResponse.Status + ",Message: " + templateResponse.Message);
                return templateResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred in SMS Interactor while updating SMS template: ", ex.Message);
                templateResponse.Message = "Error occurred while updating SMS template: " + ex.Message;
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
                _logger.LogDebug("Status: " + poolResponse.Status + ",Message: " + poolResponse.Message);
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

        public SMSResponseDto<SMSBalanceDto> SendSMS(SMSDto smsInputs)
        {

            _logger.LogInformation("SendSMS interactor method.");
            var response = new SMSResponseDto<SMSBalanceDto>();
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
                var smsBalance = _smsQuotaInteractor.CheckIfQuotaExceeded(smsInputs.ChannelKey);
                if (smsBalance != null && smsBalance.IsExceeded)
                {
                    _logger.LogError($"SMS Quota for Channel {smsInputs.ChannelKey} has been reached.");
                    response.Status = !smsBalance.IsExceeded;
                    response.Message = $"SMS Quota for Channel {smsInputs.ChannelKey} has been reached.";
                    return response;
                }
                _logger.LogInformation("Trying to send sms.");
                _smsEventInteractor.SendSMS(smsInputs);
                response.Status = true;
                response.Result = smsBalance;
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

        public SMSResponseDto<List<SMSHistoryDto>> GetSMSHistoriesByDateRange(string channelKey, string tag, string fromDate, string toDate)
        {
            _logger.LogInformation("GetSMSHistoriesByDateRange interactor method.");
            SMSResponseDto<List<SMSHistoryDto>> poolResponse = new SMSResponseDto<List<SMSHistoryDto>>();
            try
            {
                poolResponse = _smsHistoryInteractor.GetSMSHistoriesByDateRange(channelKey, tag, fromDate, toDate);
                _logger.LogDebug("Status: " + poolResponse.Status + ",Message: " + poolResponse.Message);
                return poolResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred in SMS Interactor while getting SMS histories by date range: ", ex.Message);
                poolResponse.Message = "Error occurred while getting SMS histories by date range: " + ex.Message;
                poolResponse.Status = false;
                return poolResponse;
            }
        }

        public SMSResponseDto<SMSQuotaDto> GetSMSQuota(string channelKey)
        {
            _logger.LogInformation("GetSMSQuota interactor method.");
            SMSResponseDto<SMSQuotaDto> quotaResponse = new SMSResponseDto<SMSQuotaDto>();
            try
            {
                var channelExist = _smsChannelInteractor.CheckIfChannelExist(channelKey).Result;
                if (!channelExist)
                {
                    quotaResponse.Status = channelExist;
                    quotaResponse.Message = $"Invalid Channel key {channelKey}.";
                }
                else
                {
                    quotaResponse = _smsQuotaInteractor.GetSMSQuota(channelKey);
                }
                _logger.LogDebug("Status: " + quotaResponse.Status + ",Message: " + quotaResponse.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred in SMS Interactor while getting SMS Quota: ", ex.Message);
                quotaResponse.Message = "Error occurred while getting SMS Quota: " + ex.Message;
                quotaResponse.Status = false;
            }
            return quotaResponse;
        }
    }
}
