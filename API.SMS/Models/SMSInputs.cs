using AutoMapper;
using SMSService.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.SMS.Models
{
    public class SMSInput
    {
        public string ChannelKey { get; set; }
        public string TemplateName { get; set; }
        public string Variant { get; set; }
        public string Sender { get; set; }
        public string Recipients { get; set; }
        public Dictionary<string, string> MessageParts { get; set; }
        public string Tag { get; set; }
    }
    public class SMSInputProfile : Profile
    {
        public SMSInputProfile()
        {
            CreateMap<SMSInput, SMSDto>().ReverseMap();
        }
    }
}
