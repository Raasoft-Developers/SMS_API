using Nvg.SMSService.DTOS;
using System.Collections.Generic;

namespace Nvg.SMSService.SMSProvider
{
    public interface ISMSProviderInteractor
    {
        SMSResponseDto<SMSProviderSettingsDto> AddSMSProvider(SMSProviderSettingsDto smsProviderInput);
        SMSResponseDto<SMSProviderSettingsDto> UpdateSMSProvider(SMSProviderSettingsDto smsProviderInput);
        SMSResponseDto<SMSProviderSettingsDto> GetSMSProviderByChannel(string channelKey);
        SMSResponseDto<List<SMSProviderSettingsDto>> GetSMSProvidersByPool(string poolName, string providerName);
    }
}
