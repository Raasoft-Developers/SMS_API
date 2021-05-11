using Nvg.SMSService;
using System.Collections.Generic;

namespace Nvg.SMSBackgroundTask.Models
{
    public class SMS
    {
        public SMS()
        {
            MessageParts = new Dictionary<string, string>();
        }
        public string Recipients { get; set; }
        public string Sender { get; set; }
        public string TemplateName { get; set; }
        public Dictionary<string, string> MessageParts { get; set; }
        public string Variant { get; internal set; }
        public string ChannelKey { get; internal set; }
        public string ProviderName { get; internal set; }
        public string Tag { get; set; }

        public string GetMessage(ISMSTemplateInteractor smsTemplateInteractor)
        {
            var template = smsTemplateInteractor.GetSMSTemplate(TemplateName, ChannelKey, Variant);
            var msg = template?.MessageTemplate;
            foreach (var item in MessageParts)
                msg = msg.Replace($"{{{item.Key}}}", item.Value);
            return msg;
        }

        public string GetSender(ISMSTemplateInteractor smsTemplateInteractor)
        {
            var template = smsTemplateInteractor.GetSMSTemplate(TemplateName, ChannelKey, Variant);
            return template?.Sender;
        }
    }
}
