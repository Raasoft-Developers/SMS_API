using AutoMapper;
using Microsoft.Extensions.Logging;
using Nvg.SMSService.Data.Entities;
using Nvg.SMSService.Data.SMSChannel;
using Nvg.SMSService.Data.SMSHistory;
using Nvg.SMSService.Data.SMSPool;
using Nvg.SMSService.Data.SMSProvider;
using Nvg.SMSService.Data.SMSQuota;
using Nvg.SMSService.Data.SMSTemplate;
using Nvg.SMSService.DTOS;
using Nvg.SMSService.SMSQuota;
using System;
using System.Collections.Generic;

namespace Nvg.SMSService.SMS
{
    public class SMSManagementInteractor : ISMSManagementInteractor
    {
        private readonly IMapper _mapper;
        private readonly ISMSEventInteractor _smsEventInteractor;
        private readonly ISMSPoolRepository _smsPoolRepository;
        private readonly ISMSProviderRepository _smsProviderRepository;
        private readonly ISMSChannelRepository _smsChannelRepository;
        private readonly ISMSTemplateRepository _smsTemplateRepository;
        private readonly ISMSHistoryRepository _smsHistoryRepository;
        private readonly ISMSQuotaRepository _smsQuotaRepository;
        private readonly ILogger<SMSManagementInteractor> _logger;

        public SMSManagementInteractor(IMapper mapper, ISMSEventInteractor smsEventInteractor, ISMSPoolRepository smsPoolRepository, ISMSProviderRepository smsProviderRepository,
            ISMSChannelRepository smsChannelRepository, ISMSTemplateRepository smsTemplateRepository,
            ISMSHistoryRepository smsHistoryRepository, ISMSQuotaRepository smsQuotaRepository, ILogger<SMSManagementInteractor> logger)
        {
            _mapper = mapper;
            _smsEventInteractor = smsEventInteractor;
            _smsPoolRepository = smsPoolRepository;
            _smsProviderRepository = smsProviderRepository;
            _smsChannelRepository = smsChannelRepository;
            _smsTemplateRepository = smsTemplateRepository;
            _smsHistoryRepository = smsHistoryRepository;
            _smsQuotaRepository = smsQuotaRepository;
            _logger = logger;
        }
        #region SMS Pool
        public SMSResponseDto<List<SMSPoolDto>> GetSMSPools()
        {
            _logger.LogInformation("GetSMSPools interactor method.");
            SMSResponseDto<List<SMSPoolDto>> poolResponse = new SMSResponseDto<List<SMSPoolDto>>();
            try
            {
                _logger.LogInformation("Trying to get SMS Pools.");
                var response = _smsPoolRepository.GetSMSPools();
                poolResponse = _mapper.Map<SMSResponseDto<List<SMSPoolDto>>>(response);
                _logger.LogDebug("Status: " + poolResponse.Status + ", " + poolResponse.Message);
                return poolResponse;
            }
            catch (Exception ex)
            {

                _logger.LogError("Error occurred in SMS Management Interactor while getting SMS pool: ", ex.Message);
                poolResponse.Message = "Error occurred while getting SMS pool: " + ex.Message;
                poolResponse.Status = false;
                return poolResponse;
            }
        }

        public SMSResponseDto<List<SMSPoolDto>> GetSMSPoolNames()
        {
            _logger.LogInformation("GetSMSPoolNames interactor method.");
            SMSResponseDto<List<SMSPoolDto>> poolResponse = new SMSResponseDto<List<SMSPoolDto>>();
            try
            {
                _logger.LogInformation("Trying to get SMS Pool Names.");
                var response = _smsPoolRepository.GetSMSPoolNames();
                poolResponse = _mapper.Map<SMSResponseDto<List<SMSPoolDto>>>(response);
                _logger.LogDebug("Status: " + poolResponse.Status + ", " + poolResponse.Message);
                return poolResponse;
            }
            catch (Exception ex)
            {

                _logger.LogError("Error occurred in SMS Management Interactor while getting SMS pool names: ", ex.Message);
                poolResponse.Message = "Error occurred while getting SMS pool: " + ex.Message;
                poolResponse.Status = false;
                return poolResponse;
            }
        }

