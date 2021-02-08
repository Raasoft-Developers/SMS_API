using Nvg.SMSService.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.SMSService.SMS
{
    public interface ISMSInteractor
    {
        SMSResponseDto<string> SendSMS(SMSDto smsInputs);
        SMSResponseDto<SMSPoolDto> AddSMSPool(SMSPoolDto poolInput);
        SMSResponseDto<SMSProviderSettingsDto> AddSMSProvider(SMSProviderSettingsDto providerInput);
        SMSResponseDto<SMSChannelDto> AddSMSChannel(SMSChannelDto channelInput);
        SMSResponseDto<SMSTemplateDto> AddSMSTemplate(SMSTemplateDto templateInput);
        SMSResponseDto<SMSChannelDto> GetSMSChannelByKey(string channelKey);
        SMSResponseDto<List<SMSProviderSettingsDto>> GetSMSProvidersByPool(string poolName, string providerName);
    }
}
