using Nvg.SMSService.Data.Entities;
using Nvg.SMSService.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.SMSService.Data.SMSChannel
{
    public interface ISMSChannelRepository
    {
        SMSResponseDto<SMSChannelTable> AddSMSChannel(SMSChannelTable channelInput);
        SMSResponseDto<SMSChannelTable> UpdateSMSChannel(SMSChannelTable channelInput);
        SMSResponseDto<SMSChannelDto> GetSMSChannelByKey(string channelKey);
        SMSResponseDto<bool> CheckIfChannelExist(string channelKey);
        SMSResponseDto<string> CheckIfSmsChannelIDIsValid(string channelID);
        SMSResponseDto<string> CheckIfSmsChannelIDKeyValid(string channelID, string channelKey);
    }
}
