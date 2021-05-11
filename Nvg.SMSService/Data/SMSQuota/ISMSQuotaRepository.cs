using Nvg.SMSService.Data.Entities;
using Nvg.SMSService.DTOS;

namespace Nvg.SMSService.Data.SMSQuota
{
    public interface ISMSQuotaRepository
    {
        SMSResponseDto<SMSQuotaTable> GetSMSQuota(string channelKey);
        SMSResponseDto<SMSQuotaTable> UpdateSMSQuota(string channelID);
    }
}
