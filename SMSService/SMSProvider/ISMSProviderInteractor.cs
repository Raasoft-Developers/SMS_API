using SMSService.DTOS;
using System.Collections.Generic;

namespace SMSService.SMSProvider
{
    public interface ISMSProviderInteractor
    {
        /// <summary>
        /// Adds the SMS Provider in the database.
        /// </summary>
        /// <param name="smsProviderInput"><see cref="SMSProviderSettingsDto"/> model</param>
        /// <returns><see cref="SMSResponseDto{T}"/> model</returns>
        SMSResponseDto<SMSProviderSettingsDto> AddSMSProvider(SMSProviderSettingsDto smsProviderInput);

        /// <summary>
        /// Updates the SMS Provider in the database.
        /// </summary>
        /// <param name="smsProviderInput"><see cref="SMSProviderSettingsDto"/> model</param>
        /// <returns><see cref="SMSResponseDto{T}"/> model</returns>
        SMSResponseDto<SMSProviderSettingsDto> UpdateSMSProvider(SMSProviderSettingsDto smsProviderInput);

        /// <summary>
        /// Gets the SMS Provider data by channel key.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <returns><see cref="SMSResponseDto{T}"/> model</returns>
        SMSResponseDto<SMSProviderSettingsDto> GetSMSProviderByChannel(string channelKey);

        /// <summary>
        /// Gets the SMS Provider data by pool.
        /// </summary>
        /// <param name="poolName">Pool Name</param>
        /// <param name="providerName">Provider Name</param>
        /// <returns><see cref="SMSResponseDto{T}"/> model</returns>
        SMSResponseDto<List<SMSProviderSettingsDto>> GetSMSProvidersByPool(string poolName, string providerName);
    }
}
