using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nvg.SMSService;
using Nvg.SMSService.DTOS;

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
        public ActionResult SendSMS(SMSDto smsInputs)
        {
            CustomeResponse<string> response = new CustomeResponse<string>();
            _smsInteractor.SendSMS(smsInputs);
            response.Status = true;
            response.Message = $"SMS is sent successfully to {smsInputs.To} ";
            response.Result = "SENT";
            return Ok(response);
        }
    }
}