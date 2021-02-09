using Nvg.SMSService.Data.Entities;
using Nvg.SMSService.DTOS;
using System.Collections.Generic;

namespace Nvg.SMSService.Data.SMSHistory
{
    public interface ISMSHistoryRepository
    {
        SMSResponseDto<SMSHistoryTable> AddSMSHistory(SMSHistoryTable historyInput);
        SMSResponseDto<List<SMSHistoryTable>> GetSMSHistoriesByTag(string channelKey, string tag);
    }
}