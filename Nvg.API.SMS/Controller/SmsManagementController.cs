using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nvg.SMSService.DTOS;
using Nvg.SMSService.SMS;

namespace Nvg.API.SMS.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SmsManagementController : ControllerBase
    {
        private readonly ISMSManagementInteractor _smsManagementInteractor;
        private readonly ILogger<SmsManagementController> _logger;

        public SmsManagementController(ISMSManagementInteractor smsManagementInteractor, ILogger<SmsManagementController> logger)
        {
            _smsManagementInteractor = smsManagementInteractor;
            _logger = logger;
        }

        #region SMS Pool
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

        [HttpPost]
        public ActionResult UpdateSMSPool(SMSPoolDto poolInput)
        {
            _logger.LogInformation("UpdateSMSPool action method.");
            try
            {
                var poolResponse = _smsManagementInteractor.UpdateSMSPool(poolInput);
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
                _logger.LogError("Internal server error: Error occurred while updating sms pool: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet("{poolID}")]
        public ActionResult DeleteSMSPool(string poolID)
        {
            _logger.LogInformation("DeleteSMSPool action method.");
            _logger.LogDebug("Pool ID: " + poolID);
            try
            {
                var poolResponse = _smsManagementInteractor.DeleteSMSPool(poolID);
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
                _logger.LogError("Internal server error: Error occurred while deleting sms pool: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }
        #endregion

        #region SMS Provider
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

        [HttpPost]
        public ActionResult AddSMSProvider(SMSProviderSettingsDto providerInput)
        {
            _logger.LogInformation("AddSMSProvider action method.");
            _logger.LogDebug("Pool Name: " + providerInput.SMSPoolName);
            try
            {
                var providerResponse = _smsManagementInteractor.AddUpdateSMSProvider(providerInput);
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
                _logger.LogError("Internal server error: Error occurred while updating sms providers: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        public ActionResult UpdateSMSProvider(SMSProviderSettingsDto providerInput)
        {
            _logger.LogInformation("UpdateSMSProvider action method.");
            _logger.LogDebug("Pool Name: " + providerInput.SMSPoolName);
            try
            {
                var providerResponse = _smsManagementInteractor.AddUpdateSMSProvider(providerInput);
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
                _logger.LogError("Internal server error: Error occurred while updating sms providers: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet("{providerID}")]
        public ActionResult DeleteSMSProvider(string providerID)
        {
            _logger.LogInformation("DeleteSMSProvider action method.");
            _logger.LogDebug("Provider ID: " + providerID);
            try
            {
                var providerResponse = _smsManagementInteractor.DeleteSMSProvider(providerID);
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
                _logger.LogError("Internal server error: Error occurred while deleting sms provider: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }
        #endregion

        #region SMS Channel
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

        [HttpPost]
        public ActionResult AddSMSChannel(SMSChannelDto channelInput)
        {
            _logger.LogInformation("AddSMSChannel action method.");
            _logger.LogDebug("Pool Name: " + channelInput.SMSPoolName);
            try
            {
                var channelResponse = _smsManagementInteractor.AddUpdateSMSChannel(channelInput);
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
                _logger.LogError("Internal server error: Error occurred while adding sms channel: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        public ActionResult UpdateSMSChannel(SMSChannelDto channelInput)
        {
            _logger.LogInformation("UpdateSMSChannel action method.");
            _logger.LogDebug("Pool Name: " + channelInput.SMSPoolName);
            try
            {
                var channelResponse = _smsManagementInteractor.AddUpdateSMSChannel(channelInput);
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
                _logger.LogError("Internal server error: Error occurred while updating sms channel: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet("{channelID}")]
        public ActionResult DeleteSMSChannel(string channelID)
        {
            _logger.LogInformation("DeleteSMSChannel action method.");
            _logger.LogDebug("Channel ID: " + channelID);
            try
            {
                var channelResponse = _smsManagementInteractor.DeleteSMSChannel(channelID);
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
                _logger.LogError("Internal server error: Error occurred while deleting sms channel: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }
        #endregion

        #region SMS Template
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

        [HttpPost]
        public ActionResult AddSMSTemplate(SMSTemplateDto templateInput)
        {
            _logger.LogInformation("AddSMSTemplate action method.");
            _logger.LogDebug("Pool ID: " + templateInput.SMSPoolID);
            try
            {
                var templateResponse = _smsManagementInteractor.AddUpdateSMSTemplate(templateInput);
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
                _logger.LogError("Internal server error: Error occurred while adding sms template: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        public ActionResult UpdateSMSTemplate(SMSTemplateDto templateInput)
        {
            _logger.LogInformation("UpdateSMSTemplate action method.");
            _logger.LogDebug("Pool ID: " + templateInput.SMSPoolID);
            try
            {
                var templateResponse = _smsManagementInteractor.AddUpdateSMSTemplate(templateInput);
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
                _logger.LogError("Internal server error: Error occurred while updating sms template: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet("{templateID}")]
        public ActionResult DeleteSMSTemplate(string templateID)
        {
            _logger.LogInformation("DeleteSMSTemplate action method.");
            _logger.LogDebug("Tempalte ID: " + templateID);
            try
            {
                var templateResponse = _smsManagementInteractor.DeleteSMSTemplate(templateID);
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
                _logger.LogError("Internal server error: Error occurred while deleting sms template: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }
        #endregion

        #region SMS histores
        /// <summary>
        /// API to get the sms history by channel id and tag.
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

    }
}
