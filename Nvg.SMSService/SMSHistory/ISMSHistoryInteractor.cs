using Nvg.SMSService.DTOS;
using System.Collections.Generic;

namespace Nvg.SMSService.SMSHistory
{
    public interface ISMSHistoryInteractor
    {
        SMSResponseDto<SMSHistoryDto> AddSMSHistory(SMSHistoryDto historyInput);
        SMSResponseDto<List<SMSHistoryDto>> GetSMSHistoriesByTag(string channelKey, string tag);
    }
}