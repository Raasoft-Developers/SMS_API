using System;
using System.Collections.Generic;
using System.Text;

namespace SMSService.SMSSenderModel
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
        public string Variant { get; set; }
        public string ChannelKey { get; set; }
        public string ProviderName { get; set; }
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
