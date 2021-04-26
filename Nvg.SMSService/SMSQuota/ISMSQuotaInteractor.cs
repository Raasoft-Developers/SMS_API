using Nvg.SMSService.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.SMSService.SMSQuota
{
    public interface ISMSQuotaInteractor
    {
        /// <summary>
        /// Gets the SMS quota by channel key.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <returns><see cref="SMSResponseDto{SMSQuotaDto}"/></returns>
        SMSResponseDto<SMSQuotaDto> GetSMSQuota(string channelKey);

        /// <summary>
        /// Updates the sms quota.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <returns><see cref="SMSResponseDto{SMSQuotaDto}"/></returns>
        SMSResponseDto<SMSQuotaDto> UpdateSMSQuota(string channelKey);

        /// <summary>
        /// Check if SMS Quota of the channel is exceeded.Status is set to true or false based on value
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <returns><see cref="bool"/></returns>
        bool CheckIfQuotaExceeded(string channelKey);


        /// <summary>
        /// Adds the SMS Quota Values for Channel.
        /// </summary>
        /// <param name="smsChannel">SMS Channel </param>
        /// <returns><see cref="SMSResponseDto{SMSQuotaDto}"/></returns>
        SMSResponseDto<SMSQuotaDto> AddSMSQuota(SMSChannelDto smsChannel);



    }
}
