using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nvg.SMSService.Data;
using Nvg.SMSService.Data.Entities;
using Nvg.SMSService.DTOS;
using Nvg.SMSService.SMS;

namespace Nvg.API.SMS.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SmsController : ControllerBase
    {
        private readonly ISMSInteractor _smsInteractor;
        private readonly ILogger<SmsController> _logger;

        public SmsController(ISMSInteractor smsInteractor, ILogger<SmsController> logger)
        {
            _smsInteractor = smsInteractor;
            _logger = logger;
        }

        [HttpPost]
        public ActionResult AddSMSPool(SMSPoolDto poolInput)
        {
            _logger.LogInformation("In SmsController: AddSMSPool action method hit.");
            try
            {
                var poolResponse = _smsInteractor.AddSMSPool(poolInput);
                if (poolResponse.Status)
                {
                    _logger.LogDebug("In SmsController: " + poolResponse.Message);
                    return Ok(poolResponse);
                }
                else
                {
                    _logger.LogError("In SmsController: " + poolResponse.Message);
                    return StatusCode(412, poolResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("In SmsController: Internal server error: Error occurred while adding SMS pool: " + ex.Message);
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public ActionResult AddSMSProvider(SMSProviderSettingsDto providerInput)
        {
            _logger.LogInformation("In SmsController: AddSMSProvider action method hit.");
            try
            {
                var providerResponse = _smsInteractor.AddSMSProvider(providerInput);
                if (providerResponse.Status)
                {
                    _logger.LogDebug("In SmsController: " + providerResponse.Message);
                    return Ok(providerResponse);
                }
                else
                {
                    _logger.LogError("In SmsController: " + providerResponse.Message);
                    return StatusCode(412, providerResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("In SmsController: Internal server error: Error occurred while adding SMS provider: " + ex.Message);
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public ActionResult AddSMSChannel(SMSChannelDto channelInput)
        {
            _logger.LogInformation("In SmsController: AddSMSChannel action method hit.");
            try
            {
                var channelResponse = _smsInteractor.AddSMSChannel(channelInput);
                if (channelResponse.Status)
                {
                    _logger.LogDebug("In SmsController: " + channelResponse.Message);
                    return Ok(channelResponse);
                }
                else
                {
                    _logger.LogError("In SmsController: " + channelResponse.Message);
                    return StatusCode(412, channelResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("In SmsController: Internal server error: Error occurred while adding SMS channel: " + ex.Message);
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public ActionResult AddSMSTemplate(SMSTemplateDto templateInput)
        {
            _logger.LogInformation("In SmsController: AddSMSTemplate action method hit.");
            try
            {
                var templateResponse = _smsInteractor.AddSMSTemplate(templateInput);
                if (templateResponse.Status)
                {
                    _logger.LogDebug("In SmsController: " + templateResponse.Message);
                    return Ok(templateResponse);
                }
                else
                {
                    _logger.LogError("In SmsController: " + templateResponse.Message);
                    return StatusCode(412, templateResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("In SmsController: Internal server error: Error occurred while adding SMS template: " + ex.Message);
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{channelKey}")]
        public ActionResult GetSMSChannelByKey(string channelKey)
        {
            _logger.LogInformation("In SmsController: GetSMSChannelByKey action method hit.");
            try
            {
                var channelResponse = _smsInteractor.GetSMSChannelByKey(channelKey);
                if (channelResponse.Status)
                {
                    _logger.LogDebug("In SmsController: " + channelResponse.Message);
                    return Ok(channelResponse);
                }
                else
                {
                    _logger.LogError("In SmsController: " + channelResponse.Message);
                    return StatusCode(412, channelResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("In SmsController: Internal server error: Error occurred while getting SMS channel by key: " + ex.Message);
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{poolName}/{providerName}")]
        public ActionResult GetSMSProvidersByPool(string poolName, string providerName)
        {
            _logger.LogInformation("In SmsController: GetSMSProvidersByPool action method hit.");
            try
            {
                var poolResponse = _smsInteractor.GetSMSProvidersByPool(poolName, providerName);
                if (poolResponse.Status)
                {
                    _logger.LogDebug("In SmsController: " + poolResponse.Message);
                    return Ok(poolResponse);
                }
                else
                {
                    _logger.LogError("In SmsController: " + poolResponse.Message);
                    return StatusCode(412, poolResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("In SmsController: Internal server error: Error occurred while getting SMS providers by pool: " + ex.Message);
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{channelKey}/{tag?}")]
        public ActionResult GetSMSHistories(string channelKey, string tag = null)
        {
            _logger.LogInformation("In SmsController: GetSMSHistories action method hit.");
            try
            {
                var historiesResponse = _smsInteractor.GetSMSHistoriesByTag(channelKey, tag);
                if (historiesResponse.Status)
                {
                    _logger.LogDebug("In SmsController: " + historiesResponse.Message);
                    return Ok(historiesResponse);
                }
                else
                {
                    _logger.LogError("In SmsController: " + historiesResponse.Message);
                    return StatusCode(412, historiesResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("In SmsController: Internal server error: Error occurred while getting SMS histories: " + ex.Message);
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public ActionResult SendSMS(SMSDto smsInputs)
        {
            _logger.LogInformation("In SmsController: SendSMS action method hit.");
            try
            {
                var smsResponse = _smsInteractor.SendSMS(smsInputs);
            if (smsResponse.Status)
                {
                    _logger.LogDebug("In SmsController: " + smsResponse.Message);
                    return Ok(smsResponse);
                }
            else
                {
                    _logger.LogError("In SmsController: " + smsResponse.Message);
                    return StatusCode(412, smsResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("In SmsController: Internal server error: Error occurred while sending SMS: " + ex.Message);
                return StatusCode(500, ex);
            }
        }
    }
}