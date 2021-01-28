using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBus.Events;

namespace Nvg.SMSBackgroundTask.Events
{
    public class SendSMSEvent : IntegrationEvent
    {
        public string TenantID { get; set; }
        public string FacilityID { get; set; }
        public string Sender { get; set; }
        public string Recipients { get; set; }
        public string TemplateName { get; set; }
        public Dictionary<string, string> MessageParts { get; set; }

        public SendSMSEvent(string sender, string recipients, string templateName)
        {
            Sender = sender;
            Recipients = recipients;
            TemplateName = templateName;
        }
    }
}
