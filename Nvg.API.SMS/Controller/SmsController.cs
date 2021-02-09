using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public SmsController(ISMSInteractor smsInteractor)
        {
            _smsInteractor = smsInteractor;
        }

        [HttpPost]
        public ActionResult AddSMSPool(SMSPoolDto poolInput)
        {
            var poolResponse = _smsInteractor.AddSMSPool(poolInput);
            if (poolResponse.Status)
                return Ok(poolResponse);
            else
                return StatusCode(412, poolResponse);
        }

        [HttpPost]
        public ActionResult AddSMSProvider(SMSProviderSettingsDto providerInput)
        {
            var providerResponse = _smsInteractor.AddSMSProvider(providerInput);
            if (providerResponse.Status)
                return Ok(providerResponse);
            else
                return StatusCode(412, providerResponse);
        }

        [HttpPost]
        public ActionResult AddSMSChannel(SMSChannelDto channelInput)
        {
            var channelResponse = _smsInteractor.AddSMSChannel(channelInput);
            if (channelResponse.Status)
                return Ok(channelResponse);
            else
                return StatusCode(412, channelResponse);
        }

        [HttpPost]
        public ActionResult AddSMSTemplate(SMSTemplateDto templateInput)
        {
            var templateResponse = _smsInteractor.AddSMSTemplate(templateInput);
            if (templateResponse.Status)
                return Ok(templateResponse);
            else
                return StatusCode(412, templateResponse);
        }

        [HttpGet]
        public ActionResult GetSMSChannelByKey(string channelKey)
        {
            var channelResponse = _smsInteractor.GetSMSChannelByKey(channelKey);
            if (channelResponse.Status)
                return Ok(channelResponse);
            else
                return StatusCode(412, channelResponse);
        }

        [HttpGet]
        public ActionResult GetSMSProvidersByPool(string poolName, string providerName)
        {
            var poolResponse = _smsInteractor.GetSMSProvidersByPool(poolName, providerName);
            if (poolResponse.Status)
                return Ok(poolResponse);
            else
                return StatusCode(412, poolResponse);
        }

        [HttpGet]
        public ActionResult GetSMSHistories(string channelKey, string tag = null)
        {
            var historiesResponse = _smsInteractor.GetSMSHistoriesByTag(channelKey, tag);
            if (historiesResponse.Status)
                return Ok(historiesResponse);
            else
                return StatusCode(412, historiesResponse);
        }

        [HttpPost]
        public ActionResult SendSMS(SMSDto smsInputs)
        {
            var smsResponse = _smsInteractor.SendSMS(smsInputs);
            if(smsResponse.Status)
                return Ok(smsResponse);
            else
                return StatusCode(412, smsResponse);
        }
    }
}