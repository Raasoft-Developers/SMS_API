using Nvg.SMSService.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.SMSService.SMSPool
{
    public interface ISMSPoolInteractor
    {
        SMSResponseDto<SMSPoolDto> AddSMSPool(SMSPoolDto smsPoolInput);
    }
}
