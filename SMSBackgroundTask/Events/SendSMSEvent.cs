using EventBus.Events;
using System.Collections.Generic;

namespace SMSBackgroundTask.Events
{
    public class SendSMSEvent : IntegrationEvent
    {
        public string ChannelKey { get; set; }
        public string Variant { get; set; }
        public string Sender { get; set; }
        public string Recipients { get; set; }
        public string TemplateName { get; set; }
        public string Tag { get; set; }
        public Dictionary<string, string> MessageParts { get; set; }

        public SendSMSEvent(string sender, string recipients, string templateName)
        {
            Sender = sender;
            Recipients = recipients;
            TemplateName = templateName;
        }
    }
}
