using AutoMapper;
using SMSService.Data.Entities;
using System.Collections.Generic;

namespace SMSService.DTOS
{
    public class SMSTemplateDto
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Variant { get; set; }
        public string Sender { get; set; }
        public string SMSPoolID { get; set; }
        public string SMSPoolName { get; set; }
        public string MessageTemplate { get; set; }
    }

    public class SMSTemplateProfile : Profile
    {
        public SMSTemplateProfile()
        {
            CreateMap<SMSTemplateDto, SMSTemplateTable>().ReverseMap();
            CreateMap<SMSResponseDto<SMSTemplateTable>, SMSResponseDto<SMSTemplateDto>>();
            CreateMap<SMSResponseDto<List<SMSTemplateTable>>, SMSResponseDto<List<SMSTemplateDto>>>();
        }
    }
}
