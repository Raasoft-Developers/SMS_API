using Nvg.SMSService.DTOS;
using System.Collections.Generic;

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
        /// Adds the sms pool into the database.
        /// </summary>
        /// <param name="poolInput"><see cref="SMSPoolDto"/> model</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<SMSPoolDto> AddSMSPool(SMSPoolDto poolInput);

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
        /// Add the SMS provider to the database.
        /// </summary>
        /// <param name="providerInput"><see cref="SMSProviderSettingsDto"/></param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<SMSProviderSettingsDto> AddSMSProvider(SMSProviderSettingsDto providerInput);

        /// <summary>
        /// Update the SMS provider in the database.
        /// </summary>
        /// <param name="providerInput"><see cref="SMSProviderSettingsDto"/></param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<SMSProviderSettingsDto> UpdateSMSProvider(SMSProviderSettingsDto providerInput);

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
        /// Gets all the SMS Templates by channel ID.
        /// </summary>
        /// <param name="channelID">Channel ID</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<List<SMSTemplateDto>> GetSMSTemplatesByChannelID(string channelID);

        /// <summary>
        /// Delete the SMS Channel into the database.
        /// </summary>
        /// <param name="channelID">Provider ID</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<string> DeleteSMSChannel(string channelID);
        /// <summary>
        /// Add the SMS provider to the database.
        /// </summary>
        /// <param name="channelInput"><see cref="SMSChannelDto"/></param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<SMSChannelDto> AddSMSChannel(SMSChannelDto channelInput);

        /// <summary>
        /// Update the SMS provider in the database.
        /// </summary>
        /// <param name="channelInput"><see cref="SMSChannelDto"/></param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<SMSChannelDto> UpdateSMSChannel(SMSChannelDto channelInput);

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
        /// Gets the sms template by ID.
        /// </summary>
        /// <param name="templateID">template ID</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<SMSTemplateDto> GetSMSTemplate(string templateID);

        /// <summary>
        /// Updates the SMS template
        /// </summary>
        /// <param name="templateInput"><see cref="SMSTemplateDto"/></param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<SMSTemplateDto> UpdateSMSTemplate(SMSTemplateDto templateInput);

        /// <summary>
        /// Adds the SMS template
        /// </summary>
        /// <param name="templateInput"><see cref="SMSTemplateDto"/></param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<SMSTemplateDto> AddSMSTemplate(SMSTemplateDto templateInput);

        /// <summary>
        /// Delete the SMS Template into the database.
        /// </summary>
        /// <param name="templateID">Template ID</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<string> DeleteSMSTemplate(string templateID);
        #endregion

        #region SMS Histories
        /// <summary>
        /// Gets the SMS Histories by channel.
        /// </summary>
        /// <param name="channelID">Channel ID</param>
        /// <param name="tag">Tag</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<List<SMSHistoryDto>> GetSMSHistories(string channelID, string tag);
        #endregion

        #region SMS Quota
        /// <summary>
        /// Gets the SMS Quota.
        /// </summary>
        /// <param name="channelID">Channel ID</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<List<SMSQuotaDto>> GetSMSQuotaList(string channelID);

        /// <summary>
        /// Adds the SMS Quota Values for Channel.
        /// </summary>
        /// <param name="smsChannel">SMS Channel </param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<SMSQuotaDto> AddSMSQuota(SMSChannelDto smsChannel);

        /// <summary>
        /// Updates the SMS quota.
        /// </summary>
        /// <param name="smsChannel">SMS Channel </param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<SMSQuotaDto> UpdateSMSQuota(SMSChannelDto smsChannel);

        /// <summary>
        /// Deletes the SMS quota.
        /// </summary>
        /// <param name="channelID">Channel ID</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<string> DeleteSMSQuota(string channelID);
        #endregion


        /// <summary>
        /// Sends the sms.
        /// </summary>
        /// <param name="smsInputs"><see cref="SMSDto"/> model</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<string> SendSMS(SMSDto smsInputs);
    }
}
