using Nvg.SMSService.Data.Entities;
using Nvg.SMSService.DTOS;
using System.Collections.Generic;

namespace Nvg.SMSService.Data.SMSProvider
{
    public interface ISMSProviderRepository
    {
        SMSResponseDto<SMSProviderSettingsTable> AddSMSProvider(SMSProviderSettingsTable providerInput);
        SMSResponseDto<SMSProviderSettingsTable> UpdateSMSProvider(SMSProviderSettingsTable providerInput);
        SMSResponseDto<SMSProviderSettingsTable> GetSMSProviderByName(string providerName);
        SMSResponseDto<SMSProviderSettingsTable> GetSMSProviderByChannelKey(string channelKey);
        SMSResponseDto<List<SMSProviderSettingsTable>> GetSMSProvidersByPool(string poolName, string providerName);
        SMSResponseDto<SMSProviderSettingsTable> GetDefaultSMSProvider();
        SMSResponseDto<List<SMSProviderSettingsTable>> GetSMSProviders(string poolName);
        SMSResponseDto<List<SMSProviderSettingsTable>> GetSMSProviderNames(string poolID);
        SMSResponseDto<string> DeleteSMSProvider(string providerID);
        SMSResponseDto<string> CheckIfSmsProviderIDIsValid(string providerID);
        SMSResponseDto<string> CheckIfSmsProviderIDNameValid(string providerID, string providerName);
    }
}
