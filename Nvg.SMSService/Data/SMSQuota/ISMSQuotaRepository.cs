using Nvg.SMSService.Data.Entities;
using Nvg.SMSService.DTOS;

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
        /// Updates the SMS Quota.
        /// </summary>
        /// <param name="channelID">Channel ID</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<SMSQuotaTable> UpdateSMSQuota(string channelID);
    }
}
