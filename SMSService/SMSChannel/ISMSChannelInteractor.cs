using SMSService.DTOS;

namespace SMSService.SMSChannel
{
    public interface ISMSChannelInteractor
    {
        /// <summary>
        /// Adds the SMS channel in the database.
        /// </summary>
        /// <param name="channelInput"><see cref="SMSChannelDto"/> model</param>
        /// <returns><see cref="SMSResponseDto{T}"/> model</returns>
        SMSResponseDto<SMSChannelDto> AddSMSChannel(SMSChannelDto channelInput);

        /// <summary>
        /// Updates the SMS channel in the database.
        /// </summary>
        /// <param name="channelInput"><see cref="SMSChannelDto"/> model</param>
        /// <returns><see cref="SMSResponseDto{T}"/> model</returns>
        SMSResponseDto<SMSChannelDto> UpdateSMSChannel(SMSChannelDto channelInput);

        /// <summary>
        /// Gets the SMS channel data by channel key.
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
    }
}