        public SMSResponseDto<SMSPoolDto> AddSMSPool(SMSPoolDto poolInput)
        {
            _logger.LogInformation("AddSMSPool interactor method.");
            SMSResponseDto<SMSPoolDto> poolResponse = new SMSResponseDto<SMSPoolDto>();
            try
            {
                var mappedSMSInput = _mapper.Map<SMSPoolTable>(poolInput);
                var response = _smsPoolRepository.AddSMSPool(mappedSMSInput);
                poolResponse = _mapper.Map<SMSResponseDto<SMSPoolDto>>(response);
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

        public SMSResponseDto<SMSPoolDto> UpdateSMSPool(SMSPoolDto SMSPoolInput)
        {
            _logger.LogInformation("UpdateSMSPool interactor method.");
            SMSResponseDto<SMSPoolDto> poolResponse = new SMSResponseDto<SMSPoolDto>();
            try
            {
                var mappedSMSInput = _mapper.Map<SMSPoolTable>(SMSPoolInput);
                _logger.LogInformation("Trying to update SMS Pools.");
                var response = _smsPoolRepository.UpdateSMSPool(mappedSMSInput);
                poolResponse = _mapper.Map<SMSResponseDto<SMSPoolDto>>(response);
                _logger.LogDebug("Status: " + poolResponse.Status + ", " + poolResponse.Message);
                return poolResponse;
            }
            catch (Exception ex)
            {

                _logger.LogError("Error occurred in SMS Management Interactor while updating SMS pool: ", ex.Message);
                poolResponse.Message = "Error occurred while updating SMS pool: " + ex.Message;
                poolResponse.Status = false;
                return poolResponse;
            }
        }

        public SMSResponseDto<string> DeleteSMSPool(string poolID)
        {
            _logger.LogInformation("DeleteSMSPool interactor method.");
            _logger.LogDebug("Pool ID:" + poolID);
            SMSResponseDto<string> poolResponse = new SMSResponseDto<string>();
            try
            {
                _logger.LogInformation("Trying to delete SMS Pools.");
                poolResponse = _smsPoolRepository.DeleteSMSPool(poolID);
                _logger.LogDebug("Status: " + poolResponse.Status + ", " + poolResponse.Message);
                return poolResponse;
            }
            catch (Exception ex)
            {

                _logger.LogError("Error occurred in SMS Management Interactor while deleting SMS pool: ", ex.Message);
                poolResponse.Message = "Error occurred while deleting SMS pool: " + ex.Message;
                poolResponse.Status = false;
                return poolResponse;
            }
        }
        #endregion

        #region SMS Provider
        public SMSResponseDto<List<SMSProviderSettingsDto>> GetSMSProviders(string poolID)
        {
            _logger.LogInformation("GetSMSProviders interactor method.");
            _logger.LogDebug("Pool ID:" + poolID);
            SMSResponseDto<List<SMSProviderSettingsDto>> providerResponse = new SMSResponseDto<List<SMSProviderSettingsDto>>();
            try
            {
                _logger.LogInformation("Trying to get SMS Providers.");
                var response = _smsProviderRepository.GetSMSProviders(poolID);
                providerResponse = _mapper.Map<SMSResponseDto<List<SMSProviderSettingsDto>>>(response);
                _logger.LogDebug("Status: " + providerResponse.Status + ", " + providerResponse.Message);
                return providerResponse;
            }
            catch (Exception ex)
            {

                _logger.LogError("Error occurred in SMS Management Interactor while getting SMS provider: ", ex.Message);
                providerResponse.Message = "Error occurred while getting SMS provider: " + ex.Message;
                providerResponse.Status = false;
                return providerResponse;
            }
        }

        public SMSResponseDto<List<SMSProviderSettingsDto>> GetSMSProviderNames(string poolID)
        {
            _logger.LogInformation("GetSMSProviderNames interactor method.");
            _logger.LogDebug("Pool Name:" + poolID);
            SMSResponseDto<List<SMSProviderSettingsDto>> providerResponse = new SMSResponseDto<List<SMSProviderSettingsDto>>();
            try
            {
                _logger.LogInformation("Trying to get SMS Providers.");
                var responseDto = _smsProviderRepository.GetSMSProviderNames(poolID);
                providerResponse = _mapper.Map<SMSResponseDto<List<SMSProviderSettingsDto>>>(responseDto);
                _logger.LogDebug("Status: " + providerResponse.Status + ", " + providerResponse.Message);
                return providerResponse;
            }
            catch (Exception ex)
            {

                _logger.LogError("Error occurred in SMS Management Interactor while getting SMS provider names: ", ex.Message);
                providerResponse.Message = "Error occurred while getting SMS provider names: " + ex.Message;
                providerResponse.Status = false;
                return providerResponse;
            }
        }

        public SMSResponseDto<SMSProviderSettingsDto> AddSMSProvider(SMSProviderSettingsDto providerInput)
        {
            _logger.LogInformation("AddSMSProvider interactor method.");
            SMSResponseDto<SMSProviderSettingsDto> providerResponse = new SMSResponseDto<SMSProviderSettingsDto>();
            try
            {
                _logger.LogInformation("Trying to add SMSProvider.");
                var mappedSMSInput = _mapper.Map<SMSProviderSettingsTable>(providerInput);
                var response = _smsProviderRepository.AddSMSProvider(mappedSMSInput);
                providerResponse = _mapper.Map<SMSResponseDto<SMSProviderSettingsDto>>(response);
                _logger.LogDebug("Status: " + providerResponse.Status + ", " + providerResponse.Message);
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
                _logger.LogInformation("Trying to update SMSProvider.");
                var mappedSMSInput = _mapper.Map<SMSProviderSettingsTable>(providerInput);
                var response = _smsProviderRepository.UpdateSMSProvider(mappedSMSInput);
                providerResponse = _mapper.Map<SMSResponseDto<SMSProviderSettingsDto>>(response);
                _logger.LogDebug("Status: " + providerResponse.Status + ", " + providerResponse.Message);
                return providerResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred in SMS Interactor while updating SMS provider: ", ex.Message);
                providerResponse.Message = "Error occurred while updating SMS provider: " + ex.Message;
                providerResponse.Status = false;
                return providerResponse;
            }
        }

        public SMSResponseDto<string> DeleteSMSProvider(string providerID)
        {
            _logger.LogInformation("DeleteSMSProvider interactor method.");
            _logger.LogDebug("Provider ID:" + providerID);
            SMSResponseDto<string> providerResponse = new SMSResponseDto<string>();
            try
            {
                _logger.LogInformation("Trying to delete SMS Provider.");
                providerResponse = _smsProviderRepository.DeleteSMSProvider(providerID);
                _logger.LogDebug("Status: " + providerResponse.Status + ", " + providerResponse.Message);
                return providerResponse;
            }
            catch (Exception ex)
            {

                _logger.LogError("Error occurred in SMS Management Interactor while deleting SMS provider: ", ex.Message);
                providerResponse.Message = "Error occurred while deleting SMS provider: " + ex.Message;
                providerResponse.Status = false;
                return providerResponse;
            }
        }
        #endregion

        #region SMS Channel
        public SMSResponseDto<List<SMSChannelDto>> GetSMSChannelsByPool(string poolID)
        {
            _logger.LogInformation("GetSMSChannelsByPool interactor method.");
            _logger.LogDebug("Pool ID:" + poolID);
            SMSResponseDto<List<SMSChannelDto>> channelResponse = new SMSResponseDto<List<SMSChannelDto>>();
            try
            {
                _logger.LogInformation("Trying to get SMS Channels.");
                var response = _smsChannelRepository.GetSMSChannels(poolID);
                //channelResponse = _mapper.Map<SMSResponseDto<List<SMSChannelDto>>>(response);
                _logger.LogDebug("Status: " + response.Status + ", " + response.Message);
                return response;
            }
            catch (Exception ex)
            {

                _logger.LogError("Error occurred in SMS Management Interactor while getting SMS channel: ", ex.Message);
                channelResponse.Message = "Error occurred while getting SMS channel: " + ex.Message;
                channelResponse.Status = false;
                return channelResponse;
            }
        }        

        public SMSResponseDto<SMSChannelDto> AddSMSChannel(SMSChannelDto channelInput)
        {
            _logger.LogInformation("AddSMSChannel interactor method.");
            SMSResponseDto<SMSChannelDto> channelResponse = new SMSResponseDto<SMSChannelDto>();
            try
            {
                _logger.LogInformation("Trying to add SMSChannel.");
                var mappedSMSInput = _mapper.Map<SMSChannelTable>(channelInput);
                var response = _smsChannelRepository.AddSMSChannel(mappedSMSInput);
                if (response.Status && channelInput.IsRestrictedByQuota)
                {
                    //If channel has been added and channel isRestrictedByQuota, add sms quota for channel
                    channelInput.ID = response.Result.ID;
                    _smsQuotaRepository.AddSMSQuota(channelInput);
                }
                channelResponse = _mapper.Map<SMSResponseDto<SMSChannelDto>>(response);
                _logger.LogDebug("Status: " + channelResponse.Status + ", " + channelResponse.Message);
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

        public SMSResponseDto<SMSChannelDto> UpdateSMSChannel(SMSChannelDto channelInput)
        {
            _logger.LogInformation("UpdateSMSChannel interactor method.");
            SMSResponseDto<SMSChannelDto> channelResponse = new SMSResponseDto<SMSChannelDto>();
            try
            {
                _logger.LogInformation("Trying to update SMSChannel.");
                var mappedSMSInput = _mapper.Map<SMSChannelTable>(channelInput);
                var response = _smsChannelRepository.UpdateSMSChannel(mappedSMSInput);
                var quotaResponse = _smsQuotaRepository.UpdateSMSQuota(channelInput);
                if (!response.Status)
                {
                    //if sms channel is not updated , then take response of sms quota updation
                    response.Status = quotaResponse.Status;
                    response.Message = quotaResponse.Message;
                }
                channelResponse = _mapper.Map<SMSResponseDto<SMSChannelDto>>(response);
                _logger.LogDebug("Status: " + channelResponse.Status + ", " + channelResponse.Message);
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

        public SMSResponseDto<string> DeleteSMSChannel(string channelID)
        {
            _logger.LogInformation("DeleteSMSChannel interactor method.");
            _logger.LogDebug("Channel ID:" + channelID);
            SMSResponseDto<string> channelResponse = new SMSResponseDto<string>();
            try
            {
                _logger.LogInformation("Trying to delete SMS Channel.");
                var quotaResponse = _smsQuotaRepository.DeleteSMSQuota(channelID);
                channelResponse = _smsChannelRepository.DeleteSMSChannel(channelID);
                _logger.LogDebug("Status: " + channelResponse.Status + ", " + channelResponse.Message);
                return channelResponse;
            }
            catch (Exception ex)
            {

                _logger.LogError("Error occurred in SMS Management Interactor while deleting SMS channel: ", ex.Message);
                channelResponse.Message = "Error occurred while deleting SMS channel: " + ex.Message;
                channelResponse.Status = false;
                return channelResponse;
            }
        }

        public SMSResponseDto<List<SMSChannelDto>> GetSMSChannelKeys()
        {
            _logger.LogInformation("GetSMSChannelKeys interactor method.");
            SMSResponseDto<List<SMSChannelDto>> channelResponse = new SMSResponseDto<List<SMSChannelDto>>();
            try
            {
                _logger.LogInformation("Trying to get SMS Channel keys.");
                var response = _smsChannelRepository.GetSMSChannelKeys();
                channelResponse = _mapper.Map<SMSResponseDto<List<SMSChannelDto>>>(response);
                _logger.LogDebug("Status: " + channelResponse.Status + ", " + channelResponse.Message);
                return channelResponse;
            }
            catch (Exception ex)
            {

                _logger.LogError("Error occurred in SMS Management Interactor while getting SMS channel keys: ", ex.Message);
                channelResponse.Message = "Error occurred while getting SMS channel keys: " + ex.Message;
                channelResponse.Status = false;
                return channelResponse;
            }
        }
        #endregion

        #region SMS Template
        public SMSResponseDto<List<SMSTemplateDto>> GetSMSTemplatesByPool(string poolID)
        {
            _logger.LogInformation("GetSMSTemplatesByPool interactor method.");
            _logger.LogDebug("Pool Name:" + poolID);
            SMSResponseDto<List<SMSTemplateDto>> templateResponse = new SMSResponseDto<List<SMSTemplateDto>>();
            try
            {
                _logger.LogInformation("Trying to get SMS Templates.");
                var response = _smsTemplateRepository.GetSMSTemplatesByPool(poolID);
                templateResponse = _mapper.Map<SMSResponseDto<List<SMSTemplateDto>>>(response);
                _logger.LogDebug("Status: " + templateResponse.Status + ", " + templateResponse.Message);
                return templateResponse;
            }
            catch (Exception ex)
            {

                _logger.LogError("Error occurred in SMS Management Interactor while getting SMS templates: ", ex.Message);
                templateResponse.Message = "Error occurred while getting SMS templates: " + ex.Message;
                templateResponse.Status = false;
                return templateResponse;
            }
        }

        public SMSResponseDto<List<SMSTemplateDto>> GetSMSTemplatesByChannelID(string channelID)
        {
            _logger.LogInformation("GetSMSTemplatesByChannelID interactor method.");
            _logger.LogDebug("Channel ID:" + channelID);
            SMSResponseDto<List<SMSTemplateDto>> templateResponse = new SMSResponseDto<List<SMSTemplateDto>>();
            try
            {
                _logger.LogInformation("Trying to get SMS Templates.");
                var poolID = _smsChannelRepository.GetSMSChannelByID(channelID)?.Result?.SMSPoolID;
                if (!string.IsNullOrEmpty(poolID))
                {
                    var response = _smsTemplateRepository.GetSMSTemplatesByPool(poolID);
                    templateResponse = _mapper.Map<SMSResponseDto<List<SMSTemplateDto>>>(response);
                }
                else
                {
                    templateResponse.Message = "Channel does not exist.";
                    templateResponse.Status = false;
                }
                _logger.LogDebug("Status: " + templateResponse.Status + ", " + templateResponse.Message);
                return templateResponse;
            }
            catch (Exception ex)
            {

                _logger.LogError("Error occurred in SMS Management Interactor while getting sms templates: ", ex.Message);
                templateResponse.Message = "Error occurred while getting sms templates: " + ex.Message;
                templateResponse.Status = false;
                return templateResponse;
            }
        }

        public SMSResponseDto<SMSTemplateDto> GetSMSTemplate(string templateID)
        {
            _logger.LogInformation("GetSMSTemplate interactor method.");
            _logger.LogDebug("Template ID:" + templateID);
            SMSResponseDto<SMSTemplateDto> templateResponse = new SMSResponseDto<SMSTemplateDto>();
            try
            {
                _logger.LogInformation("Trying to get SMS Templates.");
                var response = _smsTemplateRepository.GetSMSTemplate(templateID);
                if (response != null)
                {
                    templateResponse.Status = true;
                    templateResponse.Message = "Successfully retrieved data";
                    templateResponse.Result = _mapper.Map<SMSTemplateDto>(response);
                }
                else
                {
                    templateResponse.Status = true;
                    templateResponse.Message = "Template not found";
                }
                _logger.LogDebug("Status: " + templateResponse.Status + ", " + templateResponse.Message);
                return templateResponse;
            }
            catch (Exception ex)
            {

                _logger.LogError("Error occurred in SMS Management Interactor while getting SNS template by id: ", ex.Message);
                templateResponse.Message = "Error occurred while getting SMS template by id: " + ex.Message;
                templateResponse.Status = false;
                return templateResponse;
            }
        }

        public SMSResponseDto<SMSTemplateDto> AddSMSTemplate(SMSTemplateDto templateInput)
        {
            _logger.LogInformation("AddSMSTemplate interactor method.");
            SMSResponseDto<SMSTemplateDto> templateResponse = new SMSResponseDto<SMSTemplateDto>();
            try
            {
                _logger.LogInformation("Trying to add SMSTemplate.");
                var mappedSMSInput = _mapper.Map<SMSTemplateTable>(templateInput);
                var response = _smsTemplateRepository.AddSMSTemplate(mappedSMSInput);
                templateResponse = _mapper.Map<SMSResponseDto<SMSTemplateDto>>(response);
                _logger.LogDebug("Status: " + templateResponse.Status + ", " + templateResponse.Message);
                return templateResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred in SMS Interactor while adding SMS Template: ", ex.Message);
                templateResponse.Message = "Error occurred while adding SMS Template: " + ex.Message;
                templateResponse.Status = false;
                return templateResponse;
            }
        }

        public SMSResponseDto<SMSTemplateDto> UpdateSMSTemplate(SMSTemplateDto templateInput)
        {
            _logger.LogInformation("AddUpdateSMSTemplate interactor method.");
            SMSResponseDto<SMSTemplateDto> templateResponse = new SMSResponseDto<SMSTemplateDto>();
            try
            {
                _logger.LogInformation("Trying to update SMSTemplate.");
                var mappedSMSInput = _mapper.Map<SMSTemplateTable>(templateInput);
                var response = _smsTemplateRepository.UpdateSMSTemplate(mappedSMSInput);
                templateResponse = _mapper.Map<SMSResponseDto<SMSTemplateDto>>(response);
                _logger.LogDebug("Status: " + templateResponse.Status + ", " + templateResponse.Message);
                return templateResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred in SMS Interactor while updating SMS Template: ", ex.Message);
                templateResponse.Message = "Error occurred while updating SMS Template: " + ex.Message;
                templateResponse.Status = false;
                return templateResponse;
            }
        }


        public SMSResponseDto<string> DeleteSMSTemplate(string templateID)
        {
            _logger.LogInformation("DeleteSMSTemplate interactor method.");
            _logger.LogDebug("Template ID:" + templateID);
            SMSResponseDto<string> templateResponse = new SMSResponseDto<string>();
            try
            {
                _logger.LogInformation("Trying to delete SMS Template.");
                templateResponse = _smsTemplateRepository.DeleteSMSTemplate(templateID);
                _logger.LogDebug("Status: " + templateResponse.Status + ", " + templateResponse.Message);
                return templateResponse;
            }
            catch (Exception ex)
            {

                _logger.LogError("Error occurred in SMS Management Interactor while deleting SMS template: ", ex.Message);
                templateResponse.Message = "Error occurred while deleting SMS template: " + ex.Message;
                templateResponse.Status = false;
                return templateResponse;
            }
        }
        #endregion

        #region SMS Histories
        public SMSResponseDto<List<SMSHistoryDto>> GetSMSHistories(string channelID, string tag)
        {
            _logger.LogInformation("GetSMSHistories interactor method.");
            _logger.LogDebug("Channel ID:" + channelID);
            SMSResponseDto<List<SMSHistoryDto>> historiesResponse = new SMSResponseDto<List<SMSHistoryDto>>();
            try
            {
                var channelKey = _smsChannelRepository.GetSMSChannelByID(channelID)?.Result?.Key;
                if (!string.IsNullOrEmpty(channelKey))
                {
                    _logger.LogInformation("Trying to get SMS histories.");
                    var response = _smsHistoryRepository.GetSMSHistoriesByTag(channelKey, tag);
                    historiesResponse = _mapper.Map<SMSResponseDto<List<SMSHistoryDto>>>(response);
                    _logger.LogDebug("Status: " + historiesResponse.Status + ", " + historiesResponse.Message);
                    return historiesResponse;
                }
                else
                {
                    historiesResponse.Status = false;
                    historiesResponse.Message = "Could not get the channel data.";
                    _logger.LogDebug("Status: " + historiesResponse.Status + ", " + historiesResponse.Message);
                    return historiesResponse;
                }
            }
            catch (Exception ex)
            {

                _logger.LogError("Error occurred in SMS Management Interactor while getting SMS histories: ", ex.Message);
                historiesResponse.Message = "Error occurred while getting SMS histories: " + ex.Message;
                historiesResponse.Status = false;
                return historiesResponse;
            }
        }
        #endregion

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
                    var channelExist = _smsChannelRepository.CheckIfChannelExist(smsInputs.ChannelKey).Result;
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
                    var templateExist = _smsTemplateRepository.CheckIfTemplateExist(smsInputs.ChannelKey, smsInputs.TemplateName).Result;
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
