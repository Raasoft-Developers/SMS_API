using Nvg.SMSService.Data.Entities;
using Nvg.SMSService.DTOS;
using System.Collections.Generic;

namespace Nvg.SMSService.Data.SMSHistory
{
    public interface ISMSHistoryRepository
    {
        /// <summary>
        /// Adds the SMS history into the database.
        /// </summary>
        /// <param name="historyInput"><see cref="SMSHistoryTable"/> model</param>
        /// <returns><see cref="SMSResponseDto{T}"/> model</returns>
        SMSResponseDto<SMSHistoryTable> AddSMSHistory(SMSHistoryTable historyInput);

        /// <summary>
        /// Gets the SMS history by tag.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <param name="tag">Tag</param>
        /// <returns><see cref="SMSResponseDto{T}"/> model</returns>
        SMSResponseDto<List<SMSHistoryTable>> GetSMSHistoriesByTag(string channelKey, string tag);
    }
}