using Nvg.SMSService.Data.Entities;
using Nvg.SMSService.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.SMSService.Data.SMSProvider
{
    public interface ISMSProviderRepository
    {
        SMSResponseDto<SMSProviderSettingsTable> AddSMSProvider(SMSProviderSettingsTable providerInput);
        SMSResponseDto<SMSProviderSettingsTable> GetSMSProviderByName(string providerName);
        SMSResponseDto<SMSProviderSettingsTable> GetSMSProviderByChannelKey(string channelKey);
    }
}
