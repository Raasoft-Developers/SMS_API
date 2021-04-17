using Nvg.SMSService.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.SMSService.SMSChannel
{
    public interface ISMSChannelInteractor
    {
        SMSResponseDto<SMSChannelDto> AddSMSChannel(SMSChannelDto channelInput);
        SMSResponseDto<SMSChannelDto> UpdateSMSChannel(SMSChannelDto channelInput);
        SMSResponseDto<SMSChannelDto> GetSMSChannelByKey(string channelKey);
        SMSResponseDto<bool> CheckIfChannelExist(string channelKey);
    }
}
