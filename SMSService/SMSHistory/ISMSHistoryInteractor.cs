using SMSService.DTOS;
using System.Collections.Generic;

namespace SMSService.SMSHistory
{
    public interface ISMSHistoryInteractor
    {
        /// <summary>
        /// Adds the SMS History in the database.
        /// </summary>
        /// <param name="historyInput"><see cref="SMSHistoryDto"/> model</param>
        /// <returns><see cref="SMSResponseDto{T}"/> model</returns>
        SMSResponseDto<SMSHistoryDto> AddSMSHistory(SMSHistoryDto historyInput);

        /// <summary>
        /// Gets the SMS History data.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <param name="tag">Tag</param>
        /// <returns><see cref="SMSResponseDto{T}"/> model</returns>
        SMSResponseDto<List<SMSHistoryDto>> GetSMSHistoriesByTag(string channelKey, string tag);

        /// <summary>
        /// Gets the SMS History data by date range.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <param name="tag">Tag</param>
        /// <param name="fromDate">From date</param>
        /// <param name="toDate">To Date</param>
        /// <returns><see cref="SMSResponseDto{T}"/> model</returns>
        SMSResponseDto<List<SMSHistoryDto>> GetSMSHistoriesByDateRange(string channelKey, string tag, string fromDate, string toDate);

    }
}