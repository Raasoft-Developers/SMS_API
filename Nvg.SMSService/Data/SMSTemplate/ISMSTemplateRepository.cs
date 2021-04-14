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
        SMSResponseDto<SMSTemplateTable> AddSMSTemplate(SMSTemplateTable templateInput);
        SMSResponseDto<SMSTemplateTable> UpdateSMSTemplate(SMSTemplateTable templateInput);
        SMSTemplateTable GetSMSTemplate(string templateID);
        SMSTemplateTable GetSMSTemplate(string templateName, string channelKey, string variant = null);
        SMSResponseDto<bool> CheckIfTemplateExist(string channelKey, string templateName);

    }
}
