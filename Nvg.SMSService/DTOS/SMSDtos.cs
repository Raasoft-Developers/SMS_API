using AutoMapper;
using System.Collections.Generic;

namespace Nvg.SMSService.DTOS
{
    public class SMSDto
    {
        public string ChannelKey { get; set; }
        public string TemplateName { get; set; }
        public string Variant { get; set; }
        public string Sender { get; set; }
        public string Recipients { get; set; }
        /*public string Content { get; set; }
        public string Username { get; set; }*/
        public Dictionary<string, string> MessageParts { get; set; }
        public string Tag { get; set; }
    }

    public class SMSResponseDto<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }

    public class SMSDTOProfile : Profile
    {
        
    }

}
