using SMSService.Data.Entities;
using SMSService.DTOS;
using System.Collections.Generic;

namespace SMSService.Data.SMSTemplate
{
    public interface ISMSTemplateRepository
    {
        /// <summary>
        /// Adds the SMS template.
        /// </summary>
        /// <param name="templateInput"><see cref="SMSTemplateTable"/> model</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<SMSTemplateTable> AddSMSTemplate(SMSTemplateTable templateInput);

        /// <summary>
        /// Updates the SMS template.
        /// </summary>
        /// <param name="templateInput"><see cref="SMSTemplateTable"/> model</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<SMSTemplateTable> UpdateSMSTemplate(SMSTemplateTable templateInput);

        /// <summary>
        /// Gets the SMS template.
        /// </summary>
        /// <param name="templateID">Template ID</param>
        /// <returns><see cref="SMSTemplateTable"/></returns>
        SMSTemplateTable GetSMSTemplate(string templateID);

        /// <summary>
        /// Gets the SMS template.
        /// </summary>
        /// <param name="templateName">Template Name</param>
        /// <param name="channelKey">Channel Key</param>
        /// <param name="variant">Variant</param>
        /// <returns><see cref="SMSTemplateTable"/></returns>
        SMSTemplateTable GetSMSTemplate(string templateName, string channelKey, string variant = null);

        /// <summary>
        /// Checks if the SMS template exists in the database.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <param name="templateName">Template Name</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<bool> CheckIfTemplateExist(string channelKey, string templateName);
        /// <summary>
        /// Gets the SMS template by pool name.
        /// </summary>
        /// <param name="poolID">Pool ID</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<List<SMSTemplateTable>> GetSMSTemplatesByPool(string poolID);

        /// <summary>
        /// Delete the SMS template by template Id.
        /// </summary>
        /// <param name="templateID">Template Id</param>
        /// <returns><see cref="SMSResponseDto{T}"/></returns>
        SMSResponseDto<string> DeleteSMSTemplate(string templateID);
    }
}
