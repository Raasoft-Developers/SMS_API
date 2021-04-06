using Nvg.SMSService.DTOS;

namespace Nvg.SMSService
{
    public interface ISMSTemplateInteractor
    {
        SMSResponseDto<SMSTemplateDto> AddSMSTemplate(SMSTemplateDto templateInput);
        SMSResponseDto<SMSTemplateDto> UpdateSMSTemplate(SMSTemplateDto templateInput);
        SMSTemplateDto GetSMSTemplate(string id);
        SMSTemplateDto GetSMSTemplate(string templateName, string tenantID = null, string facilityID = null);
        SMSResponseDto<bool> CheckIfTemplateExist(string channelKey, string templateName);

    }
}