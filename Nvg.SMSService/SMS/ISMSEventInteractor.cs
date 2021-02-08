using Nvg.SMSService.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.SMSService.SMS
{
    public interface ISMSEventInteractor
    {
        void SendSMS(SMSDto smsInputs);
    }
}
