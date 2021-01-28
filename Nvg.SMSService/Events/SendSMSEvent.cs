using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.SMSService.Events
{
    public class SendSMSEvent : IntegrationEvent
    {
        public string TenantID { get; set; }
        public string FacilityID { get; set; }
        public string Recipients { get; set; }
        public string TemplateName { get; set; }
        public Dictionary<string, string> MessageParts { get; set; }

    }
}
