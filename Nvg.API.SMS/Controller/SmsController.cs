using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nvg.API.SMS.Models;
using Nvg.SMSService.DTOS;
using Nvg.SMSService.SMS;
using System;
using System.Collections.Generic;

namespace Nvg.API.SMS.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SmsController : ControllerBase
    {
        private readonly ISMSInteractor _smsInteractor;
        private readonly ILogger<SmsController> _logger;
        private readonly IMapper _mapper;
        private readonly string defaultChannelKey = "default-channel";

        public SmsController(ISMSInteractor smsInteractor, ILogger<SmsController> logger, IMapper mapper)
        {
            _smsInteractor = smsInteractor;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// API to add the SMS Pool data.
        /// </summary>
        /// <param name="poolInput"><see cref="PoolInput"/></param>
        /// <returns><see cref="SMSResponseDto{T}"></see></returns>
        [HttpPost]
        public ActionResult AddSMSPool(PoolInput poolInput)
        {
            _logger.LogInformation("AddSMSPool action method.");
            _logger.LogInformation($"SMSPoolName: {poolInput.Name}.");
            SMSResponseDto<SMSPoolDto> poolResponse = new SMSResponseDto<SMSPoolDto>();
            try
            {
                if (!string.IsNullOrWhiteSpace(poolInput.Name))
                {
                    var mappedInput = _mapper.Map<SMSPoolDto>(poolInput);
                    poolResponse = _smsInteractor.AddSMSPool(mappedInput);
                    if (poolResponse.Status)
                    {
                        _logger.LogDebug("Status: " + poolResponse.Status + ", Message:" + poolResponse.Message);
                        return Ok(poolResponse);
                    }
                    else
                    {
                        _logger.LogError("Status: " + poolResponse.Status + ", Message:" + poolResponse.Message);
                        return StatusCode(412, poolResponse);
                    }
                }
                else
                {
                    poolResponse.Status = false;
                    poolResponse.Message = "Pool Name cannot be empty or whitespace.";
                    _logger.LogError("Status: " + poolResponse.Status + ", Message:" + poolResponse.Message);
                    return StatusCode(412, poolResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while adding SMS pool: " + ex.Message);
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// API to add the SMS Provider data.
        /// </summary>
        /// <param name="providerInput"><see cref="ProviderInput"/></param>
        /// <returns><see cref="SMSResponseDto{T}"></see></returns>
        [HttpPost]
        public ActionResult AddSMSProvider(ProviderInput providerInput)
        {
            _logger.LogInformation("AddSMSProvider action method.");
            _logger.LogInformation($"SMSPoolName: {providerInput.SMSPoolName}, ProviderName: {providerInput.Name}, Configuration: {providerInput.Configuration}.");
            SMSResponseDto<SMSProviderSettingsDto> providerResponse = new SMSResponseDto<SMSProviderSettingsDto>();
            try
            {
                if (!string.IsNullOrWhiteSpace(providerInput.Name) && !string.IsNullOrWhiteSpace(providerInput.Type) && !string.IsNullOrWhiteSpace(providerInput.Configuration))
                {
                    var mappedInput = _mapper.Map<SMSProviderSettingsDto>(providerInput);
                    providerResponse = _smsInteractor.AddSMSProvider(mappedInput);
                    if (providerResponse.Status)
                    {
                        _logger.LogDebug("Status: " + providerResponse.Status + ", Message:" + providerResponse.Message);
                        return Ok(providerResponse);
                    }
                    else
                    {
                        _logger.LogError("Status: " + providerResponse.Status + ", Message:" + providerResponse.Message);
                        return StatusCode(412, providerResponse);
                    }
                }
                else
                {
                    providerResponse.Status = false;
                    providerResponse.Message = "Provider Name, Type and Configuration cannot be empty or whitespace.";
                    _logger.LogError("Status: " + providerResponse.Status + ", Message:" + providerResponse.Message);
                    return StatusCode(412, providerResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while adding SMS provider: " + ex.Message);
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// API to update the SMS Provider data.
        /// </summary>
        /// <param name="providerInput"><see cref="ProviderInput"/></param>
        /// <returns><see cref="SMSResponseDto{T}"></see></returns>
        [HttpPost]
        public ActionResult UpdateSMSProvider(ProviderInput providerInput)
        {
            _logger.LogInformation("UpdateSMSProvider action method.");
            _logger.LogInformation($"SMSPoolName: {providerInput.SMSPoolName}, ProviderName: {providerInput.Name}, Configuration: {providerInput.Configuration}.");
            try
            {
                var mappedInput = _mapper.Map<SMSProviderSettingsDto>(providerInput);
                var providerResponse = _smsInteractor.UpdateSMSProvider(mappedInput);
                if (providerResponse.Status)
                {
                    _logger.LogDebug("Status: " + providerResponse.Status + ", Message:" + providerResponse.Message);
                    return Ok(providerResponse);
                }
                else
                {
                    _logger.LogError("Status: " + providerResponse.Status + ", Message:" + providerResponse.Message);
                    return StatusCode(412, providerResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while updating SMS provider: " + ex.Message);
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// API to add the SMS Channel data.
        /// </summary>
        /// <param name="channelInput"><see cref="ChannelInput"/></param>
        /// <returns><see cref="SMSResponseDto{T}"></see></returns>
        [HttpPost]
        public ActionResult AddSMSChannel(ChannelInput channelInput)
        {
            _logger.LogInformation("AddSMSChannel action method.");
            _logger.LogInformation($"SMSPoolName: {channelInput.SMSPoolName}, ProviderName: {channelInput.SMSProviderName}.");
            SMSResponseDto<SMSChannelDto> channelResponse = new SMSResponseDto<SMSChannelDto>();
            try
            {
                if (!string.IsNullOrWhiteSpace(channelInput.Key))
                {
                    var mappedInput = _mapper.Map<SMSChannelDto>(channelInput);
                    channelResponse = _smsInteractor.AddSMSChannel(mappedInput);
                    if (channelResponse.Status)
                    {
                        _logger.LogDebug("Status: " + channelResponse.Status + ", Message:" + channelResponse.Message);
                        return Ok(channelResponse);
                    }
                    else
                    {
                        _logger.LogError("Status: " + channelResponse.Status + ", Message:" + channelResponse.Message);
                        return StatusCode(412, channelResponse);
                    }
                }
                else
                {
                    channelResponse.Status = false;
                    channelResponse.Message = "Channel Key cannot be empty or whitespace.";
                    _logger.LogError("Status: " + channelResponse.Status + ", Message:" + channelResponse.Message);
                    return StatusCode(412, channelResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while adding SMS channel: " + ex.Message);
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// API to update the SMS Channel data.
        /// </summary>
        /// <param name="channelInput"><see cref="ChannelInput"/></param>
        /// <returns><see cref="SMSResponseDto{T}"></see></returns>
        [HttpPost]
        public ActionResult UpdateSMSChannel(ChannelInput channelInput)
        {
            _logger.LogInformation("UpdateSMSChannel action method.");
            _logger.LogInformation($"SMSPoolName: {channelInput.SMSPoolName}, ProviderName: {channelInput.SMSProviderName}.");
            try
            {
                var mappedInput = _mapper.Map<SMSChannelDto>(channelInput);
                var channelResponse = _smsInteractor.UpdateSMSChannel(mappedInput);
                if (channelResponse.Status)
                {
                    _logger.LogDebug("Status: " + channelResponse.Status + ", Message:" + channelResponse.Message);
                    return Ok(channelResponse);
                }
                else
                {
                    _logger.LogError("Status: " + channelResponse.Status + ", Message:" + channelResponse.Message);
                    return StatusCode(412, channelResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while updating SMS channel: " + ex.Message);
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// API to add the SMS Template data.
        /// </summary>
        /// <param name="templateInput"><see cref="TemplateInput"/></param>
        /// <returns><see cref="SMSResponseDto{T}"></see></returns>
        [HttpPost]
        public ActionResult AddSMSTemplate(TemplateInput templateInput)
        {
            _logger.LogInformation("AddSMSTemplate action method.");
            _logger.LogInformation($"SMSPoolName: {templateInput.SMSPoolName}, MessageTemplate: {templateInput.MessageTemplate}, Variant: {templateInput.Variant}.");
            SMSResponseDto<SMSTemplateDto> templateResponse = new SMSResponseDto<SMSTemplateDto>();
            try
            {
                if (!string.IsNullOrWhiteSpace(templateInput.MessageTemplate) && !string.IsNullOrWhiteSpace(templateInput.Name))
                {
                    var mappedInput = _mapper.Map<SMSTemplateDto>(templateInput);
                    templateResponse = _smsInteractor.AddSMSTemplate(mappedInput);
                    if (templateResponse.Status)
                    {
                        _logger.LogDebug("Status: " + templateResponse.Status + ", Message:" + templateResponse.Message);
                        return Ok(templateResponse);
                    }
                    else
                    {
                        _logger.LogError("Status: " + templateResponse.Status + ", Message:" + templateResponse.Message);
                        return StatusCode(412, templateResponse);
                    }
                }
                else
                {
                    templateResponse.Status = false;
                    templateResponse.Message = "Name and Message Template cannot be empty or whitespace.";
                    _logger.LogError("Status: " + templateResponse.Status + ", Message:" + templateResponse.Message);
                    return StatusCode(412, templateResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while adding SMS template: " + ex.Message);
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// API to update the SMS Template data.
        /// </summary>
        /// <param name="templateInput"><see cref="TemplateInput"/></param>
        /// <returns><see cref="SMSResponseDto{T}"></see></returns>
        [HttpPost]
        public ActionResult UpdateSMSTemplate(TemplateInput templateInput)
        {
            _logger.LogInformation("UpdateSMSTemplate action method.");
            _logger.LogInformation($"SMSPoolName: {templateInput.SMSPoolName}, MessageTemplate: {templateInput.MessageTemplate}, Variant: {templateInput.Variant}.");
            try
            {
                var mappedInput = _mapper.Map<SMSTemplateDto>(templateInput);
                var templateResponse = _smsInteractor.UpdateSMSTemplate(mappedInput);
                if (templateResponse.Status)
                {
                    _logger.LogDebug("Status: " + templateResponse.Status + ", Message:" + templateResponse.Message);
                    return Ok(templateResponse);
                }
                else
                {
                    _logger.LogError("Status: " + templateResponse.Status + ", Message:" + templateResponse.Message);
                    return StatusCode(412, templateResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while updating SMS template: " + ex.Message);
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// API to get the SMS channel data by Channel Key.
        /// </summary>
        /// <param name="channelKey"><see cref="SMSChannelDto"/></param>
        /// <returns><see cref="SMSResponseDto{T}"></see></returns>
        [HttpGet("{channelKey}")]
        public ActionResult GetSMSChannelByKey(string channelKey)
        {
            _logger.LogInformation("GetSMSChannelByKey action method.");
            _logger.LogInformation($"ChannelKey: {channelKey}");
            try
            {
                var channelResponse = _smsInteractor.GetSMSChannelByKey(channelKey);
                if (channelResponse.Status)
                {
                    _logger.LogDebug("Status: " + channelResponse.Status + ", Message:" + channelResponse.Message);
                    return Ok(channelResponse);
                }
                else
                {
                    _logger.LogError("Status: " + channelResponse.Status + ", Message:" + channelResponse.Message);
                    return StatusCode(412, channelResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while getting SMS channel by key: " + ex.Message);
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// API to get the default Channel data.
        /// </summary>
        /// <returns><see cref="SMSResponseDto{T}"></see></returns>
        [HttpGet]
        public ActionResult GetDefaultChannelKey()
        {
            _logger.LogInformation("GetSMSChannelByKey action method.");
            try
            {
                var channelResponse = _smsInteractor.GetSMSChannelByKey(defaultChannelKey);
                if (channelResponse.Status)
                {
                    _logger.LogDebug("Status: " + channelResponse.Status + ", Message:" + channelResponse.Message);
                    return Ok(channelResponse);
                }
                else
                {
                    _logger.LogError("Status: " + channelResponse.Status + ", Message:" + channelResponse.Message);
                    return StatusCode(412, channelResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while getting SMS channel by key: " + ex.Message);
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// API to get the SMS Provider data for a pool name and provider name.
        /// </summary>
        /// <param name="poolName">Pool Name</param>
        /// <param name="providerName">Provider Name</param>
        /// <returns><see cref="SMSResponseDto{T}"></see></returns>
        [HttpGet("{poolName}/{providerName}")]
        public ActionResult GetSMSProvidersByPool(string poolName, string providerName)
        {
            _logger.LogInformation("GetSMSProvidersByPool action method.");
            _logger.LogInformation($"PoolName: {poolName}, ProviderName: {providerName}");
            try
            {
                var poolResponse = _smsInteractor.GetSMSProvidersByPool(poolName, providerName);
                if (poolResponse.Status)
                {
                    _logger.LogDebug("Status: " + poolResponse.Status + ", Message:" + poolResponse.Message);
                    return Ok(poolResponse);
                }
                else
                {
                    _logger.LogError("Status: " + poolResponse.Status + ", Message:" + poolResponse.Message);
                    return StatusCode(412, poolResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while getting SMS providers by pool: " + ex.Message);
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// API to get the SMS Histories data.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <param name="tag">Tag</param>
        /// <returns><see cref="SMSResponseDto{T}"></see></returns>
        [HttpGet("{channelKey}/{tag?}")]
        public ActionResult GetSMSHistories(string channelKey, string tag = null)
        {
            _logger.LogInformation("GetSMSHistories action method.");
            _logger.LogInformation($"ChannelKey: {channelKey}, Tag: {tag}");
            try
            {
                var historiesResponse = _smsInteractor.GetSMSHistoriesByTag(channelKey, tag);
                if (historiesResponse.Status)
                {
                    _logger.LogDebug("Status: " + historiesResponse.Status + ", Message:" + historiesResponse.Message);
                    return Ok(historiesResponse);
                }
                else
                {
                    _logger.LogError("Status: " + historiesResponse.Status + ", Message:" + historiesResponse.Message);
                    return StatusCode(412, historiesResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while getting SMS histories: " + ex.Message);
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// API to get the SMS Histories data by date range.
        /// </summary>
        /// <param name="historyInput"><see cref="HistoryInput"/> model.</param>
        /// <returns><see cref="SMSResponseDto{T}"></see></returns>
        [HttpPost]
        public ActionResult GetSMSHistoriesByDateRange(HistoryInput historyInput)
        {
            _logger.LogInformation("GetSMSHistoriesByDateRange action method.");
            _logger.LogInformation($"ChannelKey: {historyInput.ChannelKey}, Tag: {historyInput.Tag}, FromDate: {historyInput.FromDate}, ToDate: {historyInput.ToDate}");
            SMSResponseDto<List<SMSHistoryDto>> historiesResponse = new SMSResponseDto<List<SMSHistoryDto>>();
            try
            {
                if (string.IsNullOrEmpty(historyInput.ChannelKey))
                {
                    historiesResponse.Status = false;
                    historiesResponse.Message = "Channel Key cannot be null or empty.";
                    return StatusCode(412, historiesResponse);
                }
                if(string.IsNullOrEmpty(historyInput.FromDate) || string.IsNullOrEmpty(historyInput.ToDate))
                {
                    historiesResponse.Status = false;
                    historiesResponse.Message = "From Date and To Date cannot be null or empty.";
                    return StatusCode(412, historiesResponse);
                }

                historiesResponse = _smsInteractor.GetSMSHistoriesByDateRange(historyInput.ChannelKey, historyInput.Tag, historyInput.FromDate, historyInput.ToDate);
                if (historiesResponse.Status)
                {
                    _logger.LogDebug("Status: " + historiesResponse.Status + ", Message:" + historiesResponse.Message);
                    return Ok(historiesResponse);
                }
                else
                {
                    _logger.LogError("Status: " + historiesResponse.Status + ", Message:" + historiesResponse.Message);
                    return StatusCode(412, historiesResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while getting SMS histories by date range: " + ex.Message);
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// API to send SMS.
        /// </summary>
        /// <param name="smsInputs"><see cref="SMSInput"/></param>
        /// <returns><see cref="SMSResponseDto{T}"></see></returns>
        [HttpPost]
        public ActionResult SendSMS(SMSInput smsInputs)
        {
            _logger.LogInformation("SendSMS action method.");
            _logger.LogInformation($"ChannelKey: {smsInputs.ChannelKey}, Tag: {smsInputs.Tag},TemplateName: {smsInputs.TemplateName}, Recipients: {smsInputs.Recipients}");
            try
            {
                var mappedInput = _mapper.Map<SMSDto>(smsInputs);
                var smsResponse = _smsInteractor.SendSMS(mappedInput);
                if (smsResponse.Status)
                {
                    _logger.LogDebug("Status: " + smsResponse.Status + ", Message:" + smsResponse.Message);
                    return Ok(smsResponse);
                }
                else
                {
                    _logger.LogError("Status: " + smsResponse.Status + ", Message:" + smsResponse.Message);
                    return StatusCode(412, smsResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while sending SMS: " + ex.Message);
                return StatusCode(500, ex);
            }
        }
    }
}