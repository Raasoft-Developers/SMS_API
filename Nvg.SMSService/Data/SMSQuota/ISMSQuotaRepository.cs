using Nvg.SMSService.Data.Entities;
using Nvg.SMSService.DTOS;
using System.Collections.Generic;

namespace Nvg.SMSService.Data.SMSQuota
{
    public interface ISMSQuotaRepository
    {
        /// <summary>
        /// Gets the SMS Quota.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<SMSQuotaTable> GetSMSQuota(string channelKey);

        /// <summary>
        /// Gets the SMS Quota List.
        /// </summary>
        /// <param name="channelID">Channel ID</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<List<SMSQuotaTable>> GetSMSQuotaList(string channelID);

        /// <summary>
        /// Increments the Consumption value in SMS Quota .
        /// </summary>
        /// <param name="channelID">Channel Id</param>
        /// <returns><see cref="SMSResponseDto{SMSQuotaTable}"/></returns>
        SMSResponseDto<SMSQuotaTable> IncrementSMSQuota(string channelID);

        /// <summary>
        /// Updates the SMS Quota.
        /// </summary>
        /// <param name="smsChannel">SMS Channel </param>
        /// <returns><see cref="SMSResponseDto{SMSQuotaTable}"/></returns>
        SMSResponseDto<SMSQuotaTable> UpdateSMSQuota(SMSChannelDto smsChannel);

        /// <summary>
        /// Adds the SMS Quota Values for Channel.
        /// </summary>
        /// <param name="smsChannel">SMS Channel </param>
        /// <returns><see cref="SMSResponseDto{SMSQuotaTable}"/></returns>
        SMSResponseDto<SMSQuotaTable> AddSMSQuota(SMSChannelDto smsChannel);


        /// <summary>
        /// Updates the Current Month of the Channel's Quota.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <param name="currentMonth">Current Month</param>
        /// <returns><see cref="SMSResponseDto{SMSQuotaTable}"/></returns>
        SMSResponseDto<SMSQuotaTable> UpdateCurrentMonth(string channelKey, string currentMonth);
        
        /// <summary>
        /// Deletes the SMS Quota Values from the SMS Channel ID.
        /// </summary>
        /// <param name="channelID">Channel Id</param>
        /// <returns><see cref="SMSResponseDto{SMSQuotaTable}"/></returns>
        SMSResponseDto<string> DeleteSMSQuota(string channelID);

    }
}
