using SMSService.Data.Entities;
using SMSService.DTOS;
using System.Collections.Generic;

namespace SMSService.Data.SMSProvider
{
    public interface ISMSProviderRepository
    {
        /// <summary>
        /// Adds the SMS Provider in the database.
        /// </summary>
        /// <param name="providerInput"><see cref="SMSProviderSettingsTable"/></param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<SMSProviderSettingsTable> AddSMSProvider(SMSProviderSettingsTable providerInput);

        /// <summary>
        /// Updates the SMS Provider in the database.
        /// </summary>
        /// <param name="providerInput"><see cref="SMSProviderSettingsTable"/></param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<SMSProviderSettingsTable> UpdateSMSProvider(SMSProviderSettingsTable providerInput);

        /// <summary>
        /// Gets the SMS Provider data by provider name.
        /// </summary>
        /// <param name="providerName">Provider Name</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<SMSProviderSettingsTable> GetSMSProviderByName(string providerName);

        /// <summary>
        /// Gets the SMS Provider data by channel key.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<SMSProviderSettingsTable> GetSMSProviderByChannelKey(string channelKey);

        /// <summary>
        /// Gets the SMS Provider data by pool name and provider name.
        /// </summary>
        /// <param name="poolName">Pool Name</param>
        /// <param name="providerName">Provider Name</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<List<SMSProviderSettingsTable>> GetSMSProvidersByPool(string poolName, string providerName);

        /// <summary>
        /// Gets the default SMS Provider data.
        /// </summary>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<SMSProviderSettingsTable> GetDefaultSMSProvider();

        /// <summary>
        /// Gets all the SMS providers.
        /// </summary>
        /// <param name="poolName">Pool Name</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<List<SMSProviderSettingsTable>> GetSMSProviders(string poolName);

        /// <summary>
        /// Gets all the SMS provider names by Pool ID.
        /// </summary>
        /// <param name="poolID">Pool ID</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<List<SMSProviderSettingsTable>> GetSMSProviderNames(string poolID);

        /// <summary>
        /// Deletes the SMS provider data.
        /// </summary>
        /// <param name="providerID">Provider ID</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<string> DeleteSMSProvider(string providerID);

        /// <summary>
        /// Checks the SMS provider ID exists in the database.
        /// </summary>
        /// <param name="providerID">Provider ID</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<string> CheckIfSmsProviderIDIsValid(string providerID);

        /// <summary>
        /// Checks the SMS provider ID and name combination exists in the database.
        /// </summary>
        /// <param name="providerID">Provider ID</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<string> CheckIfSmsProviderIDNameValid(string providerID, string providerName);
    }
}
