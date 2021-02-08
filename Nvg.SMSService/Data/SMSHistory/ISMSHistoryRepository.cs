using Nvg.SMSService.Data.Entities;
using Nvg.SMSService.DTOS;

namespace Nvg.SMSService.Data.SMSHistory
{
    public interface ISMSHistoryRepository
    {
        SMSResponseDto<SMSHistoryTable> AddSMSHistory(SMSHistoryTable historyInput);
    }
}