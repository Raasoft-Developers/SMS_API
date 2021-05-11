using Nvg.SMSService.DTOS;

namespace Nvg.SMSService.SMSPool
{
    public interface ISMSPoolInteractor
    {
        SMSResponseDto<SMSPoolDto> AddSMSPool(SMSPoolDto smsPoolInput);
    }
}
