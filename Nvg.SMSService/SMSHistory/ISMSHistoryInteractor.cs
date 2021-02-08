using Nvg.SMSService.DTOS;

namespace Nvg.SMSService.SMSHistory
{
    public interface ISMSHistoryInteractor
    {
        SMSResponseDto<SMSHistoryDto> AddSMSHistory(SMSHistoryDto historyInput);
    }
}