using Nvg.SMSService.Data.Entities;
using Nvg.SMSService.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.SMSService.Data.SMSProvider
{
    public interface ISMSProviderRepository
    {
        SMSResponseDto<SMSProviderSettingsTable> AddUpdateSMSProvider(SMSProviderSettingsTable providerInput);
        SMSResponseDto<SMSProviderSettingsTable> GetSMSProviderByName(string providerName);
        SMSResponseDto<SMSProviderSettingsTable> GetSMSProviderByChannelKey(string channelKey);
        SMSResponseDto<List<SMSProviderSettingsTable>> GetSMSProvidersByPool(string poolName, string providerName);
        SMSResponseDto<SMSProviderSettingsTable> GetDefaultSMSProvider();

        /// <summary>
        /// Gets all the SMS provider settings.
        /// </summary>
        /// <param name="poolName">Pool name</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<List<SMSProviderSettingsTable>> GetSMSProviders(string poolName);

        /// <summary>
        /// Gets all the SMS provider Names.
        /// </summary>
        /// <param name="poolID">Pool ID</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<List<SMSProviderSettingsTable>> GetSMSProviderNames(string poolID);

        /// <summary>
        /// Delete the SMS provider to the database.
        /// </summary>
        /// <param name="providerInput"><see cref="SMSProviderSettingsTable"/> model</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<string> DeleteSMSProvider(string providerID);
    }
}
