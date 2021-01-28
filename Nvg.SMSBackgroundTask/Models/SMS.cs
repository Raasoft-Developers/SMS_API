using Nvg.SMSService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.SMSBackgroundTask.Models
{
    public class SMS
    {
        public SMS()
        {
            MessageParts = new Dictionary<string, string>();
        }
        public string To { get; set; }
        public string Sender { get; set; }
        public string TemplateName { get; set; }
        public Dictionary<string, string> MessageParts { get; set; }
        public string TenantID { get; internal set; }
        public string FacilityID { get; internal set; }


        public string GetMessage(ISMSTemplateInteractor smsTemplateInteractor)
        {
            var template = smsTemplateInteractor.GetSMSTemplate(TemplateName, TenantID, FacilityID);

            var msg = template.MessageTemplate;
            foreach (var item in MessageParts)
            {
                msg = msg.Replace($"{{{item.Key}}}", item.Value);
            }
            return msg;
        }

    }
}
