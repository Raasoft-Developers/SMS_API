using Microsoft.Extensions.Logging;
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
        private readonly ILogger<SMSTemplateRepository> _logger;

        public SMSTemplateRepository(SMSDbContext context, ILogger<SMSTemplateRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public SMSTemplateTable GetSMSTemplate(long templateID)
        {
            return _context.SMSTemplate.FirstOrDefault(x => x.ID == templateID);
        }

        public SMSTemplateTable GetSMSTemplate(string templateName, string tenantID = null, string facilityID = null)
        {
            string defaultTemplate = "DEFAULT_SMS_NOTIFICATION";
            var qry = from st in _context.SMSTemplate
                      where st.Name == templateName
                      && (st.TenantID == null && st.FacilityID == null
                           || tenantID != null && st.TenantID == tenantID
                           || facilityID != null && st.FacilityID == facilityID
                           )
                      select st;

            var smsTemplate = qry.FirstOrDefault();

            if (smsTemplate == null)
                smsTemplate = _context.SMSTemplate.FirstOrDefault(t => t.Name == defaultTemplate);

            _logger.LogDebug($"Template used : {smsTemplate?.Name}");

            return smsTemplate;
        }

    }
}
