using Nvg.SMSService.Data.Entities;
using Nvg.SMSService.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.SMSService.Data.SMSQuota
{
    public interface ISMSQuotaRepository
    {
        /// <summary>
        /// Gets the SMS Quota by channel key.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <returns><see cref="SMSResponseDto{SMSQuotaTable}"/></returns>
        SMSResponseDto<SMSQuotaTable> GetSMSQuota(string channelKey);

        /// <summary>
        /// Updates the Consumption value in SMS Quota .
        /// </summary>
        /// <param name="channelID">Channel Id</param>
        /// <returns><see cref="SMSResponseDto{SMSQuotaTable}"/></returns>
        SMSResponseDto<SMSQuotaTable> UpdateSMSQuota(string channelID);

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
