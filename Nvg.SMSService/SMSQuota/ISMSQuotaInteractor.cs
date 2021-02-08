using Nvg.SMSService.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.SMSService.SMSQuota
{
    public interface ISMSQuotaInteractor
    {
        SMSResponseDto<SMSQuotaDto> GetSMSQuota(string channelKey);
        SMSResponseDto<SMSQuotaDto> UpdateSMSQuota(string channelKey);
    }
}
