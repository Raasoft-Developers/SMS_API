using Nvg.SMSService.Data.Entities;
using Nvg.SMSService.DTOS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nvg.SMSService.Data.SMSTemplate
{
    public interface ISMSTemplateRepository
    {
        SMSResponseDto<SMSTemplateTable> AddUpdateSMSTemplate(SMSTemplateTable templateInput);
        SMSTemplateTable GetSMSTemplate(string templateID);
        SMSTemplateTable GetSMSTemplate(string templateName, string channelKey, string variant = null);
        SMSResponseDto<bool> CheckIfTemplateExist(string channelKey, string templateName);
        /// <summary>
        /// Gets the SMS template by pool name.
        /// </summary>
        /// <param name="poolID">Pool ID</param>
        /// <returns><see cref="SMSTemplateTable"/></returns>
        SMSResponseDto<List<SMSTemplateTable>> GetSMSTemplatesByPool(string poolID);

        /// <summary>
        /// Delete the SMS template by template Id.
        /// </summary>
        /// <param name="templateID">Template Id</param>
        /// <returns><see cref="SMSTemplateTable"/></returns>
        SMSResponseDto<string> DeleteSMSTemplate(string templateID);
    }
}
