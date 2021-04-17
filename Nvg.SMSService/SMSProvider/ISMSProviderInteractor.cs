using Nvg.SMSService.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

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
