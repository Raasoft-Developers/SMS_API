using SMSService.DTOS;

namespace SMSService.SMSQuota
{
    public interface ISMSQuotaInteractor
    {
        /// <summary>
        /// Gets the SMS Quota.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <returns><see cref="SMSResponseDto{T}"/> model</returns>
        SMSResponseDto<SMSQuotaDto> GetSMSQuota(string channelKey);

        /// <summary>
        /// Updates the Consumption value of the SMS Quota.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <param name="creditsUsed">Credits used for sending sms</param>
        /// <returns><see cref="SMSResponseDto{SMSQuotaDto}"/></returns>
        SMSResponseDto<SMSQuotaDto> IncrementSMSQuota(string channelKey, long creditsUsed);

        /// <summary>
        /// Check if SMS Quota of the channel is exceeded.Status is set to true or false based on value
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <returns><see cref="SMSBalanceDto"/></returns>
        SMSBalanceDto CheckIfQuotaExceeded(string channelKey);


        /// <summary>
        /// Adds the SMS Quota Values for Channel.
        /// </summary>
        /// <param name="smsChannel">SMS Channel </param>
        /// <returns><see cref="SMSResponseDto{SMSQuotaDto}"/></returns>
        SMSResponseDto<SMSQuotaDto> AddSMSQuota(SMSChannelDto smsChannel);

        /// <summary>
        /// Updates the sms quota.
        /// </summary>
        /// <param name="smsChannel">SMS Channel </param>
        /// <returns><see cref="SMSResponseDto{SMSQuotaDto}"/></returns>
        SMSResponseDto<SMSQuotaDto> UpdateSMSQuota(SMSChannelDto smsChannel);




    }
}
