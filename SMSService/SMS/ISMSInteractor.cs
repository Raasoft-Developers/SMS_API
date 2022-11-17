using SMSService.DTOS;
using System.Collections.Generic;

namespace SMSService.SMS
{
    public interface ISMSInteractor
    {
        /// <summary>
        /// Sends the SMS.
        /// </summary>
        /// <param name="smsInputs"><see cref="SMSDto"/> model</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<SMSBalanceDto> SendSMS(SMSDto smsInputs);

        /// <summary>
        /// Adds the SMS Pool to the database.
        /// </summary>
        /// <param name="poolInput"><see cref="SMSPoolDto"/> model</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<SMSPoolDto> AddSMSPool(SMSPoolDto poolInput);

        /// <summary>
        /// Adds the SMS Provider to the database.
        /// </summary>
        /// <param name="providerInput"><see cref="SMSProviderSettingsDto"/> model</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<SMSProviderSettingsDto> AddSMSProvider(SMSProviderSettingsDto providerInput);

        /// <summary>
        /// Updates the SMS Provider in the database.
        /// </summary>
        /// <param name="providerInput"><see cref="SMSProviderSettingsDto"/> model</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<SMSProviderSettingsDto> UpdateSMSProvider(SMSProviderSettingsDto providerInput);

        /// <summary>
        /// Adds the SMS Channel to the database.
        /// </summary>
        /// <param name="channelInput"><see cref="SMSChannelDto"/> model</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<SMSChannelDto> AddSMSChannel(SMSChannelDto channelInput);

        /// <summary>
        /// Updates the SMS Channel in the database.
        /// </summary>
        /// <param name="channelInput"><see cref="SMSChannelDto"/> model</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<SMSChannelDto> UpdateSMSChannel(SMSChannelDto channelInput);

        /// <summary>
        /// Adds the SMS Template to the database.
        /// </summary>
        /// <param name="templateInput"><see cref="SMSTemplateDto"/> model</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<SMSTemplateDto> AddSMSTemplate(SMSTemplateDto templateInput);

        /// <summary>
        /// Updates the SMS Template in the database.
        /// </summary>
        /// <param name="templateInput"><see cref="SMSTemplateDto"/> model</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<SMSTemplateDto> UpdateSMSTemplate(SMSTemplateDto templateInput);

        /// <summary>
        /// Gets the SMS Channel data by channel key.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<SMSChannelDto> GetSMSChannelByKey(string channelKey);

        /// <summary>
        /// Gets the SMS Provider data by pool name and provider name.
        /// </summary>
        /// <param name="poolName">Pool Name</param>
        /// <param name="providerName">Provider Name</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<List<SMSProviderSettingsDto>> GetSMSProvidersByPool(string poolName, string providerName);

        /// <summary>
        /// Gets the SMS History data.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <param name="tag">Tag</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<List<SMSHistoryDto>> GetSMSHistoriesByTag(string channelKey, string tag);

        /// <summary>
        /// Gets the SMS History data by date range.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <param name="tag">Tag</param>
        /// <param name="fromDate">From Date</param>
        /// <param name="toDate">To Date</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<List<SMSHistoryDto>> GetSMSHistoriesByDateRange(string channelKey, string tag, string fromDate, string toDate);

        /// <summary>
        /// Gets the SMS Quota.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<SMSQuotaDto> GetSMSQuota(string channelKey);
    }
}
