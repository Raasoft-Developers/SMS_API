using Nvg.SMSService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nvg.SMSService.Data
{
    public interface ISMSTemplateRepository
    {
        SMSTemplateTable GetSMSTemplate(long templateID);
        SMSTemplateTable GetSMSTemplate(string templateName, string tenantID = null, string facilityID = null);
    }
}
