using SMSService.DTOS;

namespace SMSService
{
    public interface ISMSTemplateInteractor
    {
        /// <summary>
        /// Adds the SMS Templates in the database.
        /// </summary>
        /// <param name="templateInput"><see cref="SMSTemplateDto"/> model<param>
        /// <returns><see cref="SMSResponseDto{T}"/> model</returns>
        SMSResponseDto<SMSTemplateDto> AddSMSTemplate(SMSTemplateDto templateInput);

        /// <summary>
        /// Updates the SMS Templates in the database.
        /// </summary>
        /// <param name="templateInput"><see cref="SMSTemplateDto"/> model</param>
        /// <returns><see cref="SMSResponseDto{T}"/> model</returns>
        SMSResponseDto<SMSTemplateDto> UpdateSMSTemplate(SMSTemplateDto templateInput);

        /// <summary>
        /// Gets the SMS Template data.
        /// </summary>
        /// <param name="id">Template ID</param>
        /// <returns><see cref="SMSTemplateDto"/> model</returns>
        SMSTemplateDto GetSMSTemplate(string id);

        /// <summary>
        /// Gets the SMS Template data.
        /// </summary>
        /// <param name="templateName">Template name</param>
        /// <param name="channelKey">Channel Key</param>
        /// <param name="variant">Variant</param>
        /// <returns><see cref="SMSTemplateDto"/> model</returns>
        SMSTemplateDto GetSMSTemplate(string templateName, string channelKey, string variant = null);

        /// <summary>
        /// Updates the SMS Templates in the database.
        /// </summary>
        /// <param name="templateInput"><see cref="SMSTemplateDto"/></param>
        /// <returns><see cref="SMSResponseDto{T}"/> model</returns>
        SMSResponseDto<bool> CheckIfTemplateExist(string channelKey, string templateName);

    }
}