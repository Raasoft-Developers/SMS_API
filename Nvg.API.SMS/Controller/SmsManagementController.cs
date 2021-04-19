using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nvg.SMSService.DTOS;
using Nvg.SMSService.SMS;

namespace Nvg.API.SMS.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SmsManagementController : ControllerBase
    {
        private readonly ISMSManagementInteractor _smsManagementInteractor;
        private IConfiguration _config;
        private readonly ILogger<SmsManagementController> _logger;

        public SmsManagementController(ISMSManagementInteractor smsManagementInteractor, ILogger<SmsManagementController> logger, IConfiguration config)
        {
            _smsManagementInteractor = smsManagementInteractor;
            _config = config;
            _logger = logger;
        }

        #region SMS Pool

        /// <summary>
        /// API to get all the SMS Pool data.
        /// </summary>
        /// <returns><see cref="SMSResponseDto{T}"></see></returns>
        [HttpGet]
        public ActionResult GetSMSPools()
        {
            _logger.LogInformation("GetSMSPools action method.");
            try
            {
                var poolResponse = _smsManagementInteractor.GetSMSPools();
                if (poolResponse.Status)
                {
                    _logger.LogDebug("Status: " + poolResponse.Status + ", " + poolResponse.Message);
                    return Ok(poolResponse);
                }
                else
                {
                    _logger.LogError("Status: " + poolResponse.Status + ", " + poolResponse.Message);
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, poolResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while getting sms pools: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// API to get all the SMS Pool names.
        /// </summary>
        /// <returns><see cref="SMSResponseDto{T}"></see></returns>
        [HttpGet]
        public ActionResult GetSMSPoolNames()
        {
            _logger.LogInformation("GetSMSPoolNames action method.");
            try
            {
                var poolResponse = _smsManagementInteractor.GetSMSPoolNames();
                if (poolResponse.Status)
                {
                    _logger.LogDebug("Status: " + poolResponse.Status + ", " + poolResponse.Message);
                    return Ok(poolResponse);
                }
                else
                {
                    _logger.LogError("Status: " + poolResponse.Status + ", " + poolResponse.Message);
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, poolResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while getting sms pool names: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// API to Update the SMS Pool data.
        /// </summary>
        /// <param name="poolInput"><see cref="SMSPoolDto"/></param>
        /// <returns><see cref="SMSResponseDto{T}"></see></returns>
        [HttpPost]
        public ActionResult UpdateSMSPool(SMSPoolDto poolInput)
        {
            _logger.LogInformation("UpdateSMSPool action method.");
            SMSResponseDto<SMSPoolDto> poolResponse = new SMSResponseDto<SMSPoolDto>();
            try
            {
                if (!string.IsNullOrWhiteSpace(poolInput.Name))
                {
                    poolResponse = _smsManagementInteractor.UpdateSMSPool(poolInput);
                    if (poolResponse.Status)
                    {
                        _logger.LogDebug("Status: " + poolResponse.Status + ", " + poolResponse.Message);
                        return Ok(poolResponse);
                    }
                    else
                    {
                        _logger.LogError("Status: " + poolResponse.Status + ", " + poolResponse.Message);
                        return StatusCode((int)HttpStatusCode.PreconditionFailed, poolResponse);
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
                _logger.LogError("Internal server error: Error occurred while updating sms pool: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// API to delete the SMS Pool data.
        /// </summary>
        /// <param name="poolID">Pool ID</param>
        /// <returns><see cref="SMSResponseDto{T}"></see></returns>
        [HttpGet("{poolID}")]
        public ActionResult DeleteSMSPool(string poolID)
        {
            _logger.LogInformation("DeleteSMSPool action method.");
            _logger.LogDebug("Pool ID: " + poolID);
            SMSResponseDto<string> poolResponse = new SMSResponseDto<string>();
            try
            {
                if (!string.IsNullOrWhiteSpace(poolID))
                {
                    poolResponse = _smsManagementInteractor.DeleteSMSPool(poolID);
                    if (poolResponse.Status)
                    {
                        _logger.LogDebug("Status: " + poolResponse.Status + ", " + poolResponse.Message);
                        return Ok(poolResponse);
                    }
                    else
                    {
                        _logger.LogError("Status: " + poolResponse.Status + ", " + poolResponse.Message);
                        return StatusCode((int)HttpStatusCode.PreconditionFailed, poolResponse);
                    }
                }
                else
                {
                    poolResponse.Status = false;
                    poolResponse.Message = "Pool ID cannot be empty or whitespace.";
                    _logger.LogError("Status: " + poolResponse.Status + ", Message:" + poolResponse.Message);
                    return StatusCode(412, poolResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while deleting sms pool: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }
        #endregion

        #region SMS Provider
        /// <summary>
        /// API to get all the SMS Provider data.
        /// </summary>
        /// <param name="poolID">Pool ID</param>
        /// <returns><see cref="SMSResponseDto{T}"></see></returns>
        [HttpGet("{poolID}")]
        public ActionResult GetSMSProviders(string poolID)
        {
            _logger.LogInformation("GetSMSProviders action method.");
            _logger.LogDebug("Pool ID: " + poolID);
            try
            {
                var providerResponse = _smsManagementInteractor.GetSMSProviders(poolID);
                if (providerResponse.Status)
                {
                    _logger.LogDebug("Status: " + providerResponse.Status + ", " + providerResponse.Message);
                    return Ok(providerResponse);
                }
                else
                {
                    _logger.LogError("Status: " + providerResponse.Status + ", " + providerResponse.Message);
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, providerResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while getting sms providers: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// API to get all the SMS Provider names.
        /// </summary>
        /// <param name="poolID">Pool ID</param>
        /// <returns><see cref="SMSResponseDto{T}"></see></returns>
        [HttpGet("{poolID}")]
        public ActionResult GetSMSProviderNames(string poolID)
        {
            _logger.LogInformation("GetSMSProviderNames action method.");
            _logger.LogDebug("Pool Name: " + poolID);
            try
            {
                var providerResponse = _smsManagementInteractor.GetSMSProviderNames(poolID);
                if (providerResponse.Status)
                {
                    _logger.LogDebug("Status: " + providerResponse.Status + ", " + providerResponse.Message);
                    return Ok(providerResponse);
                }
                else
                {
                    _logger.LogError("Status: " + providerResponse.Status + ", " + providerResponse.Message);
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, providerResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while getting sms provider names: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// API to add SMS Provider data.
        /// </summary>
        /// <param name="providerInput"><see cref="SMSProviderSettingsDto"/></param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        [HttpPost]
        public ActionResult AddSMSProvider(SMSProviderSettingsDto providerInput)
        {
            _logger.LogInformation("AddSMSProvider action method.");
            _logger.LogDebug("Pool Name: " + providerInput.SMSPoolName);
            SMSResponseDto<SMSProviderSettingsDto> providerResponse = new SMSResponseDto<SMSProviderSettingsDto>();
            try
            {
                if (!string.IsNullOrWhiteSpace(providerInput.Name) && !string.IsNullOrWhiteSpace(providerInput.Type) && !string.IsNullOrWhiteSpace(providerInput.Configuration))
                {
                    providerResponse = _smsManagementInteractor.AddSMSProvider(providerInput);
                    if (providerResponse.Status)
                    {
                        _logger.LogDebug("Status: " + providerResponse.Status + ", " + providerResponse.Message);
                        return Ok(providerResponse);
                    }
                    else
                    {
                        _logger.LogError("Status: " + providerResponse.Status + ", " + providerResponse.Message);
                        return StatusCode((int)HttpStatusCode.PreconditionFailed, providerResponse);
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
                _logger.LogError("Internal server error: Error occurred while updating sms providers: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// API to update the SMS Provider data.
        /// </summary>
        /// <param name="providerInput"><see cref="SMSProviderSettingsDto"/></param>
        /// <returns><see cref="SMSResponseDto{T}"></see></returns>
        [HttpPost]
        public ActionResult UpdateSMSProvider(SMSProviderSettingsDto providerInput)
        {
            _logger.LogInformation("UpdateSMSProvider action method.");
            _logger.LogDebug("Pool Name: " + providerInput.SMSPoolName);
            SMSResponseDto<SMSProviderSettingsDto> providerResponse = new SMSResponseDto<SMSProviderSettingsDto>();
            try
            {
                if (!string.IsNullOrWhiteSpace(providerInput.Name) && !string.IsNullOrWhiteSpace(providerInput.Type) && !string.IsNullOrWhiteSpace(providerInput.Configuration))
                {
                    providerResponse = _smsManagementInteractor.UpdateSMSProvider(providerInput);
                    if (providerResponse.Status)
                    {
                        _logger.LogDebug("Status: " + providerResponse.Status + ", " + providerResponse.Message);
                        return Ok(providerResponse);
                    }
                    else
                    {
                        _logger.LogError("Status: " + providerResponse.Status + ", " + providerResponse.Message);
                        return StatusCode((int)HttpStatusCode.PreconditionFailed, providerResponse);
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
                _logger.LogError("Internal server error: Error occurred while updating sms providers: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// API to delete the SMS Provider data.
        /// </summary>
        /// <param name="providerID">Provider ID</param>
        /// <returns><see cref="SMSResponseDto{T}"></see></returns>
        [HttpGet("{providerID}")]
        public ActionResult DeleteSMSProvider(string providerID)
        {
            _logger.LogInformation("DeleteSMSProvider action method.");
            _logger.LogDebug("Provider ID: " + providerID);
            SMSResponseDto<string> providerResponse = new SMSResponseDto<string>();
            try
            {
                if (!string.IsNullOrWhiteSpace(providerID))
                {
                    providerResponse = _smsManagementInteractor.DeleteSMSProvider(providerID);
                    if (providerResponse.Status)
                    {
                        _logger.LogDebug("Status: " + providerResponse.Status + ", " + providerResponse.Message);
                        return Ok(providerResponse);
                    }
                    else
                    {
                        _logger.LogError("Status: " + providerResponse.Status + ", " + providerResponse.Message);
                        return StatusCode((int)HttpStatusCode.PreconditionFailed, providerResponse);
                    }
                }
                else
                {
                    providerResponse.Status = false;
                    providerResponse.Message = "ProviderID cannot be empty or whitespace.";
                    _logger.LogError("Status: " + providerResponse.Status + ", Message:" + providerResponse.Message);
                    return StatusCode(412, providerResponse);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while deleting sms provider: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }
        #endregion

        #region SMS Channel
        /// <summary>
        /// API to get all the SMS Channel data by pool ID.
        /// </summary>
        /// <param name="poolID">Pool ID</param>
        /// <returns><see cref="SMSResponseDto{T}"></see></returns>
        [HttpGet("{poolID}")]
        public ActionResult GetSMSChannelsByPool(string poolID)
        {
            _logger.LogInformation("GetSMSChannelsByPool action method.");
            _logger.LogDebug("Pool Name: " + poolID);
            try
            {
                var channelResponse = _smsManagementInteractor.GetSMSChannelsByPool(poolID);
                if (channelResponse.Status)
                {
                    _logger.LogDebug("Status: " + channelResponse.Status + ", " + channelResponse.Message);
                    return Ok(channelResponse);
                }
                else
                {
                    _logger.LogError("Status: " + channelResponse.Status + ", " + channelResponse.Message);
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, channelResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while geting sms channels: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// API to get all the SMS Channel data.
        /// </summary>
        /// <returns><see cref="SMSResponseDto{T}"></see></returns>
        [HttpGet]     
        public ActionResult GetSMSChannelKeys()
        {
            _logger.LogInformation("GetSMSChannelKeys action method.");
            try
            {
                var channelResponse = _smsManagementInteractor.GetSMSChannelKeys();
                if (channelResponse.Status)
                {
                    _logger.LogDebug("Status: " + channelResponse.Status + ", " + channelResponse.Message);
                    return Ok(channelResponse);
                }
                else
                {
                    _logger.LogError("Status: " + channelResponse.Status + ", " + channelResponse.Message);
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, channelResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while getting sms channel keys: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// API to get the SMS Template data by channel ID.
        /// </summary>
        /// <param name="channelID">Channel ID</param>
        /// <returns><see cref="SMSResponseDto{T}"></see></returns>
        [HttpGet("{channelID}")]
        public ActionResult GetSMSTemplatesByChannelID(string channelID)
        {
            _logger.LogInformation("GetSMSTemplatesByChannelID action method.");
            _logger.LogDebug("Channel ID: " + channelID);
            try
            {
                var templateResponse = _smsManagementInteractor.GetSMSTemplatesByChannelID(channelID);
                if (templateResponse.Status)
                {
                    _logger.LogDebug("Status: " + templateResponse.Status + ", " + templateResponse.Message);
                    return Ok(templateResponse);
                }
                else
                {
                    _logger.LogError("Status: " + templateResponse.Status + ", " + templateResponse.Message);
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, templateResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while getting sms templates by pool: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// API to add the SMS Channel data.
        /// </summary>
        /// <param name="channelInput"><see cref="SMSChannelDto"/></param>
        /// <returns><see cref="SMSResponseDto{T}"></see></returns>
        [HttpPost]
        public ActionResult AddSMSChannel(SMSChannelDto channelInput)
        {
            _logger.LogInformation("AddSMSChannel action method.");
            _logger.LogDebug("Pool Name: " + channelInput.SMSPoolName);
            SMSResponseDto<SMSChannelDto> channelResponse = new SMSResponseDto<SMSChannelDto>();
            try
            {
                if (!string.IsNullOrWhiteSpace(channelInput.Key))
                {
                    channelResponse = _smsManagementInteractor.AddSMSChannel(channelInput);
                    if (channelResponse.Status)
                    {
                        _logger.LogDebug("Status: " + channelResponse.Status + ", " + channelResponse.Message);
                        return Ok(channelResponse);
                    }
                    else
                    {
                        _logger.LogError("Status: " + channelResponse.Status + ", " + channelResponse.Message);
                        return StatusCode((int)HttpStatusCode.PreconditionFailed, channelResponse);
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
                _logger.LogError("Internal server error: Error occurred while adding sms channel: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// API to update the SMS Channel data.
        /// </summary>
        /// <param name="channelInput"><see cref="SMSChannelDto"/></param>
        /// <returns><see cref="SMSResponseDto{T}"></see></returns>
        [HttpPost]
        public ActionResult UpdateSMSChannel(SMSChannelDto channelInput)
        {
            _logger.LogInformation("UpdateSMSChannel action method.");
            _logger.LogDebug("Pool Name: " + channelInput.SMSPoolName);
            SMSResponseDto<SMSChannelDto> channelResponse = new SMSResponseDto<SMSChannelDto>();
            try
            {
                if (!string.IsNullOrWhiteSpace(channelInput.Key))
                {
                    channelResponse = _smsManagementInteractor.UpdateSMSChannel(channelInput);
                    if (channelResponse.Status)
                    {
                        _logger.LogDebug("Status: " + channelResponse.Status + ", " + channelResponse.Message);
                        return Ok(channelResponse);
                    }
                    else
                    {
                        _logger.LogError("Status: " + channelResponse.Status + ", " + channelResponse.Message);
                        return StatusCode((int)HttpStatusCode.PreconditionFailed, channelResponse);
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
                _logger.LogError("Internal server error: Error occurred while updating sms channel: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// API to delete the SMS Channel data.
        /// </summary>
        /// <param name="channelID">Channel ID</param>
        /// <returns><see cref="SMSResponseDto{T}"></see></returns>
        [HttpGet("{channelID}")]
        public ActionResult DeleteSMSChannel(string channelID)
        {
            _logger.LogInformation("DeleteSMSChannel action method.");
            _logger.LogDebug("Channel ID: " + channelID);
            SMSResponseDto<string> channelResponse = new SMSResponseDto<string>();
            try
            {
                if (!string.IsNullOrWhiteSpace(channelID))
                {
                    channelResponse = _smsManagementInteractor.DeleteSMSChannel(channelID);
                    if (channelResponse.Status)
                    {
                        _logger.LogDebug("Status: " + channelResponse.Status + ", " + channelResponse.Message);
                        return Ok(channelResponse);
                    }
                    else
                    {
                        _logger.LogError("Status: " + channelResponse.Status + ", " + channelResponse.Message);
                        return StatusCode((int)HttpStatusCode.PreconditionFailed, channelResponse);
                    }
                }
                else
                {
                    channelResponse.Status = false;
                    channelResponse.Message = "Channel ID cannot be empty or whitespace.";
                    _logger.LogError("Status: " + channelResponse.Status + ", Message:" + channelResponse.Message);
                    return StatusCode(412, channelResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while deleting sms channel: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }
        #endregion

        #region SMS Template
        /// <summary>
        /// API to get the SMS Template data by pool ID.
        /// </summary>
        /// <param name="poolID">Pool ID</param>
        /// <returns><see cref="SMSResponseDto{T}"></see></returns>
        [HttpGet("{poolID}")]
        public ActionResult GetSMSTemplatesByPool(string poolID)
        {
            _logger.LogInformation("GetSMSTemplatesByPool action method.");
            _logger.LogDebug("Pool Name: " + poolID);
            try
            {
                var templateResponse = _smsManagementInteractor.GetSMSTemplatesByPool(poolID);
                if (templateResponse.Status)
                {
                    _logger.LogDebug("Status: " + templateResponse.Status + ", " + templateResponse.Message);
                    return Ok(templateResponse);
                }
                else
                {
                    _logger.LogError("Status: " + templateResponse.Status + ", " + templateResponse.Message);
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, templateResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while getting sms templates by pool: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// API to get the SMS Template data by Template ID.
        /// </summary>
        /// <param name="templateID">Template ID</param>
        /// <returns><see cref="SMSResponseDto{T}"></see></returns>
        [HttpGet("{templateID}")]
        public ActionResult GetSMSTemplateByID(string templateID)
        {
            _logger.LogInformation("GetSMSTemplateByID action method.");
            _logger.LogDebug("Template ID: " + templateID);
            try
            {
                var templateResponse = _smsManagementInteractor.GetSMSTemplate(templateID);
                if (templateResponse.Status)
                {
                    _logger.LogDebug("Status: " + templateResponse.Status + ", " + templateResponse.Message);
                    return Ok(templateResponse);
                }
                else
                {
                    _logger.LogError("Status: " + templateResponse.Status + ", " + templateResponse.Message);
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, templateResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while getting sms templates by id: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// API to add the SMS Template data.
        /// </summary>
        /// <param name="templateInput"><see cref="SMSTemplateDto"/></param>
        /// <returns><see cref="SMSResponseDto{T}"></see></returns>
        [HttpPost]
        public ActionResult AddSMSTemplate(SMSTemplateDto templateInput)
        {
            _logger.LogInformation("AddSMSTemplate action method.");
            _logger.LogDebug("Pool ID: " + templateInput.SMSPoolID);
            SMSResponseDto<SMSTemplateDto> templateResponse = new SMSResponseDto<SMSTemplateDto>();
            try
            {
                if (!string.IsNullOrWhiteSpace(templateInput.MessageTemplate) && !string.IsNullOrWhiteSpace(templateInput.Name))
                {
                    templateResponse = _smsManagementInteractor.AddSMSTemplate(templateInput);
                    if (templateResponse.Status)
                    {
                        _logger.LogDebug("Status: " + templateResponse.Status + ", " + templateResponse.Message);
                        return Ok(templateResponse);
                    }
                    else
                    {
                        _logger.LogError("Status: " + templateResponse.Status + ", " + templateResponse.Message);
                        return StatusCode((int)HttpStatusCode.PreconditionFailed, templateResponse);
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
                _logger.LogError("Internal server error: Error occurred while adding sms template: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// API to update the SMS Template data.
        /// </summary>
        /// <param name="templateInput"><see cref="SMSTemplateDto"/></param>
        /// <returns><see cref="SMSResponseDto{T}"></see></returns>
        [HttpPost]
        public ActionResult UpdateSMSTemplate(SMSTemplateDto templateInput)
        {
            _logger.LogInformation("UpdateSMSTemplate action method.");
            _logger.LogDebug("Pool ID: " + templateInput.SMSPoolID);
            SMSResponseDto<SMSTemplateDto> templateResponse = new SMSResponseDto<SMSTemplateDto>();
            try
            {
                if (!string.IsNullOrWhiteSpace(templateInput.MessageTemplate) && !string.IsNullOrWhiteSpace(templateInput.Name))
                {
                    templateResponse = _smsManagementInteractor.UpdateSMSTemplate(templateInput);
                    if (templateResponse.Status)
                    {
                        _logger.LogDebug("Status: " + templateResponse.Status + ", " + templateResponse.Message);
                        return Ok(templateResponse);
                    }
                    else
                    {
                        _logger.LogError("Status: " + templateResponse.Status + ", " + templateResponse.Message);
                        return StatusCode((int)HttpStatusCode.PreconditionFailed, templateResponse);
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
                _logger.LogError("Internal server error: Error occurred while updating sms template: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// API to delete the SMS Template data.
        /// </summary>
        /// <param name="templateID">Template ID</param>
        /// <returns><see cref="SMSResponseDto{T}"></see></returns>
        [HttpGet("{templateID}")]
        public ActionResult DeleteSMSTemplate(string templateID)
        {
            _logger.LogInformation("DeleteSMSTemplate action method.");
            _logger.LogDebug("Tempalte ID: " + templateID);
            SMSResponseDto<string> templateResponse = new SMSResponseDto<string>();
            try
            {
                if (!string.IsNullOrWhiteSpace(templateID))
                {
                    templateResponse = _smsManagementInteractor.DeleteSMSTemplate(templateID);
                    if (templateResponse.Status)
                    {
                        _logger.LogDebug("Status: " + templateResponse.Status + ", " + templateResponse.Message);
                        return Ok(templateResponse);
                    }
                    else
                    {
                        _logger.LogError("Status: " + templateResponse.Status + ", " + templateResponse.Message);
                        return StatusCode((int)HttpStatusCode.PreconditionFailed, templateResponse);
                    }
                }
                else
                {
                    templateResponse.Status = false;
                    templateResponse.Message = "Template ID cannot be empty or whitespace.";
                    _logger.LogError("Status: " + templateResponse.Status + ", Message:" + templateResponse.Message);
                    return StatusCode(412, templateResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while deleting sms template: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }
        #endregion

        #region SMS histores
        /// <summary>
        /// API to get the sms histories data.
        /// </summary>
        /// <param name="channelID">Channel ID</param>
        /// <param name="tag">Tag</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        [HttpGet("{channelID}/{tag?}")]
        public ActionResult GetSMSHistories(string channelID, string tag = null)
        {
            _logger.LogInformation("GetSMSHistories action method.");
            _logger.LogInformation($"ChannelKey: {channelID}, Tag: {tag}");
            try
            {
                var historiesResponse = _smsManagementInteractor.GetSMSHistories(channelID, tag);
                if (historiesResponse.Status)
                {
                    _logger.LogDebug("Status: " + historiesResponse.Status + ", " + historiesResponse.Message);
                    return Ok(historiesResponse);
                }
                else
                {
                    _logger.LogError("Status: " + historiesResponse.Status + ", " + historiesResponse.Message);
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, historiesResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while trying to get sms histories: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }
        #endregion

        /// <summary>
        /// API to get the SMS API document URL.
        /// </summary>
        /// <returns><see cref="SMSResponseDto{T}"></see></returns>
        [HttpGet]
        public IActionResult GetApiDocumentUrl()
        {
            CustomResponse<string> response = new CustomResponse<string>();
            string url = _config.GetSection("apiDocumentDownloadUrl").Value;
            response.Status = true;
            response.Message = "Retrieved URL.";
            response.Result = url;
            return Ok(response);
        }
    }
}
