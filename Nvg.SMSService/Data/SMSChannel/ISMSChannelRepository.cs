using Nvg.SMSService.Data.Entities;
using Nvg.SMSService.DTOS;
using System.Collections.Generic;

namespace Nvg.SMSService.Data.SMSChannel
{
    public interface ISMSChannelRepository
    {
        /// <summary>
        /// Adds the SMS channel in the database.
        /// </summary>
        /// <param name="channelInput"><see cref="SMSChannelTable"/> model</param>
        /// <returns><see cref="SMSResponseDto{T}"/> model</returns>
        SMSResponseDto<SMSChannelTable> AddSMSChannel(SMSChannelTable channelInput);

        /// <summary>
        /// Updates the SMS channel in the database.
        /// </summary>
        /// <param name="channelInput"><see cref="SMSChannelTable"/> model</param>
        /// <returns><see cref="SMSResponseDto{T}"/> model</returns>
        SMSResponseDto<SMSChannelTable> UpdateSMSChannel(SMSChannelTable channelInput);

        /// <summary>
        /// Gets the SMS channels in the database by channel key.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <returns><see cref="SMSResponseDto{T}"/> model</returns>
        SMSResponseDto<SMSChannelDto> GetSMSChannelByKey(string channelKey);

        /// <summary>
        /// Checks if the SMS channel exists in the database.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <returns><see cref="SMSResponseDto{T}"/> model</returns>
        SMSResponseDto<bool> CheckIfChannelExist(string channelKey);
        
        /// <summary>
        /// Gets the SMS channels in the database.
        /// </summary>
        /// <param name="poolID">Pool ID</param>
        /// <returns><see cref="SMSResponseDto{T}"/> model</returns>
        SMSResponseDto<List<SMSChannelDto>> GetSMSChannels(string poolID);

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

        /// <summary>
        /// Gets the SMS channel in the database by channel ID.
        /// </summary>
        /// <param name="channelID">Channel ID</param>
        /// <returns><see cref="SMSResponseDto{T}"/> model</returns>
        SMSResponseDto<SMSChannelTable> GetSMSChannelByID(string channelID);

        /// <summary>
        /// Gets the SMS channel ID exists in the database.
        /// </summary>
        /// <param name="channelID">Channel ID</param>
        /// <returns><see cref="SMSResponseDto{T}"/> model</returns>
        SMSResponseDto<string> CheckIfSmsChannelIDIsValid(string channelID);

        /// <summary>
        /// Gets the SMS channel ID and Key combination exists in the database.
        /// </summary>
        /// <param name="channelID">Channel ID</param>
        /// <param name="channelKey">Channel Key</param>
        /// <returns><see cref="SMSResponseDto{T}"/> model</returns>
        SMSResponseDto<string> CheckIfSmsChannelIDKeyValid(string channelID, string channelKey);
    }
}
