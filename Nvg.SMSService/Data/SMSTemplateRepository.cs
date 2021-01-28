using Nvg.SMSService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nvg.SMSService.Data
{
    public class SMSTemplateRepository : ISMSTemplateRepository
    {
        private readonly SMSDbContext _context;

        public SMSTemplateRepository(SMSDbContext context)
        {
            _context = context;
        }
        public SMSTemplateTable GetSMSTemplate(long templateID)
        {
            return _context.SMSTemplate.FirstOrDefault(x => x.ID == templateID);
        }

        public SMSTemplateTable GetSMSTemplate(string templateName, string tenantID = null, string facilityID = null)
        {
            var qry = from st in _context.SMSTemplate
                      where st.Name == templateName
                      && (st.TenantID == null && st.FacilityID == null
                           || tenantID != null && st.TenantID == tenantID
                           || facilityID != null && st.FacilityID == facilityID
                           )
                      select st;


            var smsTemplate = qry.FirstOrDefault();
            return smsTemplate;
        }

    }
}
