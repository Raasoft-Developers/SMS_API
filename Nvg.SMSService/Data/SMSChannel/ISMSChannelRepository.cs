using Nvg.SMSService.Data.Entities;
using Nvg.SMSService.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.SMSService.Data.SMSChannel
{
    public interface ISMSChannelRepository
    {
        SMSResponseDto<SMSChannelTable> AddUpdateSMSChannel(SMSChannelTable channelInput);
        SMSResponseDto<SMSChannelTable> GetSMSChannelByKey(string channelKey);
        SMSResponseDto<bool> CheckIfChannelExist(string channelKey);
        /// <summary>
        /// Gets the SMS channels in the database.
        /// </summary>
        /// <param name="poolID">Pool ID</param>
        /// <returns><see cref="SMSResponseDto{T}"/> model</returns>
        SMSResponseDto<List<SMSChannelTable>> GetSMSChannels(string poolID);

        /// <summary>
        /// Delete the SMS channel in the database.
        /// </summary>
        /// <param name="channelID">Channel ID</param>
        /// <returns><see cref="SMSResponseDto{T}"/> model</returns>
        SMSResponseDto<string> DeleteSMSChannel(string channelID);

        /// <summary>
        /// Gets the SMS channel keys in the database.
        /// </summary>
        /// <returns><see cref="SMSResponseDto{T}"/> model</returns>
        SMSResponseDto<List<SMSChannelTable>> GetSMSChannelKeys();

        SMSResponseDto<SMSChannelTable> GetSMSChannelByID(string channelID);

    }
}
