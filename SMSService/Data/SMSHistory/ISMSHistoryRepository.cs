using SMSService.Data.Entities;
using SMSService.DTOS;
using System.Collections.Generic;

namespace SMSService.Data.SMSHistory
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

        /// <summary>
        /// Gets the sms history in between 2 date ranges
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <param name="tag">Tag</param>
        /// <param name="fromDate">From Date</param>
        /// <param name="toDate">To Date</param>
        /// <returns><see cref="SMSResponseDto{T}"/> model</returns>
        SMSResponseDto<List<SMSHistoryTable>> GetSMSHistoriesByDateRange(string channelKey, string tag, string fromDate, string toDate);
    }
}