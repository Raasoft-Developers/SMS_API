using Nvg.SMSService.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.SMSService
{
    public interface ISMSInteractor
    {
        void SendSMS(SMSDto smsInputs);
    }
}
