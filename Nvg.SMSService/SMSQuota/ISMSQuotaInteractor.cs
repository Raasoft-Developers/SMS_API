using Nvg.SMSService.DTOS;

namespace Nvg.SMSService.SMSQuota
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
        /// Updates the SMS Quota.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <returns><see cref="SMSResponseDto{T}"/> model</returns>
        SMSResponseDto<SMSQuotaDto> UpdateSMSQuota(string channelKey);
    }
}
