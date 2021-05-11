using Nvg.SMSService.DTOS;

namespace Nvg.SMSService.SMSQuota
{
    public interface ISMSQuotaInteractor
    {
        SMSResponseDto<SMSQuotaDto> GetSMSQuota(string channelKey);
        SMSResponseDto<SMSQuotaDto> UpdateSMSQuota(string channelKey);
    }
}
