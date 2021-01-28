using Nvg.SMSService.DTOS;

namespace Nvg.SMSService
{
    public interface ISMSTemplateInteractor
    {
        SMSTemplateDto GetSMSTemplate(long id);
        SMSTemplateDto GetSMSTemplate(string templateName, string tenantID = null, string facilityID = null);
    }
}