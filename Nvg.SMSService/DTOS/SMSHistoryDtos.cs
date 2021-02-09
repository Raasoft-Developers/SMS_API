using AutoMapper;
using Nvg.SMSService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.SMSService.DTOS
{
    public class SMSHistoryDto
    {
        public string ID { get; set; }
        public string MessageSent { get; set; }
        public string Sender { get; set; }
        public string Recipients { get; set; }
        public DateTime SentOn { get; set; }
        public string TemplateName { get; set; }
        public string TemplateVariant { get; set; }
        public string SMSChannelID { get; set; }
        public string ChannelKey { get; set; }
        public string SMSProviderID { get; set; }
        public string ProviderName { get; set; }
        public string Tags { get; set; }
        public string Status { get; set; }
        public int Attempts { get; set; }
    }

    public class SMSHistoryProfile : Profile
    {
        public SMSHistoryProfile()
        {
            CreateMap<SMSHistoryDto, SMSHistoryTable>().ReverseMap();
            CreateMap<SMSResponseDto<SMSHistoryTable>, SMSResponseDto<SMSHistoryDto>>();
            CreateMap<SMSResponseDto<List<SMSHistoryTable>>, SMSResponseDto<List<SMSHistoryDto>>>();

        }
    }
}
