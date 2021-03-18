using Nvg.SMSService.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.SMSService.SMS
{
    public interface ISMSManagementInteractor
    {
        #region SMS Pool
        /// <summary>
        /// Gets all the sms pools.
        /// </summary>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<List<SMSPoolDto>> GetSMSPools();

        /// <summary>
        /// Gets all the sms pool Names.
        /// </summary>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<List<SMSPoolDto>> GetSMSPoolNames();

        /// <summary>
        /// Updates the sms pool into the database.
        /// </summary>
        /// <param name="SMSPoolInput"><see cref="SMSPoolDto"/> model</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<SMSPoolDto> UpdateSMSPool(SMSPoolDto SMSPoolInput);

        /// <summary>
        /// Delete the sms pool into the database.
        /// </summary>
        /// <param name="poolID">Pool ID</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<string> DeleteSMSPool(string poolID);
        #endregion

        #region SMS Provider
        /// <summary>
        /// Gets all the SMS Provider.
        /// </summary>
        /// <param name="poolName">Pool Name</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<List<SMSProviderSettingsDto>> GetSMSProviders(string poolName);

        /// <summary>
        /// Gets all the SMS Provider Names.
        /// </summary>
        /// <param name="poolName">Pool Name</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<List<SMSProviderSettingsDto>> GetSMSProviderNames(string poolName);

        /// <summary>
        /// Add/Update the SMS provider to the database.
        /// </summary>
        /// <param name="providerInput"><see cref="SMSProviderSettingsDto"/></param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<SMSProviderSettingsDto> AddUpdateSMSProvider(SMSProviderSettingsDto providerInput);

        /// <summary>
        /// Delete the SMS Provider into the database.
        /// </summary>
        /// <param name="providerID">Provider ID</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<string> DeleteSMSProvider(string providerID);
        #endregion

        #region SMS Channel
        /// <summary>
        /// Gets all the SMS Channels for pool.
        /// </summary>
        /// <param name="poolID">Pool ID</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<List<SMSChannelDto>> GetSMSChannelsByPool(string poolID);

        /// <summary>
        /// Delete the SMS Channel into the database.
        /// </summary>
        /// <param name="channelID">Provider ID</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<string> DeleteSMSChannel(string channelID);
        /// <summary>
        /// Add/Update the SMS provider to the database.
        /// </summary>
        /// <param name="channelInput"><see cref="SMSChannelDto"/></param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<SMSChannelDto> AddUpdateSMSChannel(SMSChannelDto channelInput);

        /// <summary>
        /// Gets the SMS Channel keys from the database.
        /// </summary>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<List<SMSChannelDto>> GetSMSChannelKeys();
        #endregion

        #region SMS Template
        /// <summary>
        /// Gets all the SMS Templates for pool.
        /// </summary>
        /// <param name="poolID">Pool Name</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<List<SMSTemplateDto>> GetSMSTemplatesByPool(string poolID);

        /// <summary>
        /// Add/Updates the SMS template
        /// </summary>
        /// <param name="templateInput"><see cref="SMSTemplateDto"/></param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<SMSTemplateDto> AddUpdateSMSTemplate(SMSTemplateDto templateInput);

        /// <summary>
        /// Delete the SMS Template into the database.
        /// </summary>
        /// <param name="templateID">Template ID</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<string> DeleteSMSTemplate(string templateID);
        #endregion

        #region SMS Histories

        SMSResponseDto<List<SMSHistoryDto>> GetSMSHistories(string channelID, string tag);
        #endregion
    }
}
